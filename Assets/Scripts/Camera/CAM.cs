using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAM : MonoBehaviour
{
    [SerializeField] private GameObject Target;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Target.transform.position;
    }
}
