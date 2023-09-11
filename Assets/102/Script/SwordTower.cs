using UnityEngine;

public class SwordTower : Tower
{
    #region 
    [Header("Auto업그레이드")]
    [SerializeField] private float autoMoveTime;
    [SerializeField] private float atk;
    public float Timer;

    #endregion

    private void Start()
    {
     
    }


    private void Update()
    {
        AutoMove();
        TimeContinue();
        SetRotation();
        Timer += Time.deltaTime;
      
      
         
    }
    void SetRotation()
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
    void TimeContinue()
    {

        autoMoveTime += Time.deltaTime;
        if (autoMoveTime > 4)
        {
            autoMoveTime = 0;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            SoundManager.instance.PlaySwordHit();
            collision.gameObject.GetComponent<M_Base>().Damage1(atk);

        }
    }
    void AutoMove()
    {
        
        if (angle < leftLockAngle)
        {
            if (autoMoveTime > 0 && autoMoveTime < 2)
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
            if (autoMoveTime > 2 && autoMoveTime < 4)
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
