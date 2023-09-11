using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerBig : MonoBehaviour
{
    #region 
    [Header("∫Ú∑π¿Ã¿˙")]
    [SerializeField] private float Atk;


    #endregion

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            collision.gameObject.GetComponent<M_Base>().Damage1(Atk);
     
        }
    }
    private void Start()
    {
        SoundManager.instance.PlayBigLazer();
    }
    private void Update()
    {
        UpdateAtk();
    }
    void UpdateAtk()
    { 
        if(SkillTreeManager.Instance.isChargeDelayLess == true) 
        {
            Atk *= 2;
        }
    }
}
