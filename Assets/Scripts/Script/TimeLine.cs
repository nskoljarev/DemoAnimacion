using UnityEngine;
using System.Collections;
using System;

public class TimeLine : MonoBehaviour 
{
	public DateTime InitTime, CurrentTime, FinishTime;

	// Set resolution time in seconds (3600-> 1 hour)
	TimeSpan TimeResolution = new TimeSpan(1,0,0);
	TimeSpan DefaultTimeResolution;


	//Variables Daniel
	public bool pruebaTiempo;

	// Use this for initialization
	void Start () 
	{
		DefaultTimeResolution = new TimeSpan(TimeResolution.Ticks);
		InitTime = new DateTime(2015,2,15,0,0,0);
		CurrentTime = DateTime.Now;
		FinishTime = new DateTime(2016,4,20,0,0,0);


		//Variables Daniel
		//Falta un funcion para volver el tiempo al dia actual
		pruebaTiempo =  false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		print (CurrentTime);

		if(pruebaTiempo)
		{
			Play();
		}

	}

	public void Play()
	{
		InvokeRepeating("AddResolutionTimeToCurrentTime",0,1);
	}
	void AddResolutionTimeToCurrentTime()
	{
		CurrentTime += TimeResolution;
	}

	public void PlayReverse()
	{
		InvokeRepeating("SubtractResolutionTimeToCurrentTime",0,1);
	}
	void SubtractResolutionTimeToCurrentTime()
	{
		CurrentTime -= TimeResolution;
	}

	public void Pause()
	{	
		CancelInvoke();
	}

	public void FastForward(int multiplier)
	{
		TimeResolution = TimeSpan.FromTicks(TimeResolution.Ticks*multiplier);
	}

	public void NormalForward()
	{
		TimeResolution = DefaultTimeResolution;
	}

	public DateTime GetTime()
	{
		return CurrentTime;
	}

	public void ParseDateFromString(string StringToParse, DateTime datetoparsein)
	{
		datetoparsein = DateTime.ParseExact(StringToParse, "yyyy-MM-dd HH:mm",System.Globalization.CultureInfo.InvariantCulture);
	}

}
