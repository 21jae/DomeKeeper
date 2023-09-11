 using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StunTower : SubTower
{
    [SerializeField] public Transform StunPos;

    #region
    [Header("½ºÅÏ")]
    [SerializeField] public float StunDuartion;
    [SerializeField] public float StunRestCool;
    [SerializeField] public float StunRestTime;
 
   
    #endregion
    public GameObject Stun;

    public GameObject NoneAutoStun;
    private void Start()
    {
     
        isMe = false;
    }
    protected override void Update()
    {
        base.Update();
        ShotDelay();
        Attack();
        AutoAttack();
    }
    void AutoAttack()
    {
        if (SkillTreeManager.Instance.isTech3 == true)
        {
            if (StunRestTime > StunRestCool)
            {
                
                StartCoroutine("StunAtk");
            }
        }
    }
    protected override void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Space)) { 
            if (StunRestTime > StunRestCool)
            {
                SoundManager.instance.PlayStunTower();
                StartCoroutine("StunAtk");
                    Shot();
            }
            else 
            {
                StopCoroutine("StunAtk");
        
            }
        }
    }

    void Shot()
    {
        if (SkillTreeManager.Instance.isTech3 == false)
        {
            
          
            GameObject SA = Instantiate(NoneAutoStun, StunPos.transform.position, StunPos.transform.rotation);
            Destroy(SA, 5f);
            StunRestTime = 0;
        }
    }
    void ShotDelay()
    {
        StunRestTime += Time.deltaTime;
    }
    
  
    IEnumerator StunAtk()
    {
        if(SkillTreeManager.Instance.isTech3 == true) {
           
            GameObject StunAmmo = Instantiate(Stun, StunPos.transform.position, StunPos.transform.rotation);
        Destroy(StunAmmo, 5f);
        StunRestTime = 0;
        yield return new WaitForSeconds(StunRestCool);
        }

    }                   
}
