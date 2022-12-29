using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CubeUI : MonoBehaviour
{
    public Status PlayerCube;
    public Image HpBar;
    private void Awake()
    {
        if (PlayerCube == null) PlayerCube = GameObject.Find("Cube").GetComponent<Status>();
        if (HpBar == null) HpBar = transform.GetChild(1).GetComponent<Image>();
    }
    void Hp_Update(float var)
    {
        PlayerCube.Hp -= var;
        HpBar.fillAmount = PlayerCube.Hp / PlayerCube.MaxHp; 
    }
}
