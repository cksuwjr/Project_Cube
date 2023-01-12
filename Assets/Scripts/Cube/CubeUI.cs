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
            text += "자살은 몸에 좋지 않아요..";
        else if (LiveTime <= 30)
            text += "너무 빠르다..! {0: 0.00}초..?";
        else if (LiveTime <= 60)
            text += "그래도 {0:0}초는 버텼네..";
        else if (LiveTime <= 180)
            text += "{0:0.00}초.. 좀 적응했구만?";
        else if (LiveTime <= 360)
            text += "{0:0.00}초. 뭐야 집중이야 꼼수야";
        else
            text += "잘하누, {0:0.00}초 버텼다 야.";
        */
        text += "\n\n\n\n";
        text += "버틴 시간: {0:0.00}초";
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
