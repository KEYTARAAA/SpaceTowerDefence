using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Index
{
    int row = 0;
    int col = 0;

    public Index()
    {

    }
    public Index(int r, int c)
    {
        row = r;
        col = c;
    }
    public int getRow()
    {
        return row;
    }
    public void setRow(int r)
    {
        row = r;
    }
    public int getCol()
    {
        return col;
    }
    public void setCol(int c)
    {
        col = c;
    }
    public bool getOpposite(List<Index> o)
    {
        foreach (Index i in o)
        {
            if (i.getRow() == row && i.getCol() == col * -1)
            {
                return true;
            }
            if (i.getCol() == col && i.getRow() == row * -1)
            {
                return true;
            }
        }
        return false;
    }

    public override string ToString()
    {
        return "("+row+","+col+")";
    }
}
