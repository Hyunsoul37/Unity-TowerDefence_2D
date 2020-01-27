using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScene : MonoBehaviour
{
	public Button GoMainButton;
	public Button ScoreBoardButton;

	public void ClickGoMain()
	{
		SceneManager.LoadScene("MainScene");
	}

	public void ClickScoreBoard()
	{
		SceneManager.LoadScene("ScoreboardScene");
	}
}
