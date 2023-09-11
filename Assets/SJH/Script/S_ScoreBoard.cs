using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_ScoreBoard : MonoBehaviour
{
    [SerializeField] GameObject scoreBoard;
    [SerializeField] GameObject map;
    [SerializeField] GameObject skillboard;
    float scorecount =5;
    float skillboardcount = 0f;
    void Update()
    {
        scorecount -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.I))
        {
            scorecount = 5;
            scoreBoard.SetActive(true);

        }
        if (scorecount <= 0)
        {
            scoreBoard.SetActive(false);
        }

        if(Input.GetKey(KeyCode.Tab))
        {
            map.SetActive(true);
        }
        else
        {
            map.SetActive(false);
        }

        if(S_GameManager.instance.player.isDomeCheck && Input.GetKeyDown(KeyCode.K) && skillboardcount == 0)
        {
            skillboard.SetActive(true);
            SoundManager.instance.PlaySkillOpen();
            skillboardcount++;
        }
        else if(Input.GetKeyDown(KeyCode.K) && skillboardcount ==1)
        {
            skillboard.SetActive(false);
            SoundManager.instance.PlaySkillOpen();
            skillboardcount = 0;
        }
    }


}
