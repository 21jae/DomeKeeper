using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_JemstoneStash : MonoBehaviour
{
    [Header("Score Info")]
    public float redjemScore = 0;
    public float greenjemScore = 0;
    public float bluejemScore = 0;

    float redtemp = 0;
    float greentemp = 0;
    float bluetemp = 0;

    Animator anim;
    [SerializeField] GameObject player;

    int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftControl) && S_GameManager.instance.player.isDomeCheck)
        {
            player.SetActive(false);
            SoundManager.instance.PlayComputerOn();
            S_GameManager.instance.player.playerCheck=false;
            anim.SetBool("On", true);
            count++;
        }
        else if(Input.GetKeyDown(KeyCode.LeftControl) && count == 1)
        {
            player.SetActive(true);
            SoundManager.instance.PlayComputerOff();
            anim.SetBool("On", false);
            S_GameManager.instance.player.playerCheck = true;
            count = 0;
        }

        if(redjemScore > redtemp)
        {
            M_GameManager.instance.redtotal += redjemScore-redtemp;
            redtemp = redjemScore;
        }
        else if (redjemScore < redtemp)
        {
            redtemp = redjemScore;
        }

        if (greenjemScore > greentemp)
        {
            M_GameManager.instance.greentotal += greenjemScore-greentemp;
            greentemp = greenjemScore;
        }
        else if(greenjemScore < greentemp)
        {
            greentemp = greenjemScore;
        }

        if (bluejemScore > bluetemp)
        {
            M_GameManager.instance.bluetotal += bluejemScore-bluetemp;
            bluetemp = bluejemScore;
        }
        else if (bluejemScore < bluetemp)
        {
            bluetemp = bluejemScore;
        }
    }
}
