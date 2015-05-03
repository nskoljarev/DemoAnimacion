﻿using UnityEngine;
using System.Collections;
//-----------------------------------------------------------
//Enum defining all possible game events
//More events should be added to the list
public enum EVENT_TYPE {AppIniciada,AppTerminada, CambioFecha, SpotClickeado,ProcesoSeleccionado};
//-----------------------------------------------------------
//Listener interface to be implemented on Listener classes
public interface IListener
{
//Notification function invoked when events happen
void OnEvent(EVENT_TYPE Event_Type, Component Sender, object Param = null);
}