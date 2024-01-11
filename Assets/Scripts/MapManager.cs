using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static statics;

public class MapManager : MonoBehaviour
{
    [SerializeField] GameObject[] pathsPrefabs;
    [SerializeField] GameObject start;
    GameObject[,] map = new GameObject[51,51];
    // Start is called before the first frame update
    void Start()
    {
        map[map.GetLength(0) - 1, ((int)Mathf.Ceil(map.GetLength(1) / 2))] = start;
        start.GetComponent<TileManager>().setIndex(0, ((int)Mathf.Ceil(map.GetLength(1) / 2)));
        start.transform.position = new Vector3((15 * start.GetComponent<TileManager>().getIndex().getCol()) ,0, (15 * start.GetComponent<TileManager>().getIndex().getRow()));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Plus) || Input.GetKeyDown(KeyCode.Equals))
        {
            addPath();
        } 
    }

    void addPath()
    {
        List<GameObject> openTiles =  getOpenTiles();
        GameObject selected = openTiles[Random.Range( 0, openTiles.Count ) ];
        Index exit = selected.GetComponent<TileManager>().getRandomExit();
        Debug.Log("(" + exit.getRow() + " , " + exit.getCol() + ")");
        List < GameObject > tiles = getPossibleTiles(selected.GetComponent<TileManager>().getIndex(), exit);
        GameObject tile = tiles[Random.Range(0, tiles.Count)];
        int spinIndex = 0;
        Debug.Log("SELECTED:");
        //selected.GetComponent<TileManager>().printTile();
        Debug.Log("TILE : 0");
        //tile.GetComponent<TileManager>().printTile();
        //while (!(selected.GetComponent<TileManager>().Attachable(tile.GetComponent<TileManager>())))
        while(! exit.getOpposite(tile.GetComponent<TileManager>().getExits()))
        {
            tile.GetComponent<TileManager>().Spin();
            spinIndex++;
            Debug.Log("TILE : " + spinIndex);
            //tile.GetComponent<TileManager>().printTile();
            Debug.Log("Spinner");
        }
        GameObject go;
        if (exit.getRow() != 0)
        {
            go = Instantiate(tile, new Vector3(  selected.transform.position.x, 0, (15 * (selected.GetComponent<TileManager>().getIndex().getRow() - exit.getRow()))), tile.transform.rotation, transform);
        }
        else
        {
            go = Instantiate(tile, new Vector3(  (15 * (selected.GetComponent<TileManager>().getIndex().getCol() + exit.getCol())), 0 , selected.transform.position.z ) , tile.transform.rotation, transform);
        }

        go.GetComponent<TileManager>().setIndex(selected.GetComponent<TileManager>().getIndex().getRow() - exit.getRow(), selected.GetComponent<TileManager>().getIndex().getCol() - exit.getCol());
        go.GetComponent<TileManager>().setExit(( (exit.getRow() * -1) +1 ) , ( (exit.getCol() * -1) +1 ) , PATH.NONE);
        Debug.Log("BEFORE");
        selected.GetComponent<TileManager>().printTile();
        selected.GetComponent<TileManager>().setExit(( (exit.getRow()) +1 ) , ( (exit.getCol()) +1 ) , PATH.NONE);
        Debug.Log("AFTER");
        selected.GetComponent<TileManager>().printTile();

        Debug.Log("exits " + selected.GetComponent<TileManager>().getExits().Count);
        if (selected.GetComponent<TileManager>().getExits().Count == 0)
        {
            selected.GetComponent<TileManager>().setTileStatus(TILESTATUS.CLOSE);
        }
        go.GetComponent<TileManager>().setTileStatus(TILESTATUS.OPEN);
        go.GetComponent<TileManager>().setSpinIndex(spinIndex);

        map[go.GetComponent<TileManager>().getIndex().getRow(), go.GetComponent<TileManager>().getIndex().getCol()] = go;

        Debug.Log("____________________________________________");

    }


    List<GameObject> getOpenTiles()
    {
        List<GameObject> open = new List<GameObject>();
        foreach (GameObject g in map)
        {
            if (g!=null)
            {
                if (g.GetComponent<TileManager>().getTileStatus() == TILESTATUS.OPEN)
                {
                    open.Add(g);
                }
            }
        }
        return open;
    }


    List<GameObject> getPossibleTiles(Index t, Index index)
    {
        List<GameObject> possibleNeighbours = new List<GameObject>();
        int emptyNeighbours = 0;
        Debug.Log("(" + t.getRow() + " , " + t.getCol() + ")");
        int checkRow = t.getRow() - index.getRow();
        int checkCol = t.getCol() + index.getCol();
        Debug.Log("(" + checkRow + " , " + checkCol + ")");
        if (checkRow > 0)
        {
            if (map[checkRow-1, checkCol] == null)
            {
                emptyNeighbours++;
            }
        }

        if (checkRow < map.GetLength(0)-1)
        {
            if (map[checkRow+1, checkCol] == null)
            {
                emptyNeighbours++;
            }
        }

        if (checkCol > 0)
        {
            if (map[checkRow, checkCol-1] == null)
            {
                emptyNeighbours++;
            }
        }

        if (checkCol < map.GetLength(1)-1)
        {
            if (map[checkRow, checkCol+1] == null)
            {
                emptyNeighbours++;
            }
        }


        if (emptyNeighbours > 0)
        {
            possibleNeighbours.Add(pathsPrefabs[0]);
            if (emptyNeighbours > 1)
            {
                possibleNeighbours.Add(pathsPrefabs[1]);
                if (emptyNeighbours > 2)
                {
                    possibleNeighbours.Add(pathsPrefabs[2]);
                }
            }
        }


        return possibleNeighbours;
    }
}
