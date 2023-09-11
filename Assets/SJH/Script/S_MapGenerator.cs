using UnityEngine;
using UnityEngine.Tilemaps;

public class S_MapGenerator : MonoBehaviour
{

    Vector3Int cellPosition;
    public GameObject redjam;
    public GameObject bluejam;
    public GameObject greenjam;
    //public Transform playerPos;

    bool[] nullTile;

    [Header("MapStatus")]
    [SerializeField] Vector2Int mapSize;
    [SerializeField] float mineralXoffeset;
    [SerializeField] float mineralYoffeset;

    [Header("Tile")]
    [SerializeField] Tilemap tileMap;
    [SerializeField] Tile GroundTile;
    [SerializeField] Tile GroundTile2;
    [SerializeField] Tile downTile;
    [SerializeField] Tile topTile;
    [SerializeField] Tile leftWallTile;
    [SerializeField] Tile rightWallTile;
    [SerializeField] Tile RdownCornerTile;
    [SerializeField] Tile LdownCornerTile;
    [SerializeField] Tile RupCornerTile;
    [SerializeField] Tile LupCornerTile;
    [SerializeField] Tile[] tileList;


    void Start()
    {
        tileMap = GetComponent<Tilemap>();

        tileList = new Tile[10];

        tileList[0] = GroundTile;
        tileList[1] = GroundTile2;
        tileList[2] = downTile;
        tileList[3] = topTile;
        tileList[4] = leftWallTile;
        tileList[5] = rightWallTile;
        tileList[6] = RdownCornerTile;
        tileList[7] = LdownCornerTile;
        tileList[8] = RupCornerTile;
        tileList[9] = LupCornerTile;

        FillTile();
    }

    void FillTile()
    {
        int count = 0;

        for (int j = mapSize.y / 2 - 1; j > -mapSize.y; j--)
        {
            tileMap.SetTile(new Vector3Int(0, j, 0), GroundTile);

            for (int i = 1; i <= mapSize.x / 2; i++)
            {
                int rnd = Random.Range(0, 100);

                SetTileColor(j);

                if (rnd >= 0 && rnd < 5 && j > -mapSize.y + 1) //잼스블록 생성
                {
                    CreatJem(j, i);
                    CreatJem(j, -i);
                }

                if (rnd >= 19 && rnd < 60)
                {
                    tileMap.SetTile(new Vector3Int(i, j, 0), GroundTile2);
                    tileMap.SetTile(new Vector3Int(-i, j, 0), GroundTile2);
                }
                else
                {
                    tileMap.SetTile(new Vector3Int(i, j, 0), GroundTile);
                    tileMap.SetTile(new Vector3Int(-i, j, 0), GroundTile);
                }

                if (i > count)
                {
                    break;
                }
            }
            count++;
        }
    }

    private void SetTileColor(int j)
    {
        if (j <= mapSize.y / 2 - 1 && j > mapSize.y / 10)
        {
            for (int c = 0; c < 10; c++)
            {
                tileList[c].color = new Color(1f, 1f, 1f);
            }
        }

        else if (j <= mapSize.y / 10 && j > -mapSize.y / 2)
        {
            for (int c = 0; c < 10; c++)
            {
                tileList[c].color = new Color(0.5f, 0.5f, 0.5f);
            }
        }

        else
        {
            for (int c = 0; c < 10; c++)
            {
                tileList[c].color = new Color(0.2f, 0.2f, 0.2f);
            }
        }
    }

