using UnityEngine;
using System.Collections;

public class TimeLineEditor : TimeLine {

	public string InitTimeString = "";
	public string FinishTimeString = "";
	TimeLine TimeLineScript;
	public AnimationState MyAnimation;


	//[RequireComponent (typeof (AnimationState))]
	// Use this for initialization
	void Start () 
	{
		TimeLineScript = FindObjectOfType<TimeLine>();
		ParseDateFromString(InitTimeString,InitTime);
		ParseDateFromString(FinishTimeString,FinishTime);
	}
	
	// Update is called once per frame
	void Update ()
	{
		CurrentTime = TimeLineScript.CurrentTime;

		if(CurrentTime > InitTime && CurrentTime < FinishTime)
		{
			double normalizedTime = CurrentTime.Subtract(InitTime).TotalHours/FinishTime.Subtract(InitTime).TotalHours;
			MyAnimation.normalizedTime = (float)normalizedTime;
		}
	}
}
