using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class S_PlayerSkillUpManager : MonoBehaviour
{
    [Header("TeleprtSkill Info")]
    public GameObject teleprtUnlockImage;
    public GameObject teleportScorllBar;
    [SerializeField] public bool teleportuseCheck = false;

    [Header("LightSkill Info")]
    public GameObject lightUnlockImage;
    public GameObject lightScorllBar;
    [SerializeField] public bool lightuseCheck = false;

    [Header("DrillPowerUp Info")]
    public GameObject drillPowerUp1Image;
    public GameObject drillPowerUp2Image;

    int drillPowerTech = 1;
    int lightTech = 1;
    int lightRangeTech = 1;
    public GameObject drill;
    private void Start()
    {

    }
    public void LightRangeUp1()
    {
        S_GameManager.instance.player.GetComponentInChildren<Light2D>().pointLightInnerRadius += 0.5f;
        S_GameManager.instance.player.GetComponentInChildren<Light2D>().pointLightOuterRadius += 0.5f;
        lightRangeTech++;
    }
    public void LightRangeUp2()
    {
        if (lightRangeTech == 2)
        {
            S_GameManager.instance.player.GetComponentInChildren<Light2D>().pointLightInnerRadius += 0.5f;
            S_GameManager.instance.player.GetComponentInChildren<Light2D>().pointLightOuterRadius += 0.5f;
        }

    }
    public void DrillPowerUP1()
    {
        S_GameManager.instance.player.GetComponentInChildren<S_Drill>().damage += 10f;
        drill.GetComponent<S_Drill>().speed = 1.5f;
        drillPowerUp1Image.SetActive(true);
        drillPowerTech++;
    }
    public void DrillPowerUP2()
    {
        if (drillPowerTech == 2)
        {
            S_GameManager.instance.player.GetComponentInChildren<S_Drill>().damage += 10f;
            drill.GetComponent<S_Drill>().speed = 2f;
            drillPowerUp2Image.SetActive(true);
        }
    }

    public void UseLightSkill()
    {
        S_GameManager.instance.player.useLightSkill = true;
        lightUnlockImage.SetActive(false);
        lightScorllBar.GetComponent<Slider>().value = 100;
        lightuseCheck = true;
        lightTech++;

    }

    public void UseTeleportSkill()
    {
        if (lightTech == 2)
        {
            S_GameManager.instance.player.useteleportSkill = true;
            teleprtUnlockImage.SetActive(false);
            teleportScorllBar.GetComponent<Slider>().value = 100;
            teleportuseCheck = true;
        }
    }
}
