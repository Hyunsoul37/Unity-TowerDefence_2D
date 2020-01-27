using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public Vector3 targetPos = Vector3.zero;
	public float bullet_ATK = 0;

	private void Update()
	{
		if(this.GetComponentInParent<Tower>().gameObject.CompareTag("TowerA"))
		{
			transform.Translate(targetPos * Time.deltaTime * 5.5f);

			float distance = Vector3.Distance(this.transform.position, this.transform.parent.position);

			if (distance >= 2.25f)
			{
				Destroy(this.gameObject);
			}
		}
		else if(this.GetComponentInParent<Tower>().gameObject.CompareTag("TowerB"))
		{
			Destroy(this.gameObject, 1f);
		}

	}

	private void OnTriggerEnter2D(Collider2D obj)
	{
		if (obj.gameObject.CompareTag("Monster") && this.GetComponentInParent<Tower>().gameObject.CompareTag("TowerA"))
			Destroy(this.gameObject);
	}
}
