using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
	public GameObject MonsterPrfab;
	public int Count = 10;

	private bool b_isStart = false;
	private bool b_isSpawn = true;

	private void Start()
	{
		StartCoroutine(Spawn());
	}

	private void Update()
	{
		b_isStart = GameManager.Getinstance().isStart;
		b_isSpawn = GameManager.Getinstance().isSpawn;
	}

	IEnumerator Spawn()
	{
		while(true)
		{
			if(b_isStart && b_isSpawn)
			{
				for (int i = Count; i > 0; i--)
				{
					GameObject mon = Instantiate(MonsterPrfab) as GameObject;

					yield return new WaitForSeconds(1.0f);
				}

				GameManager.Getinstance().isSpawn = false;
			}

			yield return new WaitForSeconds(1.0f);
		}
	}
}
