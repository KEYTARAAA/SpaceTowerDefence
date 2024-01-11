using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static statics;

public class WFCTile2 : MonoBehaviour
{
    GameObject[] allNodes;
    Dictionary<int, GameObject> possibleNodes = new Dictionary<int, GameObject>();
    Index index;
    GameObject node;
    int nodeIndex = -1;
    static int maxRow, maxCol;
    static GameObject map;
    Dictionary<DIRECTIONS, bool> neighbours = new Dictionary<DIRECTIONS, bool>() { [DIRECTIONS.UP] = false, [DIRECTIONS.DOWN] = false, [DIRECTIONS.LEFT] = false, [DIRECTIONS.RIGHT] = false };

    List<int> ends = new List<int>() { 8, 9, 10, 11 };
    public WFCTile2(GameObject[] allNodes, int row, int col)
    {
        this.allNodes = allNodes;
        for (int i = 0; i < allNodes.Length; i++)
        {
            possibleNodes.Add(i, allNodes[i]);
        }
        index = new Index(row, col);
    }

    public static void setMaxRow(int r)
    {
        maxRow = r;
    }
    public static void setMaxCol(int c)
    {
        maxCol = c;
    }
    public static void setMap(GameObject m)
    {
        map = m;
    }

    public Index getIndex()
    {
        return index;
    }

    public int possibleNodeCount()
    {
        return possibleNodes.Count;
    }
    public List<int> getPossibleNodes()
    {
        return Enumerable.ToList(possibleNodes.Keys);
    }

    public void collapse(List<DIRECTIONS> closeExits)
    {
        List<int> keys = Enumerable.ToList(possibleNodes.Keys);

        /*Debug.Log("(" + index.getRow() + "," + index.getCol() + ")");
        Debug.Log(possibleNodes.Count);
        Debug.Log(keys.Count);*/
        int r = Random.Range(0, keys.Count);
        /*Debug.Log("=====================");
        Debug.Log("R = " + r );
        Debug.Log("keys.count = " + keys.Count );

        Debug.Log("=====================");*/
        lockIn(keys[r], closeExits);
    }

    public void lockIn(int lockInIndex, List<DIRECTIONS> closeExits)
    {
        nodeIndex = lockInIndex;
        node = possibleNodes[nodeIndex];
        possibleNodes.Clear();
        possibleNodes.Add(nodeIndex, node);
        node = Instantiate(node, new Vector3(index.getCol() * 15, 0, index.getRow() * 15), allNodes[nodeIndex].transform.rotation);
        node.transform.parent = map.transform;
        GetNode().setIndex(index.getRow(), index.getCol());
        //Debug.Log("EXITS");
        //Debug.Log(GetNode().getExitCount());

        /*switch (d) 
        {
            case (DIRECTIONS.UP):
                GetNode().RemoveExit(DIRECTIONS.DOWN);
                break;
            case (DIRECTIONS.DOWN):
                GetNode().RemoveExit(DIRECTIONS.UP);
                break;
            case (DIRECTIONS.LEFT):
                GetNode().RemoveExit(DIRECTIONS.RIGHT);
                break;
            case (DIRECTIONS.RIGHT):
                GetNode().RemoveExit(DIRECTIONS.LEFT);
                break;
        }*/

        foreach (DIRECTIONS d in closeExits)
        {
            GetNode().RemoveExit(d);
        }

        if (index.getRow() == 0)
        {
            GetNode().RemoveExit(DIRECTIONS.DOWN);
        }
        else if (index.getRow() == maxRow)
        {
            GetNode().RemoveExit(DIRECTIONS.UP);
        }

        if (index.getCol() == 0)
        {
            GetNode().RemoveExit(DIRECTIONS.LEFT);
        }
        else if (index.getCol() == maxCol)
        {
            GetNode().RemoveExit(DIRECTIONS.RIGHT);
        }

        foreach (Transform child in GetNode().transform)
        {
            child.GetComponent<MaterialManager>().addMaterial(PlayerMaterialManager.getHDRMidMaterial(), true, false);
        }
    }

