using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lethal : MonoBehaviour 
{

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag("Neko"))
		{
			col.GetComponent<CatController>().GetKilled(); ;
		}
	}

}
