using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubTower : Tower
{
    [SerializeField] public Transform SubPos;
    #region 
    [Header("Auto업그레이드")]
    [SerializeField] private float AutoMoveTime;
    #endregion
    [SerializeField] float attackDelayTime;
    [SerializeField] protected GameObject SubAmmo;
    [SerializeField] protected bool isMe;

    void Start()
    {
        isMe = true;
    }



    protected virtual void Update()
    {
        if (M_GameManager.instance.domehp <= 0)
        {
            gameObject.SetActive(false);
        }
        Move();
        AutoMove();
        TimeContinue();
        SetRotation();
        AttackDelay();
        if (isMe == true)
        {
            AutoAttack();
            Attack();
        }
    }
    protected void TimeContinue()
    {

        AutoMoveTime += Time.deltaTime;
        if (AutoMoveTime > 4)
        {
            AutoMoveTime = 0;
        }

    }
    void AttackDelay()
    {
        attackDelayTime += Time.deltaTime;
    }
    protected void Move()
    {
        if (S_GameManager.instance.player.playerCheck == false)
        {

            if (SkillTreeManager.Instance.isTech3 != true)
            {
                if (angle < leftLockAngle)
                {
                    if (Input.GetKey(KeyCode.RightArrow))
                    {

                        angle = angle + Time.deltaTime * angularSpeed;
                        transform.Rotate(0, 0, rote);
                        if (angle >= leftLockAngle)
                        {
                            transform.rotation = Quaternion.Euler(0, 0, 90);
                        }
                    }
                }

                if (angle > rightLockAngle)
                {
                    if (Input.GetKey(KeyCode.LeftArrow))
                    {

                        angle = angle + Time.deltaTime * -angularSpeed;
                        transform.Rotate(0, 0, -rote);
                        if (angle <= rightLockAngle)
                        {
                            transform.rotation = Quaternion.Euler(0, 0, -90);
                        }
                    }
                }
            }
        }
    }
    protected void SetRotation()
    {
        if (angle > 1.5 && angle < 1.6)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (angle >= 0.8 && angle <= 0.9)
        {
            transform.rotation = Quaternion.Euler(0, 0, -45);
        }
        if (angle >= 2.3 && angle <= 2.4)
        {
            transform.rotation = Quaternion.Euler(0, 0, 45);
        }
        posX = rotationCenter.position.x + Mathf.Cos(angle) * rotationRadius;
        posY = rotationCenter.position.y + Mathf.Sin(angle) * rotationRadius / 1.5f;


        transform.position = new Vector3(posX, posY);
    }
    protected void AutoMove()
    {
        if (SkillTreeManager.Instance.isTech3 == true)
        {
            if (angle < leftLockAngle)
            {
                if (AutoMoveTime > 0 && AutoMoveTime < 2)
                {

                    angle = angle + Time.deltaTime * angularSpeed;
                    transform.Rotate(0, 0, rote);
                    if (angle >= leftLockAngle)
                    {

                        transform.rotation = Quaternion.Euler(0, 0, 90);
                    }
                }
            }

            if (angle > rightLockAngle)
            {
                if (AutoMoveTime > 2 && AutoMoveTime < 4)
                {

                    angle = angle + Time.deltaTime * -angularSpeed;
                    transform.Rotate(0, 0, -rote);
                    if (angle <= rightLockAngle)
                    {
                        transform.rotation = Quaternion.Euler(0, 0, -90);
                    }
                    if (transform.rotation.z < -90)
                    {
                        transform.rotation = Quaternion.Euler(0, 0, -90);
                    }
                }
            }
        }
    }

    protected virtual void Attack()
    {
        if (S_GameManager.instance.player.playerCheck == false)
        {

            if (Input.GetKey(KeyCode.Space))
            {
                if (attackDelayTime > 1.5f)
                {
                  
                    attackDelayTime = 0f;
                    GameObject subAmmo = Instantiate(SubAmmo, SubPos.transform.position, SubPos.transform.rotation);
                    Destroy(subAmmo, 5f);
                }
            }
        }
    }
    void AutoAttack()
    {
        if (SkillTreeManager.Instance.isTech3)
        {
            if (attackDelayTime > 1.5f)
            {
             
                attackDelayTime = 0f;
                GameObject subAmmo = Instantiate(SubAmmo, SubPos.transform.position, SubPos.transform.rotation);
                Destroy(subAmmo, 5f);
            }
        }
    }
}