    void CreatJem(int j, int i)
    {
        int rnd = Random.Range(0, 30);

        if (rnd >= 0 && rnd < 15)
        {
            var mine = Instantiate(redjam, tileMap.transform);
            mine.transform.position = new Vector3(i + mineralXoffeset, j - mineralYoffeset, 0);
            tileMap.SetTile(new Vector3Int(i, j - 1, 0), GroundTile);
            tileMap.SetTile(new Vector3Int(-i, j - 1, 0), GroundTile);
        }
        else if ((rnd >= 15 && rnd < 25) && (j <= 20 && j > -200))
        {
            var mine = Instantiate(bluejam, tileMap.transform);
            mine.transform.position = new Vector3(i + mineralXoffeset, j - mineralYoffeset, 0);
            tileMap.SetTile(new Vector3Int(i, j - 1, 0), GroundTile);
            tileMap.SetTile(new Vector3Int(-i, j - 1, 0), GroundTile);
        }
        else if ((rnd >= 25 && rnd < 30) && (j <= -100 && j > -200))
        {
            var mine = Instantiate(greenjam, tileMap.transform);
            mine.transform.position = new Vector3(i + mineralXoffeset, j - mineralYoffeset, 0);
            tileMap.SetTile(new Vector3Int(i, j - 1, 0), GroundTile);
            tileMap.SetTile(new Vector3Int(-i, j - 1, 0), GroundTile);
        }
    }

    void FillWall() //룸 타일과 바깥 타일이 만나는 부분 
    {
        if (tileMap.GetTile(cellPosition) == null) //현재타일이 null이면
        {
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    Vector3Int checkTilePos = new Vector3Int(cellPosition.x + x, cellPosition.y + y, 0);

                    if (x == 0 && y == 0) { continue; }

                    if (tileMap.GetTile(checkTilePos) != null)  //처음 깨진 블록 기준으로 탐색 된 주변 블록
                    {
                        nullTile = new bool[9];
                        int count = 0;

                        for (int i = 0; i < 9; i++)
                        {
                            nullTile[i] = false;
                        }

                        for (int _x = -1; _x <= 1; _x++)
                        {
                            for (int _y = -1; _y <= 1; _y++)
                            {
                                if (tileMap.GetTile(new Vector3Int(checkTilePos.x + _x, checkTilePos.y + _y, 0)) == null) //위에서 탐색된 블록 기준 주변 블록
                                {
                                    nullTile[count] = true;
                                }
                                count++;
                            }
                        }

                        SetTileColor(checkTilePos.y);

                        if (nullTile[1] && nullTile[3])
                            tileMap.SetTile(new Vector3Int(checkTilePos.x, checkTilePos.y, 0), LdownCornerTile);
                        else if (nullTile[3] && nullTile[7])
                            tileMap.SetTile(new Vector3Int(checkTilePos.x, checkTilePos.y, 0), RdownCornerTile);
                        else if (nullTile[5] && nullTile[7])
                            tileMap.SetTile(new Vector3Int(checkTilePos.x, checkTilePos.y, 0), RupCornerTile);
                        else if (nullTile[1] && nullTile[5])
                            tileMap.SetTile(new Vector3Int(checkTilePos.x, checkTilePos.y, 0), LupCornerTile);
                        else if (!nullTile[0] && !nullTile[1] && !nullTile[2] && nullTile[7])
                            tileMap.SetTile(new Vector3Int(checkTilePos.x, checkTilePos.y, 0), rightWallTile);
                        else if (!nullTile[6] && !nullTile[7] && !nullTile[8] && nullTile[1])
                            tileMap.SetTile(new Vector3Int(checkTilePos.x, checkTilePos.y, 0), leftWallTile);
                        else if (!nullTile[3] && nullTile[5])
                            tileMap.SetTile(new Vector3Int(checkTilePos.x, checkTilePos.y, 0), topTile);
                        else if (nullTile[3] && !nullTile[5])
                            tileMap.SetTile(new Vector3Int(checkTilePos.x, checkTilePos.y, 0), downTile);
                    }

                }
            }

        }
    }

    public void MakeDot(Vector3 Pos)
    {
        cellPosition = tileMap.WorldToCell(Pos);
        tileMap.SetTile(cellPosition, null);
        SoundManager.instance.PlayGroundCrack();
        FillWall();
    }
}