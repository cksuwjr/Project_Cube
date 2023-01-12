using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CubeUI : MonoBehaviour
{
    public Status PlayerCube;
    public Image HpBar;

    // End
    public float LiveTime = 0f;
    public Text dietext;

    // StageTurn
    public Text stagetext;

    public Image DieFadeScreen;
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
        string text = "";
        /*
        if (LiveTime < 10)
            text += "�ڻ��� ���� ���� �ʾƿ�..";
        else if (LiveTime <= 30)
            text += "�ʹ� ������..! {0: 0.00}��..?";
        else if (LiveTime <= 60)
            text += "�׷��� {0:0}�ʴ� �����..";
        else if (LiveTime <= 180)
            text += "{0:0.00}��.. �� �����߱���?";
        else if (LiveTime <= 360)
            text += "{0:0.00}��. ���� �����̾� �ļ���";
        else
            text += "���ϴ�, {0:0.00}�� ����� ��.";
        */
        text += "\n\n\n\n";
        text += "��ƾ �ð�: {0:0.00}��";
        dietext.text = string.Format(text, LiveTime, LiveTime);
        Invoke("ReStart", 3f);
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
    IEnumerator FadeIn(float wait, float time)
    {
        yield return new WaitForSeconds(wait);
        DieFadeScreen.gameObject.SetActive(true);
        float fade = 0f;
        while (fade < time)
        {
            DieFadeScreen.color += new Color(0,0,0, 0.01f / time);
            fade += 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
        SceneManager.LoadScene("Start");
    }
    void ReStart()
    {
        StartCoroutine(FadeIn(0, 1));
    }
}
