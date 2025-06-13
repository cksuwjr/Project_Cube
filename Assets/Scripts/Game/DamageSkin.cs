using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSkin : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Floating());
        Destroy(gameObject, 1.2f);
    }
    IEnumerator Floating()
    {
        transform.rotation = Quaternion.Euler(90, 0, 0);
        while (true)
        {
            transform.position += new Vector3(0, 0, 0.01f);
            //skin.GetComponent<RectTransform>().position += new Vector3(0, 0.1f, 0);
            yield return null;
        }
    }
}
