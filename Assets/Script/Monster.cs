using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
   enum Direction
	{
		Back = 0,
		Right = 1,
		Left = 2,
		Front = 3
	}

	public GameObject HP_Bar;

	public MakeMap map;
	public Animator m_MonsterAnim;

	private Vector3 CurrentPos;
	private Vector3 TargetPos;

	private int m_X = 2;
	private int m_Y = 1;
	private int nCount = 0;

	private List<int> Road = new List<int>();

	private float time = 0f;
	public float MoveSpeed = 0.1f;

	private float HP;
	public float Max_HP;

	private void Awake()
	{
		m_MonsterAnim = GetComponent<Animator>();
		map = FindObjectOfType<MakeMap>();
		CurrentPos = new Vector3(m_X, m_Y);

		Road = map.RoadMap;

		HP = GameManager.Getinstance().Monster_HP;
		Max_HP = GameManager.Getinstance().Monster_HP;

		SetHP_Bar();
	}

	private void Update()
	{
		time += Time.deltaTime * 1f;
		CurrentPos = new Vector3(m_X, m_Y);
		Check();

		
	}

	private void Check()
	{
		if(Road[nCount] == (int)Direction.Back)
		{
			m_MonsterAnim.SetInteger("Direction", (int) Direction.Back);
			TargetPos = new Vector3(m_X, m_Y + 1);

		}
		else if(Road[nCount] == (int)Direction.Front)
		{
			m_MonsterAnim.SetInteger("Direction", (int) Direction.Front);
			TargetPos = new Vector3(m_X, m_Y - 1);

		}
		else if(Road[nCount] == (int)Direction.Left)
		{
			m_MonsterAnim.SetInteger("Direction", (int) Direction.Left);
			TargetPos = new Vector3(m_X - 1, m_Y);

		}
		else if(Road[nCount] == (int)Direction.Right)
		{
			m_MonsterAnim.SetInteger("Direction", (int) Direction.Right);
			TargetPos = new Vector3(m_X + 1, m_Y);
		
		}

		MonsterMove();

		if (this.transform.position.x == 16 && this.transform.position.y == 9)
		{
			Destroy(this.gameObject);
			GameManager.Getinstance().Count_Life--;
		}
	}

	public void MonsterMove()
	{
		float x = Mathf.Lerp(CurrentPos.x, TargetPos.x, time);
		float y = Mathf.Lerp(CurrentPos.y, TargetPos.y, time);

		if(x == TargetPos.x && y == TargetPos.y)
		{
			m_X = (int)TargetPos.x;
			m_Y = (int)TargetPos.y;
			nCount++;
		}

		if (time >= 1f)
		{
			time = 0f;
		}

		this.transform.position = new Vector3(x, y);
	}

	private void OnTriggerEnter2D(Collider2D obj)
	{
		if (obj.gameObject.CompareTag("Bullet"))
		{
			HP -= obj.GetComponent<Bullet>().bullet_ATK;
			float Persent = HP / Max_HP;

			HP_Bar.transform.localScale = new Vector3(HP_Bar.transform.localScale.x * Persent, HP_Bar.transform.localScale.y);

			if (HP <= 0.0f)
			{
				Destroy(this.gameObject);
				GameManager.Getinstance().Score += 100;
			}
		}
	}

	private void SetHP_Bar()
	{
		HP_Bar = Instantiate(HP_Bar, this.transform);

		HP_Bar.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.5f);
	}
}
