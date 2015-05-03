using UnityEngine;
using System.Collections;
using System;
using TouchScript;
using TouchScript.Gestures;

	/*
	 * Clase Atachada a la camara, Se interpretan eventos de Pan y Zoom para rotar en torno
	 * a un Objetivo (Target). Con los respectivos acercamientos y paneos
	 * La Camara debe tener ZoomGesture y PanGesture de la libreria Touchscript.
	 */ 
public class TouchCamController : MonoBehaviour {

	/********* PRIVATE *********/
	/*
	 * Gestures: Pan y Zoom
	 */
	PanGesture _PaneoIntenso;
	private float x = 0.0f;
	private float y = 0.0f;
	bool _Son2Dedos;
	Vector2 Paneo2Dedos, DeltaPaneo2Dedos;

	/*
	 * Transition variables
	 */
	public float SpeedTransition=5;
	bool IsInTransition;
	float startTime;
	Vector3 InitPosition, FinishPosition;


	ScaleGesture _ScaleoIntenso;
	float ZoomInertia=0.0f;
	float Escala=1.0f;
	
	TapGesture _TapeoIntenso;

	PressGesture _PressIntenso;
	//Vector2 _TouchScreenPos;
	/*
	 * _VSpeed: Variable Local para calcular la velocidad de Paneo
	 * Vector de Velocidad
	*/	
	Vector2 _Speed;


	private Quaternion targetRotation;

	/********* PUBLIC  *********/
	/*
	 * Gestures: Pan y Zoom
	 */

	public float distance = 10.0f;
	public float Maxdistance = 10.0f;
	public float Mindistance = 2.0f;

	public float factorPaneo=10.0f;
	public float xSpeed = 250.0f;
	public float ySpeed = 120.0f;
	
	public float yMinLimit = -20;
	public float yMaxLimit = 80;


	public float MaxDeltax=1;
	public float MaxDeltay=0.5f;
	public float FactorPaneo2Dedos=2;

	public Transform target, NextTarget;

	public Camera MyCam;

	// Use this for initialization
	void Start () {

		IsInTransition=false;
		MyCam=Camera.main;
		var angles = MyCam.transform.eulerAngles;
		x = angles.y;
		y = angles.x;
		Paneo2Dedos=new Vector2(0,0);
		
		// Make the rigid body not change rotation
		if (rigidbody) 
			rigidbody.freezeRotation = true;
	}

	private void OnEnable()
	{
		_PaneoIntenso=transform.GetComponent<PanGesture>();
		_PaneoIntenso.Panned += panStateChangeHandler;
		_PaneoIntenso.PanCompleted += panStateChangeHandler;


		_ScaleoIntenso=transform.GetComponent<ScaleGesture>();
		_ScaleoIntenso.Scaled += scaleStateChangeHandler;

		_TapeoIntenso=transform.GetComponent<TapGesture>();
		_TapeoIntenso.Tapped +=tappedHandler;

		_PressIntenso=transform.GetComponent<PressGesture>();
		_PressIntenso.Pressed += pressHandler;

	}
	
	private void OnDisable()
	{
		_PaneoIntenso.Panned -= panStateChangeHandler;
		_PaneoIntenso.PanCompleted -= panStateChangeHandler;
		_ScaleoIntenso.Scaled -= scaleStateChangeHandler;
		_TapeoIntenso.Tapped +=tappedHandler;
	}
	
