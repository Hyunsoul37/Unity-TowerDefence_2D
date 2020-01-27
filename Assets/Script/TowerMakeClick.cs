using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerMakeClick : MonoBehaviour
{
	private bool isSelected = false;

	private int SelectTowerPrice;
	private int CurrentGold;

	public GameObject Green = null;
	public GameObject Red = null;

	public GameObject SelectTower = null;
	public GameObject Area = null;
	private GameObject CreateArea = null;
	public GameObject Alpha = null;
	private GameObject CreateAlpha = null;

	private int Max_X;
	private int Max_Y;

	private int[][] MakeArea;

	private Dictionary<Vector3, GameObject> CheckMaker = new Dictionary<Vector3, GameObject>();

	private void Start()
	{
		isSelected = false;
		MakeArea = FindObjectOfType<MakeMap>().MapTypes;
		Green.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 100f / 255f);
		Red.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 100f / 255f);

		Max_X = GameManager.Getinstance().Max_X;
		Max_Y = GameManager.Getinstance().Max_Y;

		

		SelectTowerPrice = SelectTower.GetComponent<Tower>().Cost;
	}

	private void Update()
	{
		CurrentGold = GameManager.Getinstance().Gold;

		if(isSelected == false)
		{
			InitCheck();
		}
	}

	private void OnMouseDown()
	{
		isSelected = true;
		SetMark();

		CreateAlpha = Instantiate(Alpha, this.transform);
		CreateArea = Instantiate(Area, this.transform);
		Vector3 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		MousePos.z = 1f;
		CreateAlpha.transform.position = MousePos;
		CreateArea.transform.position = MousePos;
	}

	private void OnMouseDrag()
	{
		if(isSelected)
		{
			InitCheck();

			Vector3 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			MousePos.z = 1f;
			CreateAlpha.transform.position = MousePos;
			CreateArea.transform.position = MousePos;

			float x = CreateAlpha.transform.position.x + 0.5f;
			float y = CreateAlpha.transform.position.y + 0.5f;

			Vector3 towerPos = new Vector3((int)x, (int)y);

			CheckMaker[towerPos].SetActive(true);
		}
	}

	private void OnMouseUp()
	{
		isSelected = false;

		float x = CreateAlpha.transform.position.x + 0.5f;
		float y = CreateAlpha.transform.position.y + 0.5f;

		Vector3 towerPos = new Vector3((int)x, (int)y);

		if(CheckMaker[towerPos].CompareTag("GreenArea") && CurrentGold >= SelectTowerPrice)
		{
			Instantiate(SelectTower, towerPos, Quaternion.identity, transform);
			GameManager.Getinstance().Gold -= SelectTowerPrice;
		}

		MakeArea[(int)x][(int)y] = -1;

		Destroy(CreateAlpha);
		Destroy(CreateArea);
		foreach (KeyValuePair<Vector3, GameObject> tmp in CheckMaker)
		{
			Destroy(tmp.Value);
		}
		CheckMaker.Clear();
	}

	private void SetMark()
	{
		for (int y = 0; y < Max_Y; y++)
		{
			for (int x = 0; x < Max_X; x++)
			{
				if (MakeArea[x][y] == 0)
				{
					GameObject obj = Instantiate(Green, new Vector3(x, y, 0), Quaternion.identity);
					Vector3 V = new Vector3((int)x, (int)y);
					obj.SetActive(false);

					CheckMaker.Add(V, obj);
				}
				else
				{
					GameObject obj = Instantiate(Red, new Vector3(x, y, 0), Quaternion.identity);
					Vector3 V = new Vector3((int)x, (int)y);
					obj.SetActive(false);

					CheckMaker.Add(V, obj);
				}
			}
		}
	}

	private void InitCheck()
	{
		foreach (KeyValuePair<Vector3, GameObject> tmp in CheckMaker)
		{
			tmp.Value.SetActive(false);
		}
	}

	public void SetMakeArea(int _x, int _y)
	{
		MakeArea[_x][_y] = 0;
	}
}
