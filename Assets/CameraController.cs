using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour 
{
	public Vector3 yMovement = new Vector3(0, 0.1f, 0);
	public GameObject target;

	private void Update()
	{
		transform.position += yMovement * Time.deltaTime;
		if (target.transform.position.y > transform.position.y)
		{
			transform.position = Vector3.Lerp
				(transform.position, new Vector3(transform.position.x, target.transform.position.y, transform.position.z), Time.deltaTime);
		}
	}

}
