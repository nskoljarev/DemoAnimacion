using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


public class ControladorPrincipal : MonoBehaviour 
{

	/*Eventos:
	  * 
	  *Etapas
	  *Macroprocesos
	  *Procesos
	  *Tareas
	  *Animaciones 
	  * 
	  */


	//Aca se maneja el tiempo general de la aplicacion
	//public DateTime InitTimeGeneral, CurrentTimeGeneral, FinishTimeGeneral;
	    //secuenciaProcesoGeneral

	// Los spot se agregan en el inspector
	public GameObject [] objetosSpots; // lugares de visualizacion donde se desarrollan animaciones
	public List<Spot> spots = new List<Spot>();
	public int InitTimeGeneral, FinishTimeGeneral;
	public int CurrentTimeGeneral;


	//private int _currentTimeGeneral;
//	public int CurrentTimeGeneral
//	{
//		get{return _currentTimeGeneral;}
//		set
//		{
//			_currentTimeGeneral =  (int)sliderTiempo.value;
//			//EventManager.Instance.PostNotification(EVENT_TYPE.CambioFecha, this, _currentTimeGeneral);
//
//		}
//	}
	//MacroProceso [] ListaMacroprocesos;
	Slider sliderTiempo;


	public Dictionary<string, Proceso> DicProcesos = new Dictionary<string, Proceso>();


	/* La activacion de cada uno de estos eventos depende del tiempo  y de donde uno se encuentre en la aplicacion
	 * Deben existir flag que indiquen todos los lugares posibles(Los llamaremos spots) 
	 * Spots posibles: Vista general
	 */  
	Proceso QuitarValvulasTiein1;
	Proceso MovimientoDeTierrasTiein1;
	Proceso ArmadoTiein1;
	
	// Llenar diccionario de procesos del spot
	public bool prueba;


	void Awake()
	{
		sliderTiempo = GameObject.Find("SliderTiempo").GetComponent<Slider>();



		/* Crear instancias Procesos
		QuitarValvulasTiein1 =  new Proceso("QuitarValvulasTie-in1",4,8);
		MovimientoDeTierrasTiein1 =  new Proceso("MovimientoDeTierrasTie-in1",10,20);
		ArmadoTiein1 =  new Proceso("ArmadoTiein1",40,50);


		DicProcesos.Add("QuitarValvulasTie-in1", QuitarValvulasTiein1);
		DicProcesos.Add("MovimientoDeTierrasTie-in1", MovimientoDeTierrasTiein1);
		DicProcesos.Add("ArmadoTiein1", ArmadoTiein1);
		*/

		//Agarrar todos los spots()
		objetosSpots = GameObject.FindGameObjectsWithTag("spot");

		foreach(GameObject S in objetosSpots)
		{
			spots.Add(S.GetComponent<Spot>());
		}



	
	}

	// Use this for initialization
	void Start () 
	{
		prueba = false;
		//Instanciar procesos
		


	}
	
	// Update is called once per frame
	void Update () 
	{
		if(prueba)
		{
			foreach (string key in DicProcesos.Keys)
			{
				print (key);
			}
			prueba = false;
		}
	}

	public void CambioBarraDeTiempo()
	{
		//print(sliderTiempo.value);
		//Actualizar progreso de cada spot y actualizar fecha de cada proceso
		CurrentTimeGeneral = (int)sliderTiempo.value;
		EventManager.Instance.PostNotification(EVENT_TYPE.CambioFecha, this, (int)CurrentTimeGeneral);

	}


}
