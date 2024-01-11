using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static statics;
using Random = UnityEngine.Random;

public class WFCNode : MonoBehaviour
{
    [SerializeField] int index = 0 , row = 0, col = 0;
    [SerializeField] List<int> possibleNeighboursUP, possibleNeighboursDOWN, possibleNeighboursLEFT, possibleNeighboursRIGHT;
    [SerializeField] bool exitUP, exitDOWN, exitLEFT, exitRIGHT, entryUP, entryDOWN, entryLEFT, entryRIGHT;
    [SerializeField] Dictionary<DIRECTIONS, bool> exits = new Dictionary<DIRECTIONS, bool>(), entries = new Dictionary<DIRECTIONS, bool>();

    // Start is called before the first frame update
    void Awake()
    {
        if (exitUP)
        {
            exits.Add(DIRECTIONS.UP, true);
        }
        if (exitDOWN)
        {
            exits.Add(DIRECTIONS.DOWN, true);
        }
        if (exitLEFT)
        {
            exits.Add(DIRECTIONS.LEFT, true);
        }
        if (exitRIGHT)
        {
            exits.Add(DIRECTIONS.RIGHT, true);
        }

        if (entryUP)
        {
            entries.Add(DIRECTIONS.UP, true);
        }
        if (entryDOWN)
        {
            entries.Add(DIRECTIONS.DOWN, true);
        }
        if (entryLEFT)
        {
            entries.Add(DIRECTIONS.LEFT, true);
        }
        if (entryRIGHT)
        {
            entries.Add(DIRECTIONS.RIGHT, true);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public List<int> getPossibleNeighboursUP()
    {
        return possibleNeighboursUP;
    }
    public List<int> getPossibleNeighboursDOWN()
    {
        return possibleNeighboursDOWN;
    }
    public List<int> getPossibleNeighboursLEFT()
    {
        return possibleNeighboursLEFT;
    }
    public List<int> getPossibleNeighboursRIGHT()
    {
        return possibleNeighboursRIGHT;
    }

    public void addPossibleNeighboursUP(int i)
    {
        possibleNeighboursUP.Add(i);
    }
    public void addPossibleNeighboursDOWN(int i)
    {
        possibleNeighboursDOWN.Add(i);
    }
    public void addPossibleNeighboursLEFT(int i)
    {
        possibleNeighboursLEFT.Add(i);
    }
    public void addPossibleNeighboursRIGHT(int i)
    {
        possibleNeighboursRIGHT.Add(i);
    }

    public Dictionary<DIRECTIONS, bool> getExits()
    {
        return exits;
    }
    public List<DIRECTIONS> getExitList()
    {
        return Enumerable.ToList(exits.Keys);
    }

    public void RemoveExit(DIRECTIONS d)
    {
        exits.Remove(d);

        switch (d)
        {
            case (DIRECTIONS.UP):
                exitUP = false;
                break;
            case (DIRECTIONS.DOWN):
                exitDOWN = false;
                break;
            case (DIRECTIONS.LEFT):
                exitLEFT = false;
                break;
            case (DIRECTIONS.RIGHT):
                exitRIGHT = false;
                break;
        }
    }

    public int getExitCount()
    {
        return exits.Count;
    }

    public DIRECTIONS getRandomExit()
    {
        List<DIRECTIONS> keys = Enumerable.ToList(exits.Keys);
        return keys[Random.Range(0, keys.Count)];
    }

    public void setIndex(int r, int c)
    {
        row = r;
        col = c;
    }

    public int getIndex()
    {
        return index;
    }

    public bool getExit(DIRECTIONS d)
    {
        if (exits.ContainsKey(d))
        {
            return exits[d];
        }
        else
        {
            return false;
        }
    } 

    public bool getEntry(DIRECTIONS d)
    {
        if (entries.ContainsKey(d))
        {
            return entries[d];
        }
        else
        {
            return false;
        }
    } 

    public bool getExitUp()
    {
        return exitUP;
    }

    public bool getExitDown()
    {
        return exitDOWN;
    }

    public bool getExitLeft()
    {
        return exitLEFT;
    }

    public bool getExitRight()
    {
        return exitRIGHT;
    }

    public void clearNeighbours()
    {
        possibleNeighboursDOWN.Clear();
        possibleNeighboursLEFT.Clear();
        possibleNeighboursRIGHT.Clear();
        possibleNeighboursUP.Clear();
    }
}
