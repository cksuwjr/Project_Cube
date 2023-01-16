using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    protected CubeController controller;
    public int skill_Level;
    [SerializeField] protected float cooltime;
    protected float ReusableWaitTime;
    protected bool Castable;
    protected void Start()
    {
        if (GetComponent<CubeController>())
            controller = GetComponent<CubeController>();
        else
            Debug.LogError("�ش� ��� CubeController.cs �� �����ϴ�!");
        if (cooltime == 0)
            cooltime = 1f;
        Castable = true;
    }
    public bool Cast()
    {
        if (Castable && skill_Level > 0)
        {
            StopCast(); // ������ ����Ǵ� ���� ��ų �ߴ�
            StartCoroutine("Cast_");
            StartCoroutine(Cooltime());
            return true;
        }
        else
        {
            //Debug.Log("�ش� ��ų�� ������ð� �� �Դϴ�!");
            return false;
        }
    }
    public void StopCast()
    {
        StopCoroutine("Cast_");
    }
    protected abstract IEnumerator Cast_();
    protected IEnumerator Cooltime()
    {
        Castable = false;
        ReusableWaitTime = cooltime;
        while (ReusableWaitTime > 0)
        {
            ReusableWaitTime -= Time.deltaTime;
            yield return null;
        }
        ReusableWaitTime = 0;
        Castable = true;
    }
    
}
