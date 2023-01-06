using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CubeUI : MonoBehaviour
{
    public Status PlayerCube;
    public Image HpBar;

    // End
    public float LiveTime = 0f;
    public Text dietext;

    // StageTurn
    public Text stagetext;


    private void Awake()
    {
        if (PlayerCube == null) PlayerCube = GameObject.Find("Cube").GetComponent<Status>();
        if (HpBar == null) HpBar = transform.GetChild(1).GetComponent<Image>();

        dietext.text = "";

        PopupStageUI("Stage 1");
    }
    void Hp_Update(float var)
    {
        PlayerCube.Hp -= var;
        HpBar.fillAmount = PlayerCube.Hp / PlayerCube.MaxHp;
    }

    private void Update()
    {
        LiveTime += Time.deltaTime;
        Hp_Update(0);

        
    }

    public void PopupDieUI()
    {
        string text = "\n\n";
        /*
        if (LiveTime < 10)
            text += "�ƴ� ����?? �ڻ� ������;;";
        else if (LiveTime <= 30)
            text += "�ܿ� {0}�ʻ�? ��Ƽ���߳� ����";
        else if (LiveTime <= 60)
            text += "�׷��� {0:0.00}�ʴ� �����.. ��";
        else if (LiveTime <= 180)
            text += "{0:0.00}��.. �� �����ߴ�?";
        else if (LiveTime <= 360)
            text += "{0:0.00}��. ���� �����̾� �ļ���";
        else
            text += "���ϴ�, {0:0.00}�� ����� ��.";
        */
        text += "��ƾ �ð�: {0:0.00}��";
        dietext.text = string.Format(text, LiveTime);
    }
    public void PopupStageUI(string text)
    {
        stagetext.text = text;
        StartCoroutine("Hide");
    }
    IEnumerator Hide()
    {
        RectTransform pos = stagetext.GetComponent<RectTransform>();
        pos.localPosition = new Vector3(0,5,0);
        float timer = 0;
        while(timer < 1.2f)
        {
            pos.localPosition = new Vector3(0, 5 + timer, 0);
            timer += Time.deltaTime;
            yield return null;
        }
        stagetext.text = "";
    }
}
