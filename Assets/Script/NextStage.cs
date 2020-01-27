using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NextStage : MonoBehaviour
{
	public Button NextStageButton = null;
	public Button FinishButton = null;

	public SpriteRenderer Monster_image= null;

	public Text Monster_Name = null;
	public Text Monster_HP = null;

	private void Update()
	{
		Monster_Name.text = "Monster Name : Yellow Monster";
		Monster_HP.text = "Monster HP : " + (GameManager.Getinstance().Monster_HP + 10);
	}

	public void ClickNextButton()
	{
		Debug.Log("ClickNextButton");
		GameManager.Getinstance().b_NextStage = false;
	}

	public void ClickFinishButton()
	{
		PlayerPrefs.SetInt("CurrentScore", GameManager.Getinstance().Score);
		SceneManager.LoadScene("ScoreboardScene");
	}
}
