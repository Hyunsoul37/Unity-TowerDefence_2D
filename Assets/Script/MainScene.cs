using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScene : MonoBehaviour
{
	private void OnMouseDown()
	{
		Debug.Log("Clicked");
		SceneManager.LoadScene("GameScene");
	}
}
