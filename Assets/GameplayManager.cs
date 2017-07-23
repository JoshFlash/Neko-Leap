using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameplayManager : MonoBehaviour 
{

	Camera main;

	private void Awake()
	{
		main = Camera.main;
	}

	private void Start()
	{
		StartCoroutine(StartLevel(3,main));
	}

	public static IEnumerator StartLevel(float delay, Camera cam)
	{
		yield return new WaitForSeconds(delay);
		cam.GetComponent<CameraController>().enabled = true;
	}

	public static IEnumerator ResetLevel(float delay, int sceneIndex)
	{
		yield return new WaitForSeconds(delay);
		SceneManager.LoadScene(sceneIndex);
	}
	
}
