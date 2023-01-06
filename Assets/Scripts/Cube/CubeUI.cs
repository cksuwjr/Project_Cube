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
            text += "아니 뭐함?? 자살 ㄴㄴ요;;";
        else if (LiveTime <= 30)
            text += "겨우 {0}초뿐? 버티긴했네 ㅋㅋ";
        else if (LiveTime <= 60)
            text += "그래도 {0:0.00}초는 버텼네.. ㅋ";
        else if (LiveTime <= 180)
            text += "{0:0.00}초.. 좀 적응했누?";
        else if (LiveTime <= 360)
            text += "{0:0.00}초. 뭐야 집중이야 꼼수야";
        else
            text += "잘하누, {0:0.00}초 버텼다 야.";
        */
        text += "버틴 시간: {0:0.00}초";
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
