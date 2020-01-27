using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	private static GameManager instance;

	public static GameManager Getinstance()
	{
		if(instance == null)
		{
			instance = FindObjectOfType<GameManager>();
			if(instance == null)
			{
				Debug.LogError("There Needs to one active GameManager script a GameObject in your Scene");
			}

			return instance;
		}
		
		return instance;
	}

	public int Max_X = 18;
	public int Max_Y = 10;

	public float Monster_HP = 0f;

	public bool isStart = false;
	public bool isSpawn = true;

	public int Score = 0;
	public int Gold = 1000;

	public GameObject NextStage = null;
	public bool b_NextStage = true;
	public bool isShow = false;

	public int Count_Life = 5;

	private void Start()
	{
		StartCoroutine(GoldPerSec());
		NextStage = Instantiate(NextStage, this.transform);
		NextStage.SetActive(b_NextStage);
	}

	private void Update()
	{ 
		if(isSpawn == false && Clear() == 0)
		{
			isStart = false;
		}

		ResetTower();
		ShowNextStage();
		GameOver();
	}

	IEnumerator GoldPerSec()
	{
		while(true)
		{
			if (isStart)
				Gold += 3;

			yield return new WaitForSeconds(1.0f);
		}
	}

	private int Clear()
	{
		GameObject[] monsters;
		int mon = 0;

		monsters = FindObjectsOfType<GameObject>();

		for(int i = 0; i < monsters.Length; i++)
		{
			if (monsters[i].CompareTag("Monster"))
				mon++;
		}

		return mon;
	}

	public void ResetTower()
	{
		Tower[] Towers = FindObjectsOfType<Tower>();
		if(isStart == true)
		{
			foreach (Tower obj in Towers)
			{
				obj.Explanation.SetActive(false);
			}
		}
		else
		{
			foreach (Tower obj in Towers)
			{
				Quaternion q = Quaternion.Euler(new Vector3(0, 0, 0));
				obj.transform.rotation = Quaternion.Slerp(obj.transform.rotation, q, 0.2f);
			}
		}
	}

	private void ShowNextStage()
	{
		if(isStart == false)
		{
			isShow = true;
			if(isShow == true)
			{
				NextStage.SetActive(b_NextStage);
			}
		}
		else
		{
			b_NextStage = true;
			isShow = false;
		}

	}

	private void GameOver()
	{
		if(Count_Life <= 0)
		{
			PlayerPrefs.SetInt("CurrentScore", Score);
			SceneManager.LoadScene("GameOverScene");
		}
	}
}
