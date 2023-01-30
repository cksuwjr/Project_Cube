using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill_Craft : Skill
{
	[SerializeField] private GameObject Block;
	private void Awake()
	{
		inform.Add("Craft ");
		inform.Add("���濡 1 x 1 ũ���� ���� �����մϴ�. ���� ���� �ƴҶ��� ��밡���ϸ� ���� ��, �Ʊ� ������� ��� �Ұ��ϰ� �����ð��� 15�� �Դϴ�.\n\n��Ÿ�� : 5��");
		inform.Add("���濡 1 x 1 ũ���� ���� �����մϴ�. ���� ���� �ƴҶ��� ��밡���ϸ� ���� ��, �Ʊ� ������� ��� �Ұ��ϰ� �����ð��� 20�� �Դϴ�.\n\n��Ÿ�� : 5��");
		inform.Add("���濡 1 x 1 ũ���� ���� �����մϴ�. ���� ���� �ƴҶ��� ��밡���ϸ� ���� ��, �Ʊ� ������� ��� �Ұ��ϰ� �����ð��� 25�� �Դϴ�.\n\n��Ÿ�� : 5��");
		inform.Add("���濡 1 x 1 ũ���� ���� �����մϴ�. ���� ���� �ƴҶ��� ��밡���ϸ� ���� ��, �Ʊ� ������� ��� �Ұ��ϰ� �����ð��� 30�� �Դϴ�.\n\n��Ÿ�� : 5��");
		inform.Add("���濡 1 x 1 ũ���� ���� �����մϴ�. ���� ���� �ƴҶ��� ��밡���ϸ� ���� ��, �Ʊ� ������� ��� �Ұ��ϰ� �����ð��� 35�� �Դϴ�.\n\n��Ÿ�� : 5��");
	}
	protected override IEnumerator Cast_()
	{
		float DestroyTime = 10 + 5 * skill_Level;
		if (controller.isGround)
		{
			Destroy(Instantiate(Block, transform.position + (controller.direction * 1.2f), transform.rotation), DestroyTime);
		}
		yield return null;
	}
}
