using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static statics;

public class WFCMap : MonoBehaviour
{
    [SerializeField] WFCNode start;
    [SerializeField] GameObject[] tiles;
    WFCTile[,] map = new WFCTile[51, 51];
    List<WFCTile> collapsedTiles = new List<WFCTile>();
    List<WFCTile> openTiles = new List<WFCTile>();
    // Start is called before the first frame update
    void Start()
    {
        for (int row=0; row<map.GetLength(0); row++)
        {
            for (int col = 0; col < map.GetLength(1); col++)
            {
                map[row, col] = new WFCTile(tiles,row, col);
            }
        }

        foreach (WFCTile t in map)
        {
            openTiles.Add(t);
        }

        map[0, ((int)Mathf.Ceil(map.GetLength(1) / 2))].lockIn(0);
        //openTiles.Remove(map[0, ((int)Mathf.Ceil(map.GetLength(1) / 2))]);
        for ( int i=0; i< openTiles.Count; i++)
        {
            WFCTile w = openTiles[i];
            Debug.Log("(" +w.getIndex().getRow() + "," + w.getIndex().getCol() + ")");
            if (w.getIndex().getRow() == 0 && w.getIndex().getCol() ==25)
            {
                //Debug.Log("STILL HERE");
                openTiles.RemoveAt(i);
            }
        }
        for ( int i=0; i< openTiles.Count; i++)
        {
            WFCTile w = openTiles[i];
            Debug.Log("(" +w.getIndex().getRow() + "," + w.getIndex().getCol() + ")");
            if (w.getIndex().getRow() == 0 && w.getIndex().getCol() ==25)
            {
                Debug.Log("STILL HERE");
            }
        }
        propogate(map[0, ((int)Mathf.Ceil(map.GetLength(1) / 2))]);// = start;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Plus) || Input.GetKeyDown(KeyCode.Equals))
        {
            addTile();
        }
    }

    public void addTile()
    {
        //WFCTile selected = collapsedTiles[Random.Range(0,collapsedTiles.Count)];
        WFCTile choosen = openTiles[5];
        //choosen.printPossibilities();
        foreach (WFCTile t in openTiles)
        {
            //t.printPossibilities();
            if (t.possibleNodeCount() < choosen.possibleNodeCount())
            {
                choosen = t;
            }
        }


        Debug.Log("BEFORE " + openTiles.Count);
        //openTiles.Remove(choosen);

        for (int i = 0; i < openTiles.Count; i++)
        {
            WFCTile w = openTiles[i];
            Debug.Log("(" + w.getIndex().getRow() + "," + w.getIndex().getCol() + ")");
            if (w.getIndex().getRow() == choosen.getIndex().getRow() && w.getIndex().getCol() == choosen.getIndex().getCol())
            {
                //Debug.Log("STILL HERE");
                openTiles.RemoveAt(i);
            }
        }
        Debug.Log("After " + openTiles.Count);
        choosen.collapse();

    }


    public void propogate(WFCTile tile)
    {
        Stack<WFCTile> propogationStack = new Stack<WFCTile>();
        propogationStack.Push(tile);

        while (propogationStack.Count > 0)
        {
            WFCTile t = propogationStack.Pop();
            //t.printPossibilities();
            Index i = t.getIndex();
            if (cellExists2DArray(i.getRow() - 1, i.getCol(), map))
            {
                if (map[i.getRow() - 1, i.getCol()].getNodeIndex() == -1) {
                    if (map[i.getRow() - 1, i.getCol()].propogate(t, DIRECTIONS.DOWN))
                    {
                        propogationStack.Push(map[i.getRow() - 1, i.getCol()]);
                    }
                }
            }
            if (cellExists2DArray(i.getRow() + 1, i.getCol(), map))
            {
                if (map[i.getRow() + 1, i.getCol()].getNodeIndex() == -1)
                {
                    if (map[i.getRow() + 1, i.getCol()].propogate(t, DIRECTIONS.UP))
                    {
                        propogationStack.Push(map[i.getRow() + 1, i.getCol()]);
                    }
                }
            }
            if (cellExists2DArray(i.getRow(), i.getCol() - 1, map))
            {
                if (map[i.getRow(), i.getCol() - 1].getNodeIndex() == -1)
                {
                    if (map[i.getRow(), i.getCol() - 1].propogate(t, DIRECTIONS.LEFT))
                    {
                        propogationStack.Push(map[i.getRow(), i.getCol() - 1]);
                    }
                }
            }
            if (cellExists2DArray(i.getRow(), i.getCol() + 1, map))
            {
                if (map[i.getRow(), i.getCol() + 1].getNodeIndex() == -1)
                {
                    if (map[i.getRow(), i.getCol() + 1].propogate(t, DIRECTIONS.RIGHT))
                    {
                        //map[i.getRow(), i.getCol() + 1].printPossibilities();
                        propogationStack.Push(map[i.getRow(), i.getCol() + 1]);
                    }
                }
            }
        }
    }


}
