using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aoiti.Pathfinding; // 경로 탐색 라이브러리를 가져옴
using Unity.VisualScripting;

public class NavigationController2D : MonoBehaviour
{
    [Header("Navigator 옵션")]
    [SerializeField] float gridSize = 0.5f; // 그리드 크기 설정, 큰 맵을 위해 Patience나 gridSize를 늘릴 수 있음
    [SerializeField] float speed = 0.05f; // 움직임 속도 설정, 더 빠른 이동을 위해 값을 증가시킬 수 있음

    Pathfinder<Vector2> pathfinder; // 경로 탐색 메서드와 Patience를 저장하는 Pathfinder 객체
    [Tooltip("Navigator가 통과할 수 없는 레이어들")]
    [SerializeField] LayerMask obstacles;
    [Tooltip("마지막 지점에 도달할 때까지 네비게이터가 그리드 위에서만 움직이도록 비활성화. 경로는 짧아지지만 추가적인 Physics2D.LineCast가 필요함")]
    [SerializeField] bool searchShortcut = false;
    [Tooltip("가장 가까운 그리드 위의 점에서 멈추도록 네비게이터를 설정합니다.")]
    [SerializeField] bool snapToGrid = false;
    Vector2 targetNode; // 2D 공간에서의 목표
    List<Vector2> path;
    List<Vector2> pathLeftToGo = new List<Vector2>();
    [SerializeField] bool drawDebugLines;

    Rigidbody2D rb;
    CircleCollider2D circleColl;
    PetEntity pt;

    void Start()
    {
        pathfinder = new Pathfinder<Vector2>(GetDistance, GetNeighbourNodes, 1000); // 큰 맵을 위해 Patience나 gridSize를 늘릴 수 있음
        rb = GetComponent<Rigidbody2D>();
        circleColl = GetComponent<CircleCollider2D>();
        pt = GetComponent<PetEntity>();
        Gmc();
    }

    void Update()
    {
        if (pathLeftToGo.Count > 0) // 목표에 도달하지 않았을 때
        {
            Vector3 dir = (Vector3)pathLeftToGo[0] - transform.position;    //목표 위치와 현재 위치 간의 차이 벡터를 계산.
            transform.position += dir.normalized * speed; //현재 위치에서 목표 위치로 향하는 벡터를 정규화해 이동 속도를 곱한 값 = 일정한 속도로 이동가능
            if (((Vector2)transform.position - pathLeftToGo[0]).sqrMagnitude < speed * speed) // 현재 위치와 목표사이의 거리의 제곱이 특정 조간보다 작은지 확인, 제곱근 연산은 계산 비용이 높기에 sqrMagnitude를 사용해 연산 효율 높임.
            {
                transform.position = pathLeftToGo[0];   //현재 위치를 목표 위치로 설정
                pathLeftToGo.RemoveAt(0);   //이동이 완료되었으므로 경로 리스트에서 첫 번째 위치를 제거.

                if (pathLeftToGo.Count == 0)
                {
                    circleColl.enabled = true;
                }
            }
        }
        //시각화
        if (drawDebugLines) // 시각화를 위해 경로를 화면에 표시한다. 점을 추가하는 로직은 GetMoveCommand, BackMoveCommand이다.
        {
            for (int i = 0; i < pathLeftToGo.Count - 1; i++) // List에 저장된 점들 사이에 라인을 그리기 위한 반복문. Count -1을 하는 이유는 마지막 점과 그 이전의 점간에 라인을 그리기 위하여.
            {
                Debug.DrawLine(pathLeftToGo[i], pathLeftToGo[i + 1]);   //현재 점과 다음 점 간에 라인을 그린다.
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

        if (canMove)
        {
            if (searchShortcut && path.Count > 0)
                pathLeftToGo = ShortenPath(path);   //path 리스트에 저장된 경로를 짧게 만들어 pathLeftToGo 리스트에 저장.
            else
            {
                pathLeftToGo = new List<Vector2>(path); //path리스트에 저장된 경로를 그대로 pathLeftToGo 리스트에 복사. 경로 생성이 실패한다면 현재까지 생성된 경로로 유지 가능.
                if (!snapToGrid) pathLeftToGo.Add(targetNode);  //옵션이 활성화되지 않은경우, targetNode를 pathLeftToGo 리스트에 추가한다. 마지막으로 도달한 위치까지로 이동이 가능하게한다.
            }
        }

    }
    void BackMoveCommand(Vector2 target)
    {
        rb.gravityScale = 0;
        speed = 0.16f;
        circleColl.enabled = false;
        Vector2 closestNode = GetClosestNode(transform.position);
        if (pathfinder.GenerateAstarPath(closestNode, GetClosestNode(target), out path)) // 현재 위치와 목표 위치 주변의 그리드 점으로 경로를 생성
        {
            if (searchShortcut && path.Count > 0)
                pathLeftToGo = ShortenPath(path);
            else
            {
                pathLeftToGo = new List<Vector2>(path);
                if (!snapToGrid) pathLeftToGo.Add(target);
            }
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

    // 경로를 짧게 만드는 함수
    List<Vector2> ShortenPath(List<Vector2> path)
    {
        List<Vector2> newPath = new List<Vector2>();

        for (int i = 0; i < path.Count; i++)
        {
            newPath.Add(path[i]);
            for (int j = path.Count - 1; j > i; j--)
            {
                if (!Physics2D.Linecast(path[i], path[j], obstacles))
                {
                    i = j;
                    break;
                }
            }
            newPath.Add(path[i]);
        }
        newPath.Add(path[path.Count - 1]);
        return newPath;
    }
}