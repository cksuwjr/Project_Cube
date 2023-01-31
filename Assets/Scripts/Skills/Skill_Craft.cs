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
		inform.Add("전방에 1 x 1 크기의 벽을 생성합니다. 점프 중이 아닐때만 사용가능하며 벽은 적, 아군 상관없이 통과 불가하고 유지시간은 15초 입니다.\n\n쿨타임 : 5초");
		inform.Add("전방에 1 x 1 크기의 벽을 생성합니다. 점프 중이 아닐때만 사용가능하며 벽은 적, 아군 상관없이 통과 불가하고 유지시간은 20초 입니다.\n\n쿨타임 : 5초");
		inform.Add("전방에 1 x 1 크기의 벽을 생성합니다. 점프 중이 아닐때만 사용가능하며 벽은 적, 아군 상관없이 통과 불가하고 유지시간은 25초 입니다.\n\n쿨타임 : 5초");
		inform.Add("전방에 1 x 1 크기의 벽을 생성합니다. 점프 중이 아닐때만 사용가능하며 벽은 적, 아군 상관없이 통과 불가하고 유지시간은 30초 입니다.\n\n쿨타임 : 5초");
		inform.Add("전방에 1 x 1 크기의 벽을 생성합니다. 점프 중이 아닐때만 사용가능하며 벽은 적, 아군 상관없이 통과 불가하고 유지시간은 35초 입니다.\n\n쿨타임 : 5초");
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
