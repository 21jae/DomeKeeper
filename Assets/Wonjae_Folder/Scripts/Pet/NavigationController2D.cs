using Aoiti.Pathfinding; // ��� Ž�� ���̺귯��
using System.Collections.Generic;
using UnityEngine;

public class NavigationController2D : MonoBehaviour
{
    [Header("Navigator �ɼ�")]
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
        //�ð�ȭ
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

    // ��ǥ�� �޾ƿͼ� ������ ����� �����ϴ� �Լ�
    // List<Vector2> pathLeftToGo�� ��λ� ������ �����ϴ� ������ ���ִ� MoveCommand
    void GetMoveCommand(Vector2 target) //�־��� ��ǥ ��ġ�� �޾ƿ� �������� ����� �����ϴ� ����
    {
        Vector2 closestNode = GetClosestNode(new Vector2(0.35f, -13.35f)); //���� ��ġ���� ���� ����� �׸��� ���� ã�´�. new Vector2(0.25f, -12f)
        Vector2 targetNode = GetClosestNode(target);    //��ǥ��ġ���� ���� ����� �׸��� ���� ã�´�. 

        bool canMove = true;

        // ���� ��ġ�� ��ǥ ��ġ �ֺ��� �׸��� ������ ��θ� ����
        //��ü�� ����Ͽ� ���� ��ġ�� ��ǥ ��ġ �ֺ��� �׸��� ������ ��θ� �����ϰ�, ��ΰ� ��������� Count 0���� ����.
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
        if (pathfinder.GenerateAstarPath(closestNode, GetClosestNode(target), out path)) // ���� ��ġ�� ��ǥ ��ġ �ֺ��� �׸��� ������ ��θ� ����
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

        if (transform.localScale.x < 0) //���� ������ �ٶ󺸰��ִٸ�
            pt.Flip();

        if (transform.localScale.x > 0)
            pt.Flip();
        return randomTarget;
    }


    // ���� ����� �׸��� ���� ã�� �Լ�
    Vector2 GetClosestNode(Vector2 target)
    {
        return new Vector2(Mathf.Round(target.x / gridSize) * gridSize, Mathf.Round(target.y / gridSize) * gridSize);
    }

    // �Ÿ��� �ٻ�ȭ�ϴ� �Լ�
    float GetDistance(Vector2 A, Vector2 B)
    {
        return (A - B).sqrMagnitude; // CPU �ð��� �����ϱ� ���� ���� �Ÿ��� ���
    }

    // ������ ����� �� �Ÿ��� �׸��忡�� ã�� �Լ�
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