using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CubeUI : MonoBehaviour
{
    public CubeController PlayerCube;
    public Image HpBar;
    public Image ExpBar;

    // End
    public float LiveTime = 0f;
    public Text dietext;

    // StageTurn
    public Text stagetext;

    public Image DieFadeScreen;

    public GameObject SkillSelect;

    public GameObject Space;

    public GameObject PlayerHPBar;

    private void Awake()
    {
        if (PlayerCube == null) PlayerCube = GameObject.Find("Cube").GetComponent<CubeController>();
        if (HpBar == null) HpBar = transform.GetChild(1).GetComponent<Image>();
           
        dietext.text = "";

        //PopupStageUI("Stage 1");
    }
    void Hp_Update(float var)
    {
        if (!PlayerCube) return;

        Status mystatus = PlayerCube.GetComponent<Status>();
        //PlayerCube.Hp -= var;
        HpBar.fillAmount = mystatus.Hp / mystatus.MaxHp;
        SetHealthText((int)mystatus.Hp);
    }
    void Exp_Update(float var)
    {
        if (!PlayerCube) return;

        Status mystatus = PlayerCube.GetComponent<Status>();
        ExpBar.fillAmount = mystatus.Exp / mystatus.MaxExp;
    }

    private void Update()
    {
        LiveTime += Time.deltaTime;
        Hp_Update(0);
        Exp_Update(0);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            Space.SetActive(true);
        }
    }

    public void PopupDieUI()
    {

        PlayerHPBar.SetActive(false);

        string text = "";
        /*
        if (LiveTime < 10)
            text += "";
        else if (LiveTime <= 30)
            text += "";
        else if (LiveTime <= 60)
            text += "";
        else if (LiveTime <= 180)
            text += "";
        else if (LiveTime <= 360)
            text += "";
        else
            text += "";
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
        pos.localPosition = new Vector3(0,300,0);
        float timer = 0;
        while(timer < 3f)
        {
            pos.localPosition = new Vector3(0, 300 + (timer * 30f), 0);
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
        SceneManager.LoadScene("Menu");
    }
    void ReStart()
    {
        StartCoroutine(FadeIn(0, 1));
    }
    public void PopupSkillSelect()
    {
        SkillSelect.SetActive(true);
        Time.timeScale = 0;

        GameObject Q_Skill_UI = SkillSelect.transform.GetChild(1).GetChild(0).gameObject;
        GameObject W_Skill_UI = SkillSelect.transform.GetChild(1).GetChild(1).gameObject;
        GameObject E_Skill_UI = SkillSelect.transform.GetChild(1).GetChild(2).gameObject;
        GameObject R_Skill_UI = SkillSelect.transform.GetChild(1).GetChild(3).gameObject;

        GameObject ui;
        Skill skill;
        Text name;
        Text inform;

        // Q ui설정 
        ui = Q_Skill_UI;
        skill = PlayerCube.Skill_Q;
        if (skill.inform.Count > skill.skill_Level + 1)
        {
            if (skill.skill_Level != 0)
                ui.GetComponent<Image>().color += new Color(0, 0, 0, 1);
            name = ui.transform.GetChild(0).GetComponent<Text>();
            inform = ui.transform.GetChild(1).GetComponent<Text>();
            name.text = skill.inform[0];

            name.text += "<color=#FF0000>";
            for (int i = 0; i < skill.skill_Level + 1; i++)
                name.text += "l";
            name.text += "</color>";

            inform.text = skill.inform[skill.skill_Level + 1];
        }else{ Q_Skill_UI.SetActive(false); }
        // W ui 설정
        ui = W_Skill_UI;
        skill = PlayerCube.Skill_W;

        if (skill.inform.Count > skill.skill_Level + 1)
        {
            if (skill.skill_Level != 0)
                ui.GetComponent<Image>().color += new Color(0, 0, 0, 1);
            name = ui.transform.GetChild(0).GetComponent<Text>();
            inform = ui.transform.GetChild(1).GetComponent<Text>();
            name.text = skill.inform[0];

            name.text += "<color=#FF0000>";
            for (int i = 0; i < skill.skill_Level + 1; i++)
                name.text += "l";
            name.text += "</color>";

            inform.text = skill.inform[skill.skill_Level + 1];
        }
        else { W_Skill_UI.SetActive(false); }
        // E ui 설정
        ui = E_Skill_UI;
        skill = PlayerCube.Skill_E;

        if (skill.inform.Count > skill.skill_Level + 1)
        {
            if (skill.skill_Level != 0)
                ui.GetComponent<Image>().color += new Color(0, 0, 0, 1);
            name = ui.transform.GetChild(0).GetComponent<Text>();
            inform = ui.transform.GetChild(1).GetComponent<Text>();
            name.text = skill.inform[0];

            name.text += "<color=#FF0000>";
            for (int i = 0; i < skill.skill_Level + 1; i++)
                name.text += "l";
            name.text += "</color>";

            inform.text = skill.inform[skill.skill_Level + 1];
        }
        else { E_Skill_UI.SetActive(false); }
        // R ui 설정
        ui = R_Skill_UI;
        skill = PlayerCube.Skill_R;

        if (skill.inform.Count > skill.skill_Level + 1)
        {
            if (skill.skill_Level != 0)
                ui.GetComponent<Image>().color += new Color(0, 0, 0, 1);
            name = ui.transform.GetChild(0).GetComponent<Text>();
            inform = ui.transform.GetChild(1).GetComponent<Text>();
            name.text = skill.inform[0];

            name.text += "<color=#FF0000>";
            for (int i = 0; i < skill.skill_Level + 1; i++)
                name.text += "l";
            name.text += "</color>";

            inform.text = skill.inform[skill.skill_Level + 1];
        }
        else { R_Skill_UI.SetActive(false); }

        if (!Q_Skill_UI.activeSelf && !W_Skill_UI.activeSelf && !E_Skill_UI.activeSelf && !R_Skill_UI.activeSelf)
        {
            SkillSelect.SetActive(false);
            Time.timeScale = 1;
        }
    }
    public Text levelText;
    public Text attackpowerText;
    public Text deffensepowerText;
    public Text healthText;
    public Text healpowerText;
    public void SetLevelText(int value)
    {
        levelText.text = value.ToString();
    }
    public void SetAttackPowerText(int value)
    {
        attackpowerText.text = value.ToString();
    }
    public void SetDeffensePowerText(int value)
    {
        deffensepowerText.text = value.ToString();
    }
    public void SetHealthText(int value)
    {
        healthText.text = value.ToString();
    }
    public void SetHealPowerText(int value)
    {
        healpowerText.text = value.ToString();
    }
    public void OnClick_first()
    {
        Skill skill = PlayerCube.GetComponent<CubeController>().Skill_Q;
        skill.skill_Level += 1;
        if (skill.skill_Level == 1)
            skill.CooltimeUI(0, 0);
        SkillSelect.SetActive(false);
        Time.timeScale = 1;
    }
    public void OnClick_second()
    {
        Skill skill = PlayerCube.GetComponent<CubeController>().Skill_W;
        skill.skill_Level += 1;
        if (skill.skill_Level == 1)
            skill.CooltimeUI(0, 0);
        SkillSelect.SetActive(false);
        Time.timeScale = 1;
    }
    public void OnClick_third()
    {
        Skill skill = PlayerCube.GetComponent<CubeController>().Skill_E;
        skill.skill_Level += 1;
        if (skill.skill_Level == 1)
            skill.CooltimeUI(0, 0);
        SkillSelect.SetActive(false);
        Time.timeScale = 1;
    }
    public void OnClick_fourth()
    {
        Skill skill = PlayerCube.GetComponent<CubeController>().Skill_R;
        skill.skill_Level += 1;
        if (skill.skill_Level == 1)
            skill.CooltimeUI(0, 0);
        SkillSelect.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnClick_Resume()
    {
        Space.SetActive(false);
        if (!SkillSelect.activeSelf)
            Time.timeScale = 1;
    }

    public void OnClick_Restart()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnClick_BacktoMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

}
