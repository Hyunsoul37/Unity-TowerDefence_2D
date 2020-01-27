using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerViewer : MonoBehaviour
{
	public GameObject TowerA = null;
	public GameObject TowerB = null;

	private void Start()
	{
		Vector3 BasicPos = new Vector3(7f, 0f);
		Instantiate(TowerA, BasicPos, Quaternion.identity, this.transform);
		Instantiate(TowerB, BasicPos + new Vector3(1f, 0), Quaternion.identity, this.transform);
	}
}
