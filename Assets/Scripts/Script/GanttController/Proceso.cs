using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;


public class Proceso : MacroProceso, IListener 
{

	/*
	 * Por ejemplo: 
	 * Construir Planta Tratamiento Sal, 
	 * Modificar proceso filtrado, 
	 * Cambiar sistema de alimentacin, 
	 * 
	 */			
	public string nombreProceso;
	string spotAlQuePertenece;
	string descricionProceso; 
	float costos;
	string descripcionDelproceso;
	bool estaActivo;

	Tarea [] tareasProceso;

	//List <Tarea> tareas;

	//float[] deltaTiempoTarea; //podria ser arreglo multidimensional(revisar en funcion de facilidad de acceso)

	


	#region Constructores

	//Constructor de la clase

	public Proceso()
	{

	}

	public Proceso(int tiempoInicioProceso,int tiempoFinalProceso)
	{
		InitTimeGeneral =  tiempoInicioProceso;
		FinishTimeGeneral = tiempoFinalProceso;

	}  

	public Proceso(string nombre,int tiempoInicioProceso,int tiempoFinalProceso)
	{
		InitTimeGeneral =  tiempoInicioProceso;
		FinishTimeGeneral = tiempoFinalProceso;
		nombreProceso = nombre;
		//Esto es un prueba, tampoco ya que no corria el start
		EventManager.Instance.AddListener(EVENT_TYPE.CambioFecha, this);
		//print(nombre);

	}  
	
	#endregion

	//Variables de prueba
	//TimerTemporal timerTemporal; 

	void Awake()
	{
		tareasProceso = this.gameObject.GetComponentsInChildren<Tarea>();
	}

	// Use this for initialization


	void Start () 
	{
		EventManager.Instance.AddListener(EVENT_TYPE.CambioFecha, this);
		//print ("Pase por aqui"+ nombreProceso);
	}
	
	// Update is called once per frame
	void Update () 
	{

		/*
		 * Asi sucesivamente por las x tareas que contenga un proceso
		 * 
		 * */

	}
	public void OnEvent(EVENT_TYPE Event_Type, Component Sender, object Param = null)
	{
		if(Event_Type == EVENT_TYPE.CambioFecha)
		{
			CurrentTimeGeneral = (int)Param;
			//print (nombreProceso + ":" + CurrentTimeGeneral);

			revisarActivo();

			//si es que no tiene tareas agregados
			calculoProgreso();
			
			print(nombreProceso + ":" + CurrentTimeGeneral + ":" + progreso);
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