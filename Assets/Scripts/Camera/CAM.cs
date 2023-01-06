using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CAM : MonoBehaviour
{
    [SerializeField] private GameObject Target;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Target)
            transform.position = Target.transform.position;

        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
            SceneManager.LoadScene("SampleScene");
    }
}