	void Update ()
	{
		if (target && _Speed.magnitude>0)
		{
			// Amortiguacion de la velocidad
			if(_Speed.magnitude>0.05){
				_Speed= (1-Time.deltaTime)* _Speed;
			}
			else{
				_Speed.Set(0.0f,0.0f);
			}
			x += factorPaneo * _Speed.x * Time.deltaTime;
			y -= factorPaneo * _Speed.y * Time.deltaTime;
			
			y = ClampAngle(y, yMinLimit, yMaxLimit);
			
			MyCam.transform.rotation = Quaternion.Euler(y, x, 0);
			//transform.position = (Quaternion.Euler(y, x, 0)) * new Vector3(0.0f, 0.0f, -distance) + target.position;
		}

		if (Mathf.Abs(ZoomInertia)>0){
			if (Mathf.Abs(ZoomInertia)>0.01){
				ZoomInertia*=(1-4*Time.deltaTime);
			}
			else{
				ZoomInertia=0;
			}
			// Amortiguo el zoom
			distance/=(Escala-ZoomInertia/4);
			// Limites de escalamiento
			distance=Mathf.Clamp(distance, Mindistance, Maxdistance);
		}

		if (DeltaPaneo2Dedos.magnitude>0){
			DeltaPaneo2Dedos*=(1-4*Time.deltaTime);
			Paneo2Dedos+=DeltaPaneo2Dedos;
			Paneo2Dedos=new Vector2(Mathf.Clamp(Paneo2Dedos.x,-MaxDeltax,MaxDeltax),
			                        Mathf.Clamp(Paneo2Dedos.y,-MaxDeltay,MaxDeltay));
		}

		//if (FactorPaneo2Dedos*Paneo2Dedos.x!=0 || FactorPaneo2Dedos*Paneo2Dedos.y!=0){

		if (IsInTransition){
			float distCovered = (Time.time - startTime)*SpeedTransition*Mathf.Sqrt(distance);
			float fracJourney = distCovered / Vector3.Distance(target.position,NextTarget.position) ;



			MyCam.transform.position = Vector3.Lerp(InitPosition,FinishPosition,fracJourney);

			/*if (fracJourney>=1f){
				IsInTransition=false;
				target=NextTarget;
			}*/

		}
		else{
			MyCam.transform.position = (Quaternion.Euler(y, x, 0)) * 
					new Vector3(FactorPaneo2Dedos*Paneo2Dedos.x,FactorPaneo2Dedos*Paneo2Dedos.y, -distance) + 
					target.position;//new Vector3(,0);
		}
		//}
	}

	static float ClampAngle(float angle, float min, float max) 
	{
		if (angle < -360)
		{
			angle += 360;
		}
		if (angle > 360)
		{
			angle -= 360;
		}
		return Mathf.Clamp(angle, min, max);
	}

	public void ChangeTarget(Transform NewTarget){
		if(NewTarget.position!=target.position){
			NextTarget=NewTarget;
			InitPosition=MyCam.transform.position;
			FinishPosition=MyCam.transform.position+(NextTarget.position-target.position);

			startTime=Time.time;
			IsInTransition=true;
		}
	}
	// Gestures Scripts

	private void panStateChangeHandler(object sender, EventArgs e)
	{

		/*
		 * _MyGesture: Variable del tipo Gesture que es casteada desde el objeto que envia el 
		 * evento. Con esto, se tiene acceso al punto previo que notifico el Gesture.
		 */ 
		Gesture _MyGesture=sender as Gesture;

		/*
		 * Decisiones en funcion del Gesture que ha sido reconocido
		 * 1 Dedo -> Rotacion alrededor del target de Camara
		 * 2 Dedos -> Paneo de Camara
		 */
		switch(_MyGesture.ActiveTouches.Count)
		{
		case 1:
			_Son2Dedos=false;
			Debug.Log("1 dedo!!!");
			//Paneo2Dedos.Set(0.0f,0.0f);
			break;
		case 2:
			_Son2Dedos=true;
			Debug.Log("2 dedos!!!");
			DeltaPaneo2Dedos+=_MyGesture.PreviousNormalizedScreenPosition-_MyGesture.NormalizedScreenPosition;
			break;
		default:
			//print ("Que chucha!!!");
			break;
		}

		switch (_MyGesture.State){
		case Gesture.GestureState.Began:
			Debug.Log("Empezo el paneo");
			break;
		case Gesture.GestureState.Ended:
			Debug.Log("Termino el Paneo");
			break;
		default :
			Debug.Log("El gesture esta siendo reconocido");
			if (_MyGesture.ScreenPosition.x!=null && !_Son2Dedos){
				//_TouchScreenPos=_MyGesture.ScreenPosition;
				_Speed=_MyGesture.ScreenPosition-_MyGesture.PreviousScreenPosition;
			}
			break;
		}

	}


	private void scaleStateChangeHandler(object sender, EventArgs e)
	{
		ScaleGesture _MyGesture=sender as ScaleGesture;

		float delta=_MyGesture.LocalDeltaScale;
		ZoomInertia=1-delta;

	}
	private void tappedHandler(object sender, EventArgs eventArgs)
	{
		var tap = sender as TapGesture;
		switch (tap.NumberOfTapsRequired)
		{
		case 1:
			// our single tap gesture
			//changeColor();
			break;
		case 2:
			Debug.Log ("DobleTap");
			Paneo2Dedos.Set(0,0);
			ZoomInertia=0;
			// our double tap gesture
			//breakObject();
			break;
		}
	}

	// Freno de las Inercias
	private void pressHandler(object sender, EventArgs e){
		Debug.Log("FRENO!!!");
		_Speed.Set(0,0);
		//Paneo2Dedos.Set(0,0);
		//ZoomInertia=0;
		//factorPaneo=0;
	}

}
