using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static statics;

public class TileManager : MonoBehaviour
{
    [SerializeField] TILES tileType;
    PATH[,] tile;
    TILESTATUS status = TILESTATUS.OPEN;
    Index index = new Index();
    void Awake()
    {
        switch (tileType)
        {
            case (TILES.START):
                tile = new PATH[3, 3] { { PATH.NONE, PATH.EXIT, PATH.NONE }, { PATH.NONE, PATH.NONE, PATH.NONE }, { PATH.NONE, PATH.NONE, PATH.NONE } };
                break;
            case (TILES.SINGLE):
                tile = new PATH[3, 3] { { PATH.NONE, PATH.EXIT, PATH.NONE }, { PATH.NONE, PATH.NONE, PATH.NONE }, { PATH.NONE, PATH.EXIT, PATH.NONE } };
                break;
            case (TILES.DOUBLE):
                tile = new PATH[3, 3] { { PATH.NONE, PATH.EXIT, PATH.NONE }, { PATH.EXIT, PATH.NONE, PATH.NONE }, { PATH.NONE, PATH.EXIT, PATH.NONE } };
                break;
            case (TILES.TRIPLE):
                tile = new PATH[3, 3] { { PATH.NONE, PATH.EXIT, PATH.NONE }, { PATH.EXIT, PATH.NONE, PATH.EXIT }, { PATH.NONE, PATH.EXIT, PATH.NONE } };
                break;

        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Spin()
    {
        transform.Rotate(new Vector3(0,-90,0));
        RotateMatrixCounterClockwise();
    }
    public void RotateMatrixCounterClockwise()
    {
        PATH[,] newMatrix = new PATH[tile.GetLength(1), tile.GetLength(0)];
        int newColumn, newRow = 0;
        for (int oldColumn = tile.GetLength(1) - 1; oldColumn >= 0; oldColumn--)
        {
            newColumn = 0;
            for (int oldRow = 0; oldRow < tile.GetLength(0); oldRow++)
            {
                newMatrix[newRow, newColumn] = tile[oldRow, oldColumn];
                newColumn++;
            }
            newRow++;
        }
        tile = newMatrix;
    }
    public void printTile()
    {
        string s = "";
        for (int i = 0; i < tile.GetLength(0); i++)
        {
            for (int j = 0; j < tile.GetLength(1); j++)
            {
                s += tile[i, j].ToString() + " ";
            }
            s += "\n";
        }
        Debug.Log(s);
    }

    public TILESTATUS getTileStatus()
    {
        return status;
    }
    public void setTileStatus(TILESTATUS t)
    {
        status = t;
    }


    public Index getRandomExit()
    {
        List<Index> exits = new List<Index>();

        for (int i = 0; i < tile.GetLength(0); i++)
        {
            for (int j = 0; j < tile.GetLength(1); j++)
            {
                if (tile[i,j] == PATH.EXIT)
                {
                    exits.Add(new Index(i -1, j -1));
                }
            }
        }

        return exits[Random.Range(0, exits.Count)];
    }

    public List<Index> getExits()
    {
        List<Index> exits = new List<Index>();

        for (int i = 0; i < tile.GetLength(0); i++)
        {
            for (int j = 0; j < tile.GetLength(1); j++)
            {
                if (tile[i, j] == PATH.EXIT)
                {
                    exits.Add(new Index(i - 1, j - 1));
                }
            }
        }

        return exits;
    }

    public bool Attachable(TileManager t)
    {
        foreach (Index i in t.getExits())
        {
            foreach (Index e in getExits())
            {
                if (i.getRow() == e.getRow() * -1)
                {
                    return true;
                }
                if (i.getCol() == e.getCol() * -1)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public Index getIndex()
    {
        return index;
    }
    public void setIndex(int row, int col)
    {
        index = new Index(row, col);
    }

    public void setExit(int r, int c, PATH p)
    {
        tile[r, c] = p; 
    }

    public PATH getExit(int r, int c)
    {
        return tile[r, c];
    }
    public void setSpinIndex(int s)
    {
        for (int i=0; i< s%4; i++)
        {
            RotateMatrixCounterClockwise();
        }
    }
}
