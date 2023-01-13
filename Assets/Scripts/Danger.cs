using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Danger : MonoBehaviour
{
    [SerializeField] private GameObject Meteo;

    void Start()
    {
        StartCoroutine(StartDangerous(1.5f));
    }
    IEnumerator StartDangerous(float time)
    {
        Renderer dangerFade = GetComponent<Renderer>();
        dangerFade.material.color = new Color(dangerFade.material.color.r, dangerFade.material.color.g, dangerFade.material.color.b, 0);
        while (dangerFade.material.color.a < 1)
        {
            dangerFade.material.color += new Color(0, 0, 0, Time.deltaTime / time);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        GetComponent<Renderer>().material.color += new Color(0, 0, 0, -1);
        Instantiate(Meteo, transform.position + new Vector3(0, 15, 0), Quaternion.identity).SetActive(true);

        Invoke("Next", Random.Range(1.01f,3.01f));
    }
    void Next()
    {
        Instantiate(gameObject, new Vector3(Random.Range(-23, 23), 1.1f, Random.Range(-23, 23)), transform.rotation).SetActive(true);
        //Instantiate(gameObject, new Vector3(Random.Range(-23, 23), 1.1f, Random.Range(-23, 23)), transform.rotation).SetActive(true);
        Destroy(gameObject);
    }
}
