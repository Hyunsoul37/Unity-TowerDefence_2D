using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
	public Text Score_Text;
	public Text Gold_Text;
	public Text Life_Text;

	public GameObject[] Life;
	public GameObject Origin;

	public Button StartButton;

	private void Start()
	{
		Life = new GameObject[5];

		InitLife();
	}

	private void Update()
	{
		Score_Text.text = "Score : " + GameManager.Getinstance().Score;
		Gold_Text.text = "Gold : " + GameManager.Getinstance().Gold;
		Life_Text.text = "Life ";

		SetLife();
	}

	public void CilckStartButton()
	{
		if(!GameManager.Getinstance().isStart && !GameManager.Getinstance().b_NextStage)
		{
			GameManager.Getinstance().isStart = true;
			GameManager.Getinstance().isSpawn = true;
			GameManager.Getinstance().Monster_HP += 10;
		}
	}

	private void InitLife()
	{
		Vector3 Pos = new Vector3(1f, 9f, -1f);

		for(int i = 0; i < GameManager.Getinstance().Count_Life; i++)
		{
			GameObject obj = Instantiate(Origin, Pos, Quaternion.identity);
			Life[i] = obj;
			Pos += new Vector3(0.5f, 0f);
		}
	}

	private void SetLife()
	{
		if(GameManager.Getinstance().Count_Life < Life.Length)
		{
			if (GameManager.Getinstance().Count_Life >= 0)
				Destroy(Life[GameManager.Getinstance().Count_Life]);
		}
	}
}
