using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
	public GameObject m_bullet = null;
	public GameObject Explanation = null;
	private List<GameObject> TargetList = new List<GameObject>();

	private bool isSelected = false;
	private float fTime = 0f;

	[HideInInspector] public string TowerName;
	[HideInInspector] public float m_ATK = 5f;
	[HideInInspector] public float m_Speed = 1f;
	[HideInInspector] public int m_level = 1;
	[HideInInspector] public int m_Range = 1;
	[HideInInspector] public int Cost = 100;

	private void Start()
	{
		Explanation = Instantiate(Explanation, this.transform.position, Quaternion.identity, this.transform) as GameObject;

		if (this.transform.position.x >= GameManager.Getinstance().Max_X - 1)
		{
			Explanation.transform.position += new Vector3(-1.2f, 0);
		}
		else
		{
			Explanation.transform.position += new Vector3(1.2f, 0);
		}

		if(this.transform.position.y >= GameManager.Getinstance().Max_Y - 2)
		{
			Explanation.transform.position += new Vector3(0, -1.2f);
		}
		else
		{
			Explanation.transform.position += new Vector3(0, 1.2f);
		}

		Explanation.SetActive(false);

		switch(this.gameObject.tag.ToString())
		{
			case "TowerA":
				TowerName = "Bullet Tower";
				break;
			case "TowerB":
				TowerName = "Lazer Tower";
				break;
		}
	}

	private void Update()
	{
		fTime += Time.deltaTime * 0.5f;
		CheckTargetList();

		switch (this.gameObject.tag.ToString())
		{
			case "TowerA":
				Attack_TowerA();
				break;
			case "TowerB":
				Attack_TowerB();
				break;
		}

		if (Input.GetMouseButtonDown(0))
			ClickTower();

		rotateTower();
	}

	public void Attack_TowerA()
	{
		if (TargetList.Count > 0)
		{
			GameObject Target = TargetList[0];
			
			if (Target != null && fTime >= 1.0f)
			{
				GameObject shootBullet = Instantiate(m_bullet, this.transform.position, Quaternion.identity, this.transform) as GameObject;
				Vector3 pos = Target.transform.position - transform.position;
				shootBullet.GetComponent<Bullet>().targetPos = pos;
				shootBullet.GetComponent<Bullet>().bullet_ATK = m_ATK;
				Debug.Log(m_ATK);
				fTime = 0f;
			}
		}
		else
		{
			fTime = 0f;
		}
	}

	public void Attack_TowerB()
	{
		if (TargetList.Count > 0)
		{
			GameObject Target = TargetList[0];
			Debug.Log(TargetList.Count);
			if (Target != null && fTime >= 1.0f)
			{
				Vector3 pos = Target.transform.position - transform.position;
				float angle = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg - 90f;

				GameObject shootBullet = Instantiate(m_bullet, this.transform.position, Quaternion.AngleAxis(angle, Vector3.forward), this.transform) as GameObject;

				shootBullet.GetComponent<Bullet>().targetPos = pos;
				shootBullet.GetComponent<Bullet>().bullet_ATK = m_ATK;
				fTime = 0f;
			}
		}
		else
		{
			fTime = 0f;
		}
	}

	private void ClickTower()
	{
		Vector3 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		Vector3 Mouse = new Vector3((int)(MousePos.x + 0.5f), (int)(MousePos.y + 0.5f), 0f);
		Vector3 Tower = new Vector3((int)(this.transform.position.x + 0.5f), (int)(this.transform.position.y + 0.5f), 0f);

		if (Mouse == Tower && GameManager.Getinstance().isStart == false)
		{
			isSelected = !isSelected;
			Explanation.SetActive(isSelected);
		}
	}

	private void rotateTower()
	{
		if (TargetList.Count > 0)
		{
			GameObject Target = TargetList[0];

			if (Target != null)
			{
				Vector3 Pos = Target.transform.position - this.transform.position;

				Vector3 offset = new Vector3(Pos.x, Pos.y);
				float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg - 90f;
				Quaternion q = Quaternion.Euler(new Vector3(0, 0, angle));
				this.transform.rotation = Quaternion.Slerp(this.transform.rotation, q, 0.2f);
			}

		}

	}

	private void CheckTargetList()
	{
		foreach (GameObject obj in TargetList)
		{
			if (obj == null)
			{
				TargetList.Remove(obj);
				break;
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D obj)
	{
		if (obj.gameObject.CompareTag("Monster"))
		{
			TargetList.Add(obj.gameObject);
		}
	}

	private void OnTriggerExit2D(Collider2D obj)
	{
		foreach (GameObject exitobj in TargetList)
		{
			if (exitobj == obj.gameObject)
			{
				TargetList.Remove(exitobj);
				break;
			}
		}
	}
}
