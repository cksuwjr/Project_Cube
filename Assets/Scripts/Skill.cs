using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    protected CubeController controller;
    protected void Start()
    {
        if (GetComponent<CubeController>())
            controller = GetComponent<CubeController>();
        else
            Debug.LogError("�ش� ��� CubeController.cs �� �����ϴ�!");
    }
    public void Cast()
    {
        StartCoroutine("Cast_");
    }
    public void StopCast()
    {
        StopCoroutine("Cast_");
    }
    protected abstract IEnumerator Cast_();
}
