//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
////-----------------------------------------------------------
////Enum defining all possible game events
////More events should be added to the list
//public enum EVENT_TYPE {GAME_INIT,GAME_END,AMMO_CHANGE,HEALTH_CHANGE,DEAD};
////-----------------------------------------------------------
////Singleton EventManager to send events to listeners
////Works with delegate implementations
//public class EventManagerD : MonoBehaviour
//{
//
//	#region C# properties
//	//-----------------------------------------------------------
//	//Public access to instance
//	public static EventManager Instance
//	{
//		get{return instance;}
//		set{}
//	}
//	#endregion
//
//	#region variables
//	//Notifications Manager instance (singleton design pattern)
//	private static EventManager instance = null;
//
//	// Declare a delegate type for events
//	public delegate void OnEvent(EVENT_TYPE Event_Type,Component Sender, object Param = null);
//
//	//Array of listener objects
//	private Dictionary<EVENT_TYPE, List<OnEvent>> Listeners = new Dictionary<EVENT_TYPE, List<OnEvent>>();
//	#endregion
//	//-----------------------------------------------------------
//	#region methods
//	//Called at start-up to initialize
//	void Awake()
//	{
//	//If no instance exists, then assign this instance
//		if(instance == null)
//		{
//			instance = this;
//			DontDestroyOnLoad(gameObject);
//		}
//		else
//			DestroyImmediate(this);
//	}
//	//-----------------------------------------------------------
//	/// <summary>
//	/// Add listener-object to array of listeners
//	/// </summary>
//	/// <param name="Event_Type">Event to Listen for</param>
//	/// <param name="Listener">Object to listen forevent</param>
//	public void AddListener(EVENT_TYPE Event_Type, OnEvent Listener)
//	{
//	//List of listeners for this event
//		List<OnEvent> ListenList = null;
//
//		// Check existing event. If one exists, add to list
//		if(Listeners.TryGetValue(Event_Type, out ListenList))
//		{
//			//List exists, so add new item
//			ListenList.Add(Listener);
//			return;
//		}
//
//		//Otherwise create new list as dictionary key
//		ListenList = new List<OnEvent>();
//		ListenList.Add(Listener);
//		Listeners.Add(Event_Type, ListenList);
//	}
//	//------------------------------------------------------
//	/// <summary>
//	/// Function to post event to listeners
//	/// </summary>
//	/// <param name="Event_Type">Event to invoke</param>
//	/// <param name="Sender">Object invoking event</param>
//	/// <param name="Param">Optional argument</param>
//	public void PostNotification(EVENT_TYPE Event_Type, Component Sender, object Param = null)
//	{
//		//Notify all listeners of an event
//	
//		//List of listeners for this event only
//		 List<OnEvent> ListenList = null;
//
//		//If no entry exists, then exit
//		if(!Listeners.TryGetValue(Event_Type, out ListenList))
//		return;
//
//		//Entry exists. Now notify appropriate listeners
//		for(int i=0; i<ListenList.Count; i++)
//		{
//			if(!ListenList[i].Equals(null))
//				ListenList[i](Event_Type, Sender, Param);
//		}
//	}
//	//-----------------------------------------------------------
//	//Remove event from dictionary, including all listeners
//	public void RemoveEvent(EVENT_TYPE Event_Type)
//	{
//	//Remove entry from dictionary
//		Listeners.Remove(Event_Type);
//	}
//	//-----------------------------------------------------------
//	//Remove all redundant entries from the Dictionary
//	public void RemoveRedundancies()
//	{
//		//Create new dictionary
//		Dictionary<EVENT_TYPE, List<OnEvent>> TmpListeners		= new Dictionary<EVENT_TYPE, List<OnEvent>>();
//		//Cycle through all dictionary entries
//		foreach(KeyValuePair<EVENT_TYPE, List<OnEvent>>Item in Listeners)
//		{
//			//Cycle through all listeners
//			for(int i = Item.Value.Count-1; i>=0; i--)
//			{
//				//If null, then remove item
//				if(Item.Value[i].Equals(null))
//					Item.Value.RemoveAt(i);
//			}
//
//			//If items remain, then add to tmp dictionary
//			if(Item.Value.Count > 0)
//				TmpListeners.Add (Item.Key, Item.Value);
//		}
//
//		//Replace listeners with new dictionary
//		Listeners = TmpListeners;
//	}
//	//-----------------------------------------------------------
//	//Called on scene change. Clean up dictionary
//	void OnLevelWasLoaded()
//	{
//		RemoveRedundancies();
//	}
//	//-----------------------------------------------------------
//	#endregion
//}
