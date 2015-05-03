using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

	int dias, horas, minutos, segundos;

	 public bool startTimer;

	public int playTime = 0;

	int [] multiplicador ;
	//public enum velocidadReproduccion{ x ,2x, 4x, 8x, 16x,32x}



	// Use this for initialization
	void Start () 
	{
		dias =  0;
		horas =  0;
		minutos =  0;
		segundos =  0;
		startTimer =  false;
		multiplicador = new int[] {2,4,8,16,32};
		StartCoroutine("PlayTimer");
	}
	
	// Update is called once per frame
	void Update () 
	{
	
		print ("PlayTime = " + dias.ToString()+ "dias:" + horas.ToString()+ "horas:" + minutos.ToString() + "Minutos:" + segundos.ToString() + "segundos");
	}
	void Awake() 
	{
	
		
	}
	
	private IEnumerator PlayTimer()
	{
		while(true)
		{
		yield return new WaitForSeconds(1);

		playTime += 1 * multiplicador[3]; // Ejemplo
		segundos = playTime % 60;
		minutos = (playTime / 60) % 60;
		horas =  (playTime / 3600) % 24;
		dias =  (playTime / 86400) % 365;
		}
	}
}
