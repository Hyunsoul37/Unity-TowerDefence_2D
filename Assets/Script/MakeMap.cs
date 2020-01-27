using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeMap : MonoBehaviour
{
	public GameObject TilePrefab;
	public Sprite[] sprites;
	private Transform StartTranceform;

	public int[][] MapTypes;
	public List<int> RoadMap = new List<int>();
	public int Max_X;
	public int Max_Y;

	public Vector3 StartPos = new Vector3(2, 0);

	private void Awake()
	{
		Max_X = GameManager.Getinstance().Max_X;
		Max_Y = GameManager.Getinstance().Max_Y;

		MapTypes = new int[Max_X][];
		for (int i = 0; i < Max_X; i++)
		{
			MapTypes[i] = new int[Max_Y];
		}
		InitMapType();
		StartTranceform = GetComponent<Transform>();
		MakeTileMap();

		InitRoadMap();
	}

	public void MakeTileMap()
	{
		for (int y = 0; y < Max_Y; y++)
		{
			for (int x = 0; x < Max_X; x++)
			{
				GameObject obj = Instantiate(TilePrefab, new Vector3(x, y, 2), Quaternion.identity);
				obj.transform.SetParent(this.transform);

				int Type;

				if (MapTypes[x][y] == 1)
				{
					Type = 1;
					Tile tile = obj.GetComponent<Tile>();
					tile.Setter(x, y, Type);
					tile.SetTileImage(sprites[Type]);
					MapTypes[x][y] = Type;
					tile.gameObject.tag = "Road";
				}
				else if (y == 0)
				{
					Type = 2;
					Tile tile = obj.GetComponent<Tile>();
					tile.Setter(x, y, Type);
					tile.SetTileImage(sprites[Type]);
					MapTypes[x][y] = Type;
					tile.gameObject.tag = "SelectArea";
				}
				else
				{
					Type = 0;
					Tile tile = obj.GetComponent<Tile>();
					tile.Setter(x, y, Type);
					tile.SetTileImage(sprites[Type]);
					MapTypes[x][y] = Type;
					tile.gameObject.tag = "Grass";
				}
			}
		}
	}

	public void InitRoadMap()
	{
		RoadMap.Add(0);

		RoadMap.Add(1);
		RoadMap.Add(1);
		RoadMap.Add(1);
		RoadMap.Add(1);

		RoadMap.Add(0);
		RoadMap.Add(0);
		RoadMap.Add(0);

		RoadMap.Add(2);
		RoadMap.Add(2);
		RoadMap.Add(2);
		RoadMap.Add(2);

		RoadMap.Add(0);
		RoadMap.Add(0);
		RoadMap.Add(0);

		RoadMap.Add(1);
		RoadMap.Add(1);
		RoadMap.Add(1);
		RoadMap.Add(1);
		RoadMap.Add(1);
		RoadMap.Add(1);
		RoadMap.Add(1);
		RoadMap.Add(1);

		RoadMap.Add(3);
		RoadMap.Add(3);

		RoadMap.Add(1);
		RoadMap.Add(1);
		RoadMap.Add(1);

		RoadMap.Add(3);
		RoadMap.Add(3);
		RoadMap.Add(3);
		RoadMap.Add(3);
		RoadMap.Add(3);

		RoadMap.Add(1);
		RoadMap.Add(1);
		RoadMap.Add(1);

		RoadMap.Add(0);
		RoadMap.Add(0);
		RoadMap.Add(0);
		RoadMap.Add(0);
		RoadMap.Add(0);
		RoadMap.Add(0);
		RoadMap.Add(0);
		RoadMap.Add(0);
	}

	public void InitMapType()
	{
		MapTypes[2][1] = 1;
		MapTypes[2][2] = 1;

		MapTypes[3][2] = 1;
		MapTypes[4][2] = 1;
		MapTypes[5][2] = 1;
		MapTypes[6][2] = 1;

		MapTypes[6][3] = 1;
		MapTypes[6][4] = 1;
		MapTypes[6][5] = 1;

		MapTypes[5][5] = 1;
		MapTypes[4][5] = 1;
		MapTypes[3][5] = 1;
		MapTypes[2][5] = 1;

		MapTypes[2][6] = 1;
		MapTypes[2][7] = 1;
		MapTypes[2][8] = 1;

		MapTypes[2][8] = 1;
		MapTypes[3][8] = 1;
		MapTypes[4][8] = 1;
		MapTypes[5][8] = 1;
		MapTypes[6][8] = 1;
		MapTypes[7][8] = 1;
		MapTypes[8][8] = 1;
		MapTypes[9][8] = 1;
		MapTypes[10][8] = 1;

		MapTypes[10][7] = 1;
		MapTypes[10][6] = 1;

		MapTypes[11][6] = 1;
		MapTypes[12][6] = 1;
		MapTypes[13][6] = 1;

		MapTypes[13][5] = 1;
		MapTypes[13][4] = 1;
		MapTypes[13][3] = 1;
		MapTypes[13][2] = 1;
		MapTypes[13][1] = 1;

		MapTypes[13][1] = 1;
		MapTypes[14][1] = 1;
		MapTypes[15][1] = 1;
		MapTypes[16][1] = 1;

		MapTypes[16][2] = 1;
		MapTypes[16][3] = 1;
		MapTypes[16][4] = 1;
		MapTypes[16][5] = 1;
		MapTypes[16][6] = 1;
		MapTypes[16][7] = 1;
		MapTypes[16][8] = 1;
		MapTypes[16][9] = 1;
	}
}