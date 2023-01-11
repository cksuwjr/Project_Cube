using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class OnStart : MonoBehaviour
{
    [SerializeField] private GameObject Background;
    [SerializeField] private Text title;
    [SerializeField] private Text start;
    [SerializeField] private Text howToPlay;
    [SerializeField] private GameObject Explanation;
    [SerializeField] private Text exit;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeOut(1f, 1.5f));
        StartCoroutine(StartWordAppear(1f, 0.75f));
        StartCoroutine(StartWordSportlights(1.5f, 1f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FadeOut(float wait, float time)
    {
        Image colorrend = Background.GetComponent<Image>();
        yield return new WaitForSeconds(wait);
        float fade = 0f;
        while(fade < time)
        {
            colorrend.color -= new Color(0.01f / time, 0.01f / time, 0.01f / time, 0);
            fade += 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
        //Debug.Log("완료");
    }
    IEnumerator StartWordAppear(float wait, float time)
    {
        yield return new WaitForSeconds(wait);

        float fade = 0f;
        while (fade < time)
        {
            title.color += new Color(0,0,0, 0.01f / time);
            start.color += new Color(0, 0, 0, 0.01f / time);
            howToPlay.color += new Color(0, 0, 0, 0.01f / time);
            exit.color += new Color(0, 0, 0, 0.01f / time);
            fade += 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
        //Debug.Log("완료");
    }
    IEnumerator StartWordSportlights(float wait, float time)
    {
        yield return new WaitForSeconds(wait);
        
        float fade = 0f;
        while (fade < time)
        {
            title.color += new Color(0.01f / time, 0.01f / time, 0.01f / time, 0);
            start.color += new Color(0.01f / time, 0.01f / time, 0.01f / time, 0);
            howToPlay.color += new Color(0.01f / time, 0.01f / time, 0.01f / time, 0);
            exit.color += new Color(0.01f / time, 0.01f / time, 0.01f / time, 0);
            fade += 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
        //Debug.Log("완료");
    }
    public void OnClick_Start()
    {
        SceneManager.LoadScene("Game");
    }
    public void OnClick_HowToPlay()
    {
        Explanation.SetActive(true);
    }
    public void OnClick_Explanation()
    {
        Explanation.SetActive(false);

    }
    public void OnClick_Exit()
    {
        Application.Quit();
    }
}
