using System;
using System.Collections.Generic;
using UnityEngine;

public class statics
{
    public enum PATH { NONE, EXIT, END };
    public enum TILES { SINGLE, DOUBLE, TRIPLE, START };
    public enum TILESTATUS { OPEN, CLOSE };
    public enum DIRECTIONS { UP, DOWN, LEFT, RIGHT };
    public enum WEAPONSETTINGS { CLOSESTTOEND, CLOSESTTOWEAPON, MOSTHEALTH };
    public enum MATERIALLEVELS { REGULAR, MID, DIM, INTENSITY2, BLOCKHDR4, PULSEHDR2To15 };
    public enum CAMERAMODE { LOCK, PAN, ROTATE };
    public enum OBSERVERTYPES { STRING, MATERIAL };

    public List<int> ends = new List<int>() { 8,9,10,11 };



    public static bool cellExists2DArray<T>(int row, int col, T[,] array)
    {
        if ((row < 0) || (row >= array.GetLength(0)) || (col < 0) || (col >= array.GetLength(1)))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public static void SetLayerRecursively(GameObject gameObject, string layer)
    {
        int LayerIgnoreRaycast = LayerMask.NameToLayer(layer);
        gameObject.layer = LayerIgnoreRaycast;

        foreach (Transform child in gameObject.transform)
        {
            SetLayerRecursively(child.gameObject, layer);
        }
    }
}
