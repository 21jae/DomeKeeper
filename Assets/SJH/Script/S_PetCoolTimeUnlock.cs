using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_PetCoolTimeUnlock : MonoBehaviour
{
    [Header("Price")]
    [SerializeField] float redjemPrice;
    [SerializeField] float bluejemPrice;
    [SerializeField] float greenjemPrice;

    [SerializeField] Button[] parentbutton;

    public bool parentCheck = false;


    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }


    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < parentbutton.Length; i++)
        {
         if(   parentbutton[i].interactable == false)
            {
                parentCheck = true;
                break;
            }
            else
            {
                parentCheck = false;
            }
        }
  
    }
    public void OnClick()
    {
        if (parentCheck)
        {
            if (S_GameManager.instance.stash.redjemScore >= redjemPrice
            && S_GameManager.instance.stash.bluejemScore >= bluejemPrice
            && S_GameManager.instance.stash.greenjemScore >= greenjemPrice)
            {
                GetComponent<Button>().interactable = false;

                S_GameManager.instance.stash.redjemScore -= redjemPrice;
                S_GameManager.instance.stash.bluejemScore -= bluejemPrice;
                S_GameManager.instance.stash.greenjemScore -= greenjemPrice;
            }
        }
    }
}
