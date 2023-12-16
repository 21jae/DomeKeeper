using Aoiti.Pathfinding; // 경로 탐색 라이브러리
using System.Collections.Generic;
using UnityEngine;

public class NavigationController2D : MonoBehaviour
{
    [Header("Navigator 옵션")]
    [SerializeField] float gridSize = 0.5f;
    [SerializeField] float speed = 0.05f;

    Pathfinder<Vector2> pathfinder;
    [SerializeField] LayerMask obstacles;
    [SerializeField] bool searchShortcut = false;
    [SerializeField] bool snapToGrid = false;
    List<Vector2> path;
    List<Vector2> pathLeftToGo = new List<Vector2>();
    [SerializeField] bool drawDebugLines;

    Rigidbody2D rb;
    CircleCollider2D circleColl;
    PetEntity pt;

    void Start()
    {
        pathfinder = new Pathfinder<Vector2>(GetDistance, GetNeighbourNodes, 1000);
        rb = GetComponent<Rigidbody2D>();
        circleColl = GetComponent<CircleCollider2D>();
        pt = GetComponent<PetEntity>();
        Gmc();
    }

    void Update()
    {
        if (pathLeftToGo.Count > 0)
        {
            Vector3 dir = (Vector3)pathLeftToGo[0] - transform.position;
            transform.position += dir.normalized * speed;
            if (((Vector2)transform.position - pathLeftToGo[0]).sqrMagnitude < speed * speed)
            {
                transform.position = pathLeftToGo[0];
                pathLeftToGo.RemoveAt(0);

                if (pathLeftToGo.Count == 0)
                {
                    circleColl.enabled = true;
                }
            }
        }
        //시각화
        if (drawDebugLines)
        {
            for (int i = 0; i < pathLeftToGo.Count - 1; i++)
            {
                Debug.DrawLine(pathLeftToGo[i], pathLeftToGo[i + 1]);
            }
        }
    }

    public void Bmc()
    {
        BackMoveCommand(new Vector2(-1.45f, -10.4f));
    }

    public void Gmc()
    {
        Vector2 randomTarget = SearchMine();
        GetMoveCommand(randomTarget);
    }

    // 목표를 받아와서 움직임 명령을 생성하는 함수
    // List<Vector2> pathLeftToGo에 경로상 점들을 저장하는 역할을 해주는 MoveCommand
    void GetMoveCommand(Vector2 target) //주어진 목표 위치를 받아와 움직임을 명령을 생성하는 역할
    {
        Vector2 closestNode = GetClosestNode(new Vector2(0.35f, -13.35f)); //현재 위치에서 가장 가까운 그리드 점을 찾는다. new Vector2(0.25f, -12f)
        Vector2 targetNode = GetClosestNode(target);    //목표위치에서 가장 가까운 그리드 점을 찾는다. 

        bool canMove = true;

        // 현재 위치와 목표 위치 주변의 그리드 점으로 경로를 생성
        //객체를 사용하여 현재 위치와 목표 위치 주변의 그리드 점으로 경로를 생성하고, 경로가 비어있으면 Count 0으로 만듦.
        if (pathfinder.GenerateAstarPath(closestNode, targetNode, out path) || path.Count == 0)
        {
            if (canMove)
            {
                path.Clear();
                path.Add(closestNode);
                path.Add(targetNode);
            }
        }

        pathLeftToGo = new List<Vector2>(path);
        if (!snapToGrid) 
            pathLeftToGo.Add(targetNode);
    }

    void BackMoveCommand(Vector2 target)
    {
        rb.gravityScale = 0;
        speed = 0.16f;
        circleColl.enabled = false;
        Vector2 closestNode = GetClosestNode(transform.position);
        if (pathfinder.GenerateAstarPath(closestNode, GetClosestNode(target), out path)) // 현재 위치와 목표 위치 주변의 그리드 점으로 경로를 생성
        {
            pathLeftToGo = new List<Vector2>(path);
            if (!snapToGrid) 
                pathLeftToGo.Add(target);
        }
    }

    private Vector2 SearchMine()
    {
        rb.gravityScale = 8;
        speed = 0.02f;
        Vector2 randomTarget = (new Vector2(Random.Range(-80.0f, 80.0f), (Random.Range(-50.0f, -300.0f))));

        if (transform.localScale.x < 0) //현재 왼쪽을 바라보고있다면
            pt.Flip();

        if (transform.localScale.x > 0)
            pt.Flip();
        return randomTarget;
    }


    // 가장 가까운 그리드 점을 찾는 함수
    Vector2 GetClosestNode(Vector2 target)
    {
        return new Vector2(Mathf.Round(target.x / gridSize) * gridSize, Mathf.Round(target.y / gridSize) * gridSize);
    }

    // 거리를 근사화하는 함수
    float GetDistance(Vector2 A, Vector2 B)
    {
        return (A - B).sqrMagnitude; // CPU 시간을 절약하기 위해 제곱 거리를 사용
    }

    // 가능한 연결과 그 거리를 그리드에서 찾는 함수
    Dictionary<Vector2, float> GetNeighbourNodes(Vector2 pos)
    {
        Dictionary<Vector2, float> neighbours = new Dictionary<Vector2, float>();

        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                if (i == 0 && j == 0) continue;

                Vector2 dir = new Vector2(i, j) * gridSize;
                if (!Physics2D.Linecast(pos, pos + dir, obstacles))
                {
                    neighbours.Add(GetClosestNode(pos + dir), dir.magnitude);
                }
            }
        }
        return neighbours;
    }
}