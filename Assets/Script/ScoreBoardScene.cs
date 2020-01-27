using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreBoardScene : MonoBehaviour
{
	public Button GoMain_Button;
	public Button EndGame_Button;

	public Text[] Ranked_Text;

	public int CurrentScore = 0;
	public int[] Ranked_Score;

	private void Start()
	{
		Ranked_Score = new int[11];

		CurrentScore = PlayerPrefs.GetInt("CurrentScore");

		for(int i = 0; i <Ranked_Score.Length; i++)
		{
			Ranked_Score[i] = 0;
		}
		Rank_Score();

		StartCoroutine(BlinkCurrentScore());
	}

	private void Update()
	{
		SetText();
	}

	public void ClickGoMain()
	{
		SceneManager.LoadScene("MainScene");
	}

	public void ClickEndGame()
	{
		Application.Quit();
	}

	private void Rank_Score()
	{
		int current = PlayerPrefs.GetInt("CurrentScore");
		PlayerPrefs.SetInt("CurrentScore", 0);

		for(int i = 0; i < 10; i++)
		{
			Ranked_Score[i] = PlayerPrefs.GetInt("Top_" + i.ToString(), 0);
		}

		for(int i = 0; i <Ranked_Score.Length; i++)
		{
			if(Ranked_Score[i] < CurrentScore)
			{
				int tmp;
				tmp = Ranked_Score[i];
				Ranked_Score[i] = CurrentScore;
				CurrentScore = tmp;
			}

			PlayerPrefs.SetInt("Top_" + i.ToString(), Ranked_Score[i]);
		}
	}

	private void SetText()
	{
		for(int i = 0; i < Ranked_Text.Length; i++)
		{
			int Score = PlayerPrefs.GetInt("Top_" + i.ToString(), 0);

			if(i == 0)
				Ranked_Text[i].text = (i + 1).ToString() + "st" + " : " + Score;
			else if( i == 1)
				Ranked_Text[i].text = (i + 1).ToString() + "nd" + " : " + Score;
			else if(i == 2)
				Ranked_Text[i].text = (i + 1).ToString() + "rd" + " : " + Score;
			else
				Ranked_Text[i].text = (i + 1).ToString() + "th" + " : " + Score;
		}
	}

	IEnumerator BlinkCurrentScore()
	{
		int i;
		for(i = 0; i < Ranked_Score.Length; i++)
		{
			if (Ranked_Score[i] == CurrentScore)
				break;
		}

		while(true)
		{
			if(i - 1 >= 0 && i - 1 <= Ranked_Text.Length)
			{
				Ranked_Text[i - 1].color = new Color(0.4575472f, 0.749798f, 1f, 1f);
				yield return new WaitForSeconds(0.5f);
				Ranked_Text[i - 1].color = new Color(0f, 0f, 0f, 1f);
				yield return new WaitForSeconds(0.5f);
			}
			else
			{
				yield return new WaitForSeconds(1.0f);
			}
		}	
	}
}
