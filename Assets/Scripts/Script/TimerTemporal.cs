using UnityEngine;
using System.Collections;

public class TimerTemporal : MonoBehaviour {

	public float tiempo; 
	public bool play, reset, stop;

	// Use this for initialization
	void Start () 
	{
		play =  false;
		stop =  false;
		reset = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if(stop)
		{
			play = false; reset = false; stop =  false;
			
		}else if(reset)
		{
			play = false; stop = false; reset =  false;
			tiempo = 0; 

		}else if(play)
		{
			reset = false ; stop = false; 
			tiempo =  tiempo + Time.deltaTime;
		
		}

		//print (tiempo);
	
	}
}
