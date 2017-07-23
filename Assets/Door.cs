using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour 
{

	public GameObject goodJob;
	bool levelComplete = false;

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (levelComplete) return;
		if (col.CompareTag("Neko"))
		{
			levelComplete = true;
			goodJob.SetActive(true);
			goodJob.transform.position = Vector3.zero;
			StartCoroutine(GameplayManager.ResetLevel(7, 0));
		}
	}

	private void Update()
	{
		if (levelComplete)
		{
			Vector3 target = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, -4);
			goodJob.transform.position = 
				Vector3.Lerp(goodJob.transform.position, target, Time.deltaTime);
		}
	}

}
