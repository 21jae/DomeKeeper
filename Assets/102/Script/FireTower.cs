using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTower : SubTower
{
    [SerializeField] RaycastHit2D hit;
    [SerializeField] LayerMask whatisEnemy;
    [SerializeField] public Transform FirePos;

    #region
    [Header("บา")]
    [SerializeField] float raydistance;
    [SerializeField] public float FireDuartion;
    [SerializeField] public float FireRestTime;
    [SerializeField] public float FireRestCool;
    [SerializeField] bool isFire;
    [SerializeField] bool playFire;
    [SerializeField] float Timer;


    #endregion
    public GameObject Fire;
    private void Start()
    {
        isMe = false;
    }
    protected override void Update()
    {
        base.Update();
        RestTimeCheck();

        UpdateFirePosition();
        Attack();
        Timer += Time.deltaTime;
    }

    void RestTimeCheck()
    {

        FireRestTime += Time.deltaTime;
    }

    void UpdateFirePosition()
    {
        if (isFire && Fire != null)
        {
            Fire.transform.position = FirePos.transform.position;
        }
    }
    protected override void Attack()
    {
        if (hit = Physics2D.Raycast(transform.position, transform.up, raydistance, whatisEnemy))
        {  
            if (FireRestTime > FireRestCool)
            {
           
                isFire = true;
            }
           
        }
       
        if (isFire)
        {
            if (Timer > 8f) { 
            SoundManager.instance.PlayFireTower();
                Debug.Log("fireaudio");
                Timer = 0;
            }
            StartCoroutine("CreateFire");
        }
        else
        {
            if (!isFire)
            {
                StopCoroutine("CreateFire");
            }
        }
    }

    IEnumerator CreateFire()
    {


            SoundManager.instance.PlayFireTower();
            GameObject Firebat = Instantiate(Fire, FirePos.transform.position, FirePos.transform.rotation);
            Destroy(Firebat, 0.2f);
            yield return new WaitForSeconds(FireDuartion);
            FireRestTime = 0f;
            isFire = false;
        
         
        

    }
}
