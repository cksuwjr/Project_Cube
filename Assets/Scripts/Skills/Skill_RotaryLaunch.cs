using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_RotaryLaunch : Skill
{
	[SerializeField] private GameObject Bullet;
	protected override IEnumerator Cast_()
	{
		float timer = 0f;
		int count = 0;
		while (timer < 2.5f)
		{
			if (GetComponent<PlayerMovement>() && (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0))
				StopCoroutine("Cast_");
			if (count++ > 2)
			{
				controller.Attack(new Vector3(6f, 1, 6f), 25f, "Middle");
				count = 0;
			}
			transform.Rotate(new Vector3(0, 225, 0));
			timer += 0.05f;
			yield return new WaitForSeconds(0.05f);
			Instantiate(Bullet, transform.position, transform.rotation).GetComponent<Bullet>().Tang(gameObject, GetComponent<Status>().AttackPower * (Random.Range(0.185f, 0.325f)), 45f, 0.15f);
		}
		transform.rotation = Quaternion.Euler(Vector3.zero);
	}
}
