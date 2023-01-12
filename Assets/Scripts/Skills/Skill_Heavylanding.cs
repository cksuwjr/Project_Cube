using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Heavylanding : Skill
{
	protected override IEnumerator Cast_()
	{
		if (!controller.isGround)
		{
			float Airbornforce = 100 * transform.position.y;

			GetComponent<Rigidbody>().MovePosition(new Vector3(transform.position.x, 1, transform.position.z));
			GameObject[] enemys;
			if (gameObject.layer == LayerMask.NameToLayer("Player"))
				enemys = GameObject.FindGameObjectsWithTag("Enemy");
			else
				enemys = GameObject.FindGameObjectsWithTag("Player");
			for (int i = 0; i < enemys.Length; i++)
			{
				CubeController enemycontroller = enemys[i].GetComponent<CubeController>();
				if (enemycontroller.isGround)
				{
					enemycontroller.AirBorned(Airbornforce);
					enemycontroller.GetDamage(1, gameObject);
				}
			}
		}
		yield return null;
	}
}