    public void propogate(List<List<int>> tiles, List<DIRECTIONS> closeExits)
    {
        Dictionary<int, int> temp = new Dictionary<int, int>();

        foreach (List<int> t in tiles)
        {
            foreach (int i in t)
            {
                if (temp.ContainsKey(i))
                {
                    temp[i] = temp[i] + 1;
                }
                else
                {
                    temp.Add(i, 1);
                }
            }
        }


        List<int> keysToRemove = new List<int>();

        foreach (int i in temp.Keys)
        {
            if (temp[i] < tiles.Count)
            {
                keysToRemove.Add(i);
            }
        }

        foreach (int i in keysToRemove)
        {
            temp.Remove(i);

        }

        keysToRemove.Clear();

        foreach (int i in possibleNodes.Keys)
        {
            if (!temp.ContainsKey(i))
            {
                keysToRemove.Add(i);
            }
        }

        foreach (int i in keysToRemove)
        {
                possibleNodes.Remove(i);

        }


        int count = 0;
        foreach (int i in possibleNodes.Keys)
        {
            if (ends.Contains(i))
            {
                count++;
            }
        }

        if (count < possibleNodes.Keys.Count)
        {
            foreach (int i in ends)
            {
                possibleNodes.Remove(i);
            }
        }



        /*List<int> temp = new List<int>() { 0,1,2,3,4,5,6,7,8,9,10,11 };
        foreach (int[] t in tiles)
        {
            foreach (int i in t)
            {
                //possibleNodes.Remove(i);
                temp.Remove(i);
            }
        }

        foreach (int i in temp)
        {
            possibleNodes.Remove(i);
        }*/
        //printPossibilities();
        collapse(closeExits);
    }
    /*public bool propogate( WFCTile tile, DIRECTIONS d)
    {
        bool changed = false;
        List<int> toUpdateKeys = tile.getPossibleNodes();
        List<int> allKeys = new List<int>();
        foreach (int possibility in toUpdateKeys)
        {
            int[] allP = getPossibleNeighbourList(possibility, d);
            foreach (int i in allP)
            {
                if (!allKeys.Contains(i))
                {
                    allKeys.Add(i);

                }
            }
        }
        if (index.getRow() == 1)
        {
            printList(allKeys);

        }
        int[] changePossibilities = allKeys.ToArray();

        //foreach (int possibility in toUpdateKeys) {
            //int[] changePossibilities = getPossibleNeighbourList(possibility, d);
            List<int> keys = Enumerable.ToList(possibleNodes.Keys);
            Dictionary<int, GameObject> temp = new Dictionary<int, GameObject>();
            foreach (int i in changePossibilities)
            {
                if (keys.Contains(i))
                {
                    temp.Add(i, possibleNodes[i]);
                }
            }

            List<int> tempKeys = Enumerable.ToList(temp.Keys);

            if (tempKeys.Count == 1)
            {
                Debug.Log("Proccess of elimination collapse");
                collapse();
            }

            if (tempKeys.Count != keys.Count)
            {
                changed = true;
                //Debug.Log(tempKeys.Count);
                possibleNodes = temp;
            }
        //}

        return changed;
    }*/

    List<int> getPossibleNeighbourList(int index, DIRECTIONS d)
    {
        switch (d)
        {
            default:
                return allNodes[index].GetComponent<WFCNode>().getPossibleNeighboursUP();
            case (DIRECTIONS.DOWN):
                return allNodes[index].GetComponent<WFCNode>().getPossibleNeighboursDOWN();
            case (DIRECTIONS.LEFT):
                return allNodes[index].GetComponent<WFCNode>().getPossibleNeighboursLEFT();
            case (DIRECTIONS.RIGHT):
                return allNodes[index].GetComponent<WFCNode>().getPossibleNeighboursRIGHT();
        }

    }

    public void printPossibilities()
    {
        string s = "";
        s += "(" + index.getRow() + "," + index.getCol() + ")\n\n [ ";

        List<int> keys = Enumerable.ToList(possibleNodes.Keys);
        foreach (int i in keys)
        {
            s += i + ", ";
        }
        s += "]\n______________________________________";
        Debug.Log(s);

    }

    void printList<T>(List<T> list)
    {
        string s = "(" + index.getRow() + "," + index.getCol() + ")\n\n { ";
        foreach (T item in list)
        {
            s += item + " ,";
        }
        s += " }\n________________________________";

        Debug.Log(s);
    }

    public int getNodeIndex()
    {
        return nodeIndex;
    }

    public WFCNode GetNode()
    {
        return node.GetComponent<WFCNode>();
    }

    public override string ToString()
    {
        return index.ToString() + " -> " + nodeIndex;
    }

}
