using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static statics;

public class WFCMap2 : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] WFCNode start;
    [SerializeField] GameObject[] tiles;
    [SerializeField] int maxRow = 51, maxCol = 51;
    [SerializeField] HealthUI healthUI;
    WFCTile2[,] map;
    List<WFCTile> collapsedTiles = new List<WFCTile>();
    List<WFCTile2> openTiles = new List<WFCTile2>();
    List<NavMeshSurface> surfaces = new List<NavMeshSurface>();
    public static List<Vector3> entryPoints = new List<Vector3>();
    public static Transform target;
    // Start is called before the first frame update
    private void Awake()
    {
        map = new WFCTile2[51, 51];
        WFCTile2.setMaxRow(maxRow);
        WFCTile2.setMaxCol(maxCol);
        WFCTile2.setMap(gameObject);
    }
    void Start()
    {
        for (int row = 0; row < map.GetLength(0); row++)
        {
            for (int col = 0; col < map.GetLength(1); col++)
            {
                map[row, col] = new WFCTile2(tiles, row, col);
            }
        }
        setTileEntries();

        map[0, ((int)Mathf.Ceil(map.GetLength(1) / 2))].lockIn(0, new List<DIRECTIONS>() { DIRECTIONS.DOWN });
        openTiles.Add(map[0, ((int)Mathf.Ceil(map.GetLength(1) / 2))]);
        target = map[0, ((int)Mathf.Ceil(map.GetLength(1) / 2))].GetNode().transform;
        target.GetComponentInChildren<HealthManager>().setMaxHealth(player.GetComponent<Player>().getHealth());
        target.GetComponentInChildren<HealthManager>().setUI(healthUI);
        LevelManager.health = target.GetComponentInChildren<HealthManager>();
        restack();
        rebake();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Plus) || Input.GetKeyDown(KeyCode.Equals))
        {
            addTile();
        }
    }

    void setTileEntries()
    {
        foreach (GameObject g in tiles)
        {
            WFCNode tile = g.GetComponent<WFCNode>();
            tile.clearNeighbours();
            foreach (GameObject o in tiles)
            {
                WFCNode other = o.GetComponent<WFCNode>();
                if (other.getIndex() != 0)
                {
                    if (tile.getExitUp() == other.getExitDown())
                    {
                        tile.addPossibleNeighboursUP(other.getIndex());
                    }
                    if (tile.getExitDown() == other.getExitUp())
                    {
                        tile.addPossibleNeighboursDOWN(other.getIndex());
                    }
                    if (tile.getExitLeft() == other.getExitRight())
                    {
                        tile.addPossibleNeighboursLEFT(other.getIndex());
                    }
                    if (tile.getExitRight() == other.getExitLeft())
                    {
                        tile.addPossibleNeighboursRIGHT(other.getIndex());
                    }
                }
            }
        }
    }

    public void addTile()
    {
        //WFCTile selected = collapsedTiles[Random.Range(0,collapsedTiles.Count)];
        WFCTile2 choosen = openTiles[Random.Range(0,openTiles.Count)];
        DIRECTIONS d = choosen.GetNode().getRandomExit();
        Index index = new Index();

        switch (d) 
        {
            case (DIRECTIONS.UP):
                index = new Index(choosen.getIndex().getRow() + 1, choosen.getIndex().getCol());
                break;
            case (DIRECTIONS.DOWN):
                index = new Index(choosen.getIndex().getRow() - 1, choosen.getIndex().getCol());
                break;
            case (DIRECTIONS.LEFT):
                index = new Index(choosen.getIndex().getRow() , choosen.getIndex().getCol() - 1);
                break;
            case (DIRECTIONS.RIGHT):
                index = new Index(choosen.getIndex().getRow(), choosen.getIndex().getCol() + 1);
                break;
        }

        List<List<int>> exitInfo = new List<List<int>>();
        List<DIRECTIONS> closeExitInfo = new List<DIRECTIONS>();

        if (cellExists2DArray(index.getRow() + 1, index.getCol(), map))
        {
            if (map[index.getRow() + 1, index.getCol()].getNodeIndex() != -1)
            {
                //Debug.Log("U");
                exitInfo.Add(map[index.getRow() + 1, index.getCol()].GetNode().getPossibleNeighboursDOWN());
                map[index.getRow() + 1, index.getCol()].GetNode().RemoveExit(DIRECTIONS.DOWN);
                closeExitInfo.Add(DIRECTIONS.UP);
                //map[index.getRow(), index.getCol()].GetNode().RemoveExit(DIRECTIONS.UP);//////////////////////////////////CHANGED B4 SLAYP
            }
        }
        if (cellExists2DArray(index.getRow() - 1, index.getCol(), map))
        {
            if (map[index.getRow() - 1, index.getCol()].getNodeIndex() != -1)
            {
                //Debug.Log("D");
                exitInfo.Add(map[index.getRow() - 1, index.getCol()].GetNode().getPossibleNeighboursUP());
                map[index.getRow() - 1, index.getCol()].GetNode().RemoveExit(DIRECTIONS.UP);
                closeExitInfo.Add(DIRECTIONS.DOWN);
                //map[index.getRow(), index.getCol()].GetNode().RemoveExit(DIRECTIONS.DOWN);//////////////////////////////////CHANGED B4 SLAYP
            }
        }
        if (cellExists2DArray(index.getRow(), index.getCol() - 1, map))
        {
            if (map[index.getRow(), index.getCol() - 1].getNodeIndex() != -1)
            {
                //Debug.Log("L");
                exitInfo.Add(map[index.getRow(), index.getCol() - 1].GetNode().getPossibleNeighboursRIGHT());
                map[index.getRow(), index.getCol() - 1].GetNode().RemoveExit(DIRECTIONS.RIGHT);
                closeExitInfo.Add(DIRECTIONS.LEFT);
                //map[index.getRow(), index.getCol()].GetNode().RemoveExit(DIRECTIONS.LEFT);//////////////////////////////////CHANGED B4 SLAYP
            }
        }
        if (cellExists2DArray(index.getRow(), index.getCol() + 1, map))
        {
            if (map[index.getRow(), index.getCol() + 1].getNodeIndex() != -1)
            {
                //Debug.Log("R");
                exitInfo.Add(map[index.getRow(), index.getCol() + 1].GetNode().getPossibleNeighboursLEFT());
                map[index.getRow(), index.getCol() + 1].GetNode().RemoveExit(DIRECTIONS.LEFT);
                closeExitInfo.Add(DIRECTIONS.RIGHT);
                //map[index.getRow(), index.getCol()].GetNode().RemoveExit(DIRECTIONS.RIGHT);//////////////////////////////////CHANGED B4 SLAYP
            }
        }
        //Debug.Log(exitInfo.Count);
        //map[choosen.getIndex().getRow(), choosen.getIndex().getCol()].GetNode().RemoveExit(d);
        map[index.getRow(), index.getCol()].propogate(exitInfo,closeExitInfo);

        if (map[index.getRow(), index.getCol()].GetNode().getExitCount() > 0)
        {
            openTiles.Add(map[index.getRow(), index.getCol()]);
        }

                for (int i = 0; i < openTiles.Count; i++)
                {
                    WFCTile2 w = openTiles[i];
                    if (w.GetNode().getExitCount() == 0)
                    {
                        openTiles.RemoveAt(i);
                    }
                }

        restack();
        rebake();
        setEntries();
    }

    void rebake()
    {
        /*NavMeshSurface n;
        foreach (Transform child in transform)
        {
            foreach (Transform t in child)
            {
                if (t.TryGetComponent<NavMeshSurface>(out n))
                {
                    n.BuildNavMesh();
                }
            }
        }
        foreach (NavMeshSurface n in surfaces)
        {
            n.BuildNavMesh();
        }*/
        target.GetComponentInChildren<TriggerDestroyer>().gameObject.GetComponent<Collider>().enabled = false;
        surfaces[0].BuildNavMesh();
        target.GetComponentInChildren<TriggerDestroyer>().gameObject.GetComponent<Collider>().enabled = true;
    }

    void restack()
    {
        surfaces.Clear();
        NavMeshSurface n;
        foreach (Transform child in transform)
        {
            foreach (Transform t in child)
            {
                if (t.TryGetComponent<NavMeshSurface>(out n))
                {
                    surfaces.Add(n);//n.BuildNavMesh();
                }
            }
        }
    }

    void setEntries()
    {
        entryPoints.Clear();
        foreach (WFCTile2 tile in map)
        {
            if (tile.getNodeIndex() != -1)
            {
                if (tile.GetNode().getExit(DIRECTIONS.UP) || tile.GetNode().getEntry(DIRECTIONS.UP))
                {
                    entryPoints.Add(tile.GetNode().transform.position + EnemyPlacementManager.placements[DIRECTIONS.UP]);
                }
                if (tile.GetNode().getExit(DIRECTIONS.DOWN) || tile.GetNode().getEntry(DIRECTIONS.DOWN))
                {
                    entryPoints.Add(tile.GetNode().transform.position + EnemyPlacementManager.placements[DIRECTIONS.DOWN]);
                }
                if (tile.GetNode().getExit(DIRECTIONS.LEFT) || tile.GetNode().getEntry(DIRECTIONS.LEFT))
                {
                    entryPoints.Add(tile.GetNode().transform.position + EnemyPlacementManager.placements[DIRECTIONS.LEFT]);
                }
                if (tile.GetNode().getExit(DIRECTIONS.RIGHT) || tile.GetNode().getEntry(DIRECTIONS.RIGHT))
                {
                    entryPoints.Add(tile.GetNode().transform.position + EnemyPlacementManager.placements[DIRECTIONS.RIGHT]);
                }
            }
        }
    }



    public static void printList<T>(List<T> list)
    {
        Debug.Log("PRINTING LIST:");
        foreach (T t in list)
        {
            Debug.Log(t.ToString());
        }
        Debug.Log("_____________________");
    }


}
