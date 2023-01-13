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
			yield return new WaitForSeconds(0.05f);
			controller.Attack(new Vector3(15, 1, 15), 1, "Middle", "AirBorned", Airbornforce, "Yes");

			/*
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
					enemycontroller.GetComponent<Rigidbody>().velocity = Vector3.zero;
					enemycontroller.AirBorned(Airbornforce);
					enemycontroller.StartCoroutine(enemycontroller.CC(0.004f * Airbornforce));
				}
			}
			*/
		}
		yield return null;
	}
}
