using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerExplanation : MonoBehaviour
{
	private Tower tower = null;

	public Text Name_Text;
	public Text Level_Text;
	public Text ATK_Text;
	public Text Speed_Text;
	public Text Range_Text;

	private string TowerName = "NULL";
	private float ATK = 0;
	private float Speed = 0;
	private int level = 0;
	private int Range = 0;
	private int Cost = 0;

	private int UpgradeCost = 100;
	private int SellCost = 0;

	private int x = 0;
	private int y = 0;

	public Text UpgradeText;
	public Text SellText;

	private void Start()
	{
		tower = GetComponentInParent<Tower>();
		SellCost = tower.Cost / 2;
	}

	private void Update()
	{
		TowerAbility();
		SetText();

		UpgradeText.text = "UpGrade\n" + UpgradeCost + " Gold";
		SellText.text = "Sell\n" + SellCost + " Gold";

		this.transform.rotation = Quaternion.identity;
	}
	public void SetText()
	{
		Name_Text.text = TowerName;
		Level_Text.text = "LEVEL : " + level;
		ATK_Text.text = "ATK : " + ATK;
		Speed_Text.text = "SPEED : " + Speed;
		Range_Text.text = "RANGE : " + Range;
	}

	public void TowerAbility()
	{
		TowerName = tower.TowerName;
		ATK = tower.m_ATK;
		Speed = tower.m_Speed;
		level = tower.m_level;
		Range = tower.m_Range;
		Cost = tower.Cost;
		x = (int)(tower.transform.position.x + 0.5f);
		y = (int)(tower.transform.position.y + 0.5f);
	}

	public void UpgrageClick()
	{
		if(GameManager.Getinstance().Gold >= UpgradeCost)
		{
			tower.m_level++;
			tower.m_ATK += 5;
			GameManager.Getinstance().Gold -= UpgradeCost;
			SellCost += UpgradeCost / 2;
			UpgradeCost *= 2;
		}
	}

	public void SellClick()
	{
		Destroy(tower.gameObject);
		GameManager.Getinstance().Gold += SellCost;
		FindObjectOfType<TowerMakeClick>().SetMakeArea(x, y);
	}
}
