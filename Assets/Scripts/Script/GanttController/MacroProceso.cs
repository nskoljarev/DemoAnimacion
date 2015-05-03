using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MacroProceso : MonoBehaviour,IListener
{ 
	

	//public DateTime InitTimeGeneral, CurrentTimeGeneral, FinishTimeGeneral; //tiempo de inicio, debe ser definido en mes/dia/hora/minuto/seg?
	public int InitTimeGeneral, CurrentTimeGeneral, FinishTimeGeneral; //tiempo de inicio, debe ser definido en mes/dia/hora/minuto/seg?

	public float progreso;


	bool flagSecuenciaTerminada;//Define el status del Macroproceso, proceso o tarea
	string [] procesos;
	string [] tareas;

	// hacer listas
	//List <Proceso>  Procesos;

	Slider sliderTiempo;




	//Macro Procesos   
	/*
	 * Por ejemplo: 
	 * LLenado de sal, 
	 * Filtrado, 
	 * Sistema de energia 
	 * etc.
	 * */


	// Use this for initialization
	void Start () 
	{
		EventManager.Instance.AddListener(EVENT_TYPE.CambioFecha, this);
	}
	
	// Update is called once per frame
	void Update () 
	{
		DetectarSiEstaActivo();
	}

	//Si fechaactual es igual a fecha activo levantar flag
	void DetectarSiEstaActivo()
	{

	}
	public void actualizarFecha()
	{
		CurrentTimeGeneral = (int)sliderTiempo.value;
	}
	//Called when events happen
	public void OnEvent(EVENT_TYPE Event_Type, Component Sender, object Param = null)
	{
		if(Event_Type == EVENT_TYPE.CambioFecha)
		{
			CurrentTimeGeneral = (int)Param;

			//si no tiene tarea particulares


			
		}
	}


}
