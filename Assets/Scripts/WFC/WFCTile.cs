using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static statics;

public class WFCTile: MonoBehaviour
{
    GameObject[] allNodes;
    Dictionary<int, GameObject> possibleNodes = new Dictionary<int, GameObject>();
    Index index;
    GameObject node;
    int nodeIndex = -1;
    public WFCTile(GameObject[] allNodes, int row, int col)
    {
        this.allNodes = allNodes;
        for (int i=0; i<allNodes.Length; i++)
        {
            possibleNodes.Add(i, allNodes[i]);
        }
        index = new Index(row, col);
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

    public void collapse()
    {
        List<int> keys = Enumerable.ToList(possibleNodes.Keys);
        Debug.Log("(" + index.getRow() + "," + index.getCol() + ")");
        Debug.Log(possibleNodes.Count);
        Debug.Log(keys.Count);
        lockIn(keys[Random.Range(0, keys.Count)]);
    }

    public void lockIn(int lockInIndex)
    {
        nodeIndex = lockInIndex;
        node = possibleNodes[nodeIndex];
        possibleNodes = new Dictionary<int, GameObject>();
        possibleNodes.Add(nodeIndex, node);
        Instantiate(node, new Vector3(index.getCol() * 15, 0, index.getRow() * 15), allNodes[nodeIndex].transform.rotation);
    }

    public bool propogate( WFCTile tile, DIRECTIONS d)
    {
        bool changed = false;
        List<int> incomingAllPNodes = tile.getPossibleNodes();
        List<int> allKeys = new List<int>();

        foreach (int possibility in incomingAllPNodes)
        {
            List<int> allP = getPossibleNeighbourList(possibility, d);
            foreach (int i in allP)
            {
                if (!allKeys.Contains(i))
                {
                    allKeys.Add(i);

                }
            }
        }

        //Debug.Log("(" + index.getRow() + ", " + index.getCol() + ") AK = " + allKeys.Count);
        //printList(allKeys);
        List<int> pnkeys = Enumerable.ToList(possibleNodes.Keys);
        Dictionary<int,GameObject> temp = new Dictionary<int, GameObject>();

        foreach (int i in allKeys)
        {
            if (pnkeys.Contains(i))
            {
                temp.Add(i, possibleNodes[i]);
            }
        }

        List<int> tempkeys = Enumerable.ToList(temp.Keys);
        if (tempkeys.Count != pnkeys.Count)
        {
            possibleNodes = temp;
            //printList(tempkeys);
            //Debug.Log("^^^New KEYS^^^");
            changed = true;
        }


        if (tempkeys.Count == 1)
        {
            Debug.Log("Proccess of elimination collapse");
            collapse();
        }

        return changed;
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

    List<int> getPossibleNeighbourList(int index,DIRECTIONS d)
    {
        switch (d) 
        {
            default:
                return allNodes[index].GetComponent<WFCNode>().getPossibleNeighboursUP();
            case(DIRECTIONS.DOWN):
                return allNodes[index].GetComponent<WFCNode>().getPossibleNeighboursDOWN();
            case(DIRECTIONS.LEFT):
                return allNodes[index].GetComponent<WFCNode>().getPossibleNeighboursLEFT();
            case(DIRECTIONS.RIGHT):
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
        foreach (T item in list )
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

}
