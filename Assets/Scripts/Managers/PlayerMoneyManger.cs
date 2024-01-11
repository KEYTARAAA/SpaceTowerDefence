using Assets.Scripts.Interfaces.Observer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static statics;

public class PlayerMoneyManger : MonoBehaviour, Subject
{
    static int money = 1000;
    static bool changed = false;
    static List<Observer> observers = new List<Observer>();

    private void Start()
    {
        setMoney(100);
    }
    public static int getMoney()
    {
        return money;
    }
    public static void setMoney( int m)
    {
        money = m;
        changed = true;
    }
    public static void increaseMoney( int m)
    {
        money += m;
        changed = true;
    }
    public static void decreaseMoney( int m)
    {
        money -= m;
        changed = true;
    }

    private void FixedUpdate()
    {
        if (changed)
        {
            changed = false;
            UpdateObservers(OBSERVERTYPES.STRING);
        }
        else
        {
            return;
        }
    }

    public void AddObserver(OBSERVERTYPES ot, Observer observer)
    {
        observers.Add(observer);
    }

    public void RemoveObserver(OBSERVERTYPES ot, Observer observer)
    {
        observers.Remove(observer);
    }

    public void ClearObservers(OBSERVERTYPES ot)
    {
        observers.Clear();
    }

    public void UpdateObservers(OBSERVERTYPES ot)
    {
        foreach(Observer observer in observers)
        {
            observer.UpdateObserver(":" +money.ToString());
        }
    }
}
