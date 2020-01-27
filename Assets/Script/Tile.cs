using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
	public int m_X, m_Y, m_Type;
	public SpriteRenderer m_renderer;

	private void Awake()
	{
		m_renderer = GetComponent<SpriteRenderer>();
	}

	public void Setter(int x, int y, int type)
	{
		m_X = x;
		m_Y = y;
		m_Type = type;
	}

	public int GetTileType()
	{
		return m_Type;
	}

	public void SetTileImage(Sprite _sprite)
	{
		m_renderer.sprite = _sprite;
	}
}
