using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeInfo : MonoBehaviour
{
    public int id;
    public bool isPurchased;
    public float cost;
    public Text Destext;

    public void Awake()
    {
        Destext = GetComponent<Text>();
    }
    public void BtnOnClick()
    { 
        if(id == 1) 
        {
           
            Destext.text = "레이저가 관통이 가능해집니다.";
        }
        if (id == 2)
        {
            Destext.text = "사용가능한 보조타워를 하나 추가합니다.";
        }
        if (id == 3)
        {
            Destext.text = "더 이상 포탑을 조종할 수 없지만 자동으로 움직이며 공격합니다.";
        }
    }
}
