using UnityEngine;
using System.Collections;
using TouchScript.Gestures;
using System.Collections.Generic;
using UnityEngine.UI;

//Requiere un collider

public class Spot : MonoBehaviour, IListener 
{

//	Tiempo
//	public DateTime CurrentTimeGeneral;
//
//	//Definen los momentos en los que esta activo el spot 
//	public DateTime InitTimeUno, InitTimeDos, InitTimeTres;

	ControladorPrincipal controladorPrincipal;
	public GameObject objetoControladorPrincipal;

	// Deben estar declarados y distintos de 0.
	public int InitTimeGeneral, CurrentTimeGeneral, FinishTimeGeneral;
	public string nombreSpot;
	public float progreso;

	bool estaActivo =  false;
	bool tieneProcesos = false;

	//Se tiene que cambiar a DateTime

	Slider sliderTiempo;

	public Proceso [] procesosSpot;
	public List<Proceso> procesosSpotL;
	//public Dictionary<string, Proceso> DicProcesosSpot = new Dictionary <string, Proceso>();
	//variable para mostrar el progreso porcentual
	 



	public bool prueba ;

	#region OnEnableOnDisable
	private void OnEnable()
	{
		// subscribe to gesture's Tapped event
		GetComponent<TapGesture>().Tapped += tappedHandler;
	}
	
	private void OnDisable()
	{
		// don't forget to unsubscribe
		GetComponent<TapGesture>().Tapped -= tappedHandler;
	}
	#endregion

	void Awake()
	{
		//Agarrar todos los procesos del spot
		procesosSpot = this.gameObject.GetComponentsInChildren<Proceso>();

		if(procesosSpot != null) tieneProcesos = true;
		foreach(Proceso P in procesosSpot)
		{
			procesosSpotL.Add(P);
		}


		/*
		foreach(Proceso P in procesosSpot)
		{
			print (P.nombreProceso.ToString());
		}
		*/
	}

	// Use this for initialization
	void Start () 
	{
//		procesosSpot[0] =  "QuitarValvulasTie-in1";
//		procesosSpot[1] =  "MovimientoDeTierrasTie-in1";

		controladorPrincipal = objetoControladorPrincipal.GetComponent<ControladorPrincipal>();
		EventManager.Instance.AddListener(EVENT_TYPE.CambioFecha, this);
		//print(procesosSpot[0]);
		prueba = false;

	}
	
	// Update is called once per frame
	void Update () 
	{
		if(prueba)
		{
			prueba =  false;
			//asignarProcesosAlSpot();
		
		}
	}

	//Detectar spot clickeado
	private void tappedHandler(object sender, System.EventArgs e)
	{
		//Aqui se debe hacer el movimiento de camara y llamar al menu
		print("Click en spot");
	}
	/*
	//Asginar procesos al spot
	private void asignarProcesosAlSpot()
	{
		//asignar un proceso a su spot respectivo
		foreach(string P in procesosSpot)
		{
		
			DicProcesosSpot.Add(P, controladorPrincipal.DicProcesos[P]);

		}
		foreach (string key in DicProcesosSpot.Keys)
		{
			print (key);
		}
			
		//controladorPrincipal.DicProcesos[procesosSpot[0]];
	}
	*/



	public void OnEvent(EVENT_TYPE Event_Type, Component Sender, object Param = null)
	{
		if(Event_Type == EVENT_TYPE.CambioFecha)
		{
			CurrentTimeGeneral = (int)Param;

			float tempProgreso = 0;
			revisarActivo();

			if(tieneProcesos)
			{
				foreach (Proceso P in procesosSpotL)
				{
					 tempProgreso += P.progreso;
				}
				progreso = tempProgreso/procesosSpotL.Count;
			}else
			{
				calculoProgreso();
			}




			print(nombreSpot + ":" + CurrentTimeGeneral + ":" + progreso);
		}


	}


	//mandar a clase utilitaria(revisar)
	void calculoProgreso()
	{
		if(CurrentTimeGeneral >= FinishTimeGeneral)
		{
			progreso =  1;
		}else if(CurrentTimeGeneral <= InitTimeGeneral)
		{
			progreso =  0;
		}else
		{
			progreso = 1.0f * (CurrentTimeGeneral - InitTimeGeneral)/(FinishTimeGeneral - InitTimeGeneral);
			
		}
	}


	//mandar a clase utilitaria(revisar)
	void revisarActivo()
	{
		if(progreso > 0 && progreso < 1)
		{	
			estaActivo = true;
		}else 
			estaActivo =  false;
	}


}
