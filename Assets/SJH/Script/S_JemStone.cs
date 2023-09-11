using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_JemStone : MonoBehaviour
{
    [SerializeField] LayerMask targetLayer;
    [SerializeField] float scanRange = 1f;
    [SerializeField] float Speed = 1f;
    [SerializeField] jemColor color;
    enum jemColor
    {
        red,
        green,
        blue,
    }

    Transform nearestTarget;
    bool isScanning = true;

    private void Update()
    {
        ScanTarget();

        if (isScanning)
        {
            nearestTarget = GetNearestTarget();
            isScanning = false;
        }

        if (nearestTarget != null)
        {
            Vector2 dir = nearestTarget.position - transform.position;
            transform.Translate(dir * Speed * Time.deltaTime);
        }
    }

    void ScanTarget()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, scanRange, targetLayer);

        if (targets.Length > 0)
        {
            float closesDistance = Mathf.Infinity;
            Transform closesTarget = null;

            foreach (Collider2D target in targets)
            {
                float distance = Vector3.Distance(transform.position, target.transform.position);
                if (distance < closesDistance)
                {
                    closesDistance = distance;
                    closesTarget = target.transform;
                }
            }

            nearestTarget = closesTarget;
        }

        else
            nearestTarget = null;

        isScanning = false;

    }

    Transform GetNearestTarget()
    {
        return nearestTarget;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);

            if (color == jemColor.red)
            {
                collision.transform.GetComponent<WJ_Player>().redjemScore++;
            }
            if (color == jemColor.green)
            {
                collision.transform.GetComponent<WJ_Player>().greenjemScore++;
            }
            if (color == jemColor.blue)
            {
                collision.transform.GetComponent<WJ_Player>().bluejemScore++;
            }
        }
        else if (collision.gameObject.CompareTag("Pet"))
        {
            Destroy(gameObject);

            if (color == jemColor.red)
            {
                collision.transform.GetComponent<PetEntity>().redjemScore++;
            }
            if (color == jemColor.green)
            {
                collision.transform.GetComponent<PetEntity>().greenjemScore++;
            }
            if (color == jemColor.blue)
            {
                collision.transform.GetComponent<PetEntity>().bluejemScore++;
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, scanRange);
    }

    private bool weightjemScore(PetEntity pet)
    {
        float totalScore = pet.redjemScore + pet.greenjemScore + pet.bluejemScore;

        return totalScore < pet.maxScore;
    }

}
