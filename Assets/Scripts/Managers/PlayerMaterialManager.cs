using Assets.Scripts.Interfaces.Observer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static statics;

public class PlayerMaterialManager : MonoBehaviour, Subject
{
    List<Observer> stringObservers = new List<Observer>(), materialObservers = new List<Observer>();
    static string HDRPath = "Materials/HDR", HDRMidPath = "Materials/HDRMid", HDRDimPath = "Materials/HDRDim",
        HDRIntensity2Path = "Materials/HDRIntensity2", BlockHDR4Path = "Materials/BlockHDR4", PulseHDR2To15Path = "Materials/PulseHDR2-15";
    static Object[] HDR, HDRMid, HDRDim, HDRIntensity2, BlockHDR4, PulseHDR2To15;
    static int index = 4;
    void Awake()
    {
        HDR = Resources.LoadAll(HDRPath);
        HDRMid = Resources.LoadAll(HDRMidPath);
        HDRDim = Resources.LoadAll(HDRDimPath);
        HDRIntensity2 = Resources.LoadAll(HDRIntensity2Path);
        BlockHDR4 = Resources.LoadAll(BlockHDR4Path);
        PulseHDR2To15 = Resources.LoadAll(PulseHDR2To15Path);
    }

    public static Material getHDRMaterial()
    {
        return HDR[index] as Material;
    }
    public static Material getHDRMidMaterial()
    {
        return HDRMid[index] as Material;
    }
    public static Material getHDRDimMaterial()
    {
        return HDRDim[index] as Material;
    }
    public static Material getHDRIntensity2Material()
    {
        return HDRIntensity2[index] as Material;
    }
    public static Material getBlockHDR4()
    {
        return BlockHDR4[(int)(index/4)] as Material;
    }
    public static Material getPulseHDR2To15()
    {
        return PulseHDR2To15[(int)(index/4)] as Material;
    }

    public void incrementIndex()
    {
        index++;
        if (index >= HDRDim.Length)
        {
            index = 0;
        }
        indexChange();
    }
    public void decrementIndex()
    {
        index--;
        if (index < 0)
        {
            index = HDR.Length-1;
        }
        indexChange();
    }

    void indexChange()
    {
        UpdateObservers(OBSERVERTYPES.STRING);
        UpdateObservers(OBSERVERTYPES.MATERIAL);
    }
    public static int getIndex()
    {
        return index;
    }

    public void AddObserver(OBSERVERTYPES ot, Observer observer)
    {
        switch (ot)
        {
            case OBSERVERTYPES.STRING:
                stringObservers.Add(observer);
                break;
            case OBSERVERTYPES.MATERIAL:
                materialObservers.Add(observer);
                break;
        }
    }

    public void RemoveObserver(OBSERVERTYPES ot, Observer observer)
    {
        switch (ot)
        {
            case OBSERVERTYPES.STRING:
                stringObservers.Remove(observer);
                break;
            case OBSERVERTYPES.MATERIAL:
                materialObservers.Remove(observer);
                break;
        }
    }

    public void ClearObservers(OBSERVERTYPES ot)
    {
        switch (ot)
        {
            case OBSERVERTYPES.STRING:
                stringObservers.Clear();
                break;
            case OBSERVERTYPES.MATERIAL:
                materialObservers.Clear();
                break;
        }
    }

    public void UpdateObservers(OBSERVERTYPES ot)
    {
        List<Observer> observers = new List<Observer>();

        switch (ot)
        {
            case OBSERVERTYPES.STRING:
                observers = stringObservers;
                break;
            case OBSERVERTYPES.MATERIAL:
                observers = materialObservers;
                break;
        }

        foreach (Observer observer in observers)
        {
            observer.UpdateObserver((index + 1) + " / " + HDR.Length);
        }
    }
}
