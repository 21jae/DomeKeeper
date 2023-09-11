using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WJ_SkillButton : MonoBehaviour
{
    public Image skillImage;
    public Text skillNameText;
    public Text skillDesText;

    public int skillButtonID;

    public void onPreviewSkill()
    {
        S_GameManager.instance.activateSkill = transform.GetComponent<Buttoninteractive>();

        skillImage.sprite = S_GameManager.instance.skills[skillButtonID].skillSprite;
        skillNameText.text = S_GameManager.instance.skills[skillButtonID].skillName;
        skillDesText.text = S_GameManager.instance.skills[skillButtonID].skillDes;
    }
        
}
