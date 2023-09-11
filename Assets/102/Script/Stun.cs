using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : MonoBehaviour
{
   
    [SerializeField] public float stunTime;

    public float Speed = 5f;

    public float Atk;
    private GameObject target; // 현재 추적 중인 적
    private float chaseRange = 50f; // 추적 범위
    private List<GameObject> availableTargets = new List<GameObject>(); // 이동 가능한 적 목록
    public M_Base monsterBase;

    private void Start()
    {
        FindAvailableTargets();
        SetNextTarget();
        //monsterBase.ChangeStunTime(100);
        if(target != null) { 
        SoundManager.instance.PlaySubTower();
        }
    }

    public float GetsStunTime()
    {
        return stunTime;
    }

    private void Update()
    {
        stunTime = M_GameManager.instance.stunTime;
        if (target != null)
        {

            // 타겟 방향 계산
            Vector3 dir = (target.transform.position - transform.position).normalized;
            // 이동
            transform.Translate(dir * Speed * Time.deltaTime, Space.World);

            // 만약 타겟과의 거리가 일정 범위 내에 있다면 타겟을 공격
            if (Vector3.Distance(transform.position, target.transform.position) < 0.5f)
            {
                target.GetComponent<M_Base>().Damage2(Atk);
                Destroy(gameObject);
            }
        }
        else
        {
            // 타겟이 없는 경우 총알 파괴
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            collision.gameObject.GetComponent<M_Base>().Damage2(Atk);
            Destroy(gameObject);
        }
    }

    // 이동 가능한 적 목록을 찾습니다.
    private void FindAvailableTargets()
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");

        foreach (GameObject monster in monsters)
        {
            float distance = Vector3.Distance(transform.position, monster.transform.position);
            if (distance < chaseRange)
            {
                availableTargets.Add(monster);
            }
        }
    }

    // 다음 타겟을 설정합니다.
    private void SetNextTarget()
    {
        if (availableTargets.Count > 0)
        {
            // 이동 가능한 적 중에서 가장 가까운 적을 선택
            float closestDistance = Mathf.Infinity;
            foreach (GameObject monster in availableTargets)
            {
                float distance = Vector3.Distance(transform.position, monster.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    target = monster;
                }
            }

            // 선택된 적은 목록에서 제거
            availableTargets.Remove(target);
        }
        else
        {
            // 이동 가능한 적이 없는 경우, 총알을 파괴
            Destroy(gameObject);
        }
    }
}