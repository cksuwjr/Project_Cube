using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceMeteo : MonoBehaviour
{
    List<GameObject> contacted = new List<GameObject>();
    bool isDowning = false;
    void Start()
    {
        StartCoroutine(MeteoStorm(15f));
    }

    IEnumerator MeteoStorm(float speed)
    {
        isDowning = true;
        transform.position = transform.position + new Vector3(0, 10, 0);
        gameObject.SetActive(true);
        GetComponent<Rigidbody>().velocity = new Vector3(0, -speed, 0);
        while(transform.position.y > transform.localScale.y) { yield return null; }
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        isDowning = false;

        yield return new WaitForSeconds(3f);
        for (int i = 0; i < contacted.Count; i++)
            if (contacted[i])
                contacted[i].GetComponent<CubeController>().IsBinded = false;
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CubeController>())
        {
            other.GetComponent<CubeController>().IsBinded = true;
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
            contacted.Add(other.gameObject);
            if (isDowning)
                other.GetComponent<CubeController>().GetDamage(400);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CubeController>())
        {
            other.GetComponent<CubeController>().IsBinded = false;
            contacted.Remove(other.gameObject);
        }
    }
}
