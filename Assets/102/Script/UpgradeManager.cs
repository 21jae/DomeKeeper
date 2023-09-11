using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public bool isPurchase; 

    public void Purchase()
    { 
        // 만약 자원보다 많으면 
        isPurchase = true;
        //else 
        //구매 불가 띄우기 
    }


}
