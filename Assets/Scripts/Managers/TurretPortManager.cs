using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPortManager : MonoBehaviour
{
    bool occupied = false;
    
    public bool getOccupied()
    {
        return occupied;
    }
    public void setOccupied( bool o)
    {
        occupied = o;
    }
}
