using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Assets.Scripts.Interfaces.Observer;
using static statics;

public class TextBoxObserver : MonoBehaviour, Observer
{
    [SerializeField] string subjectName = "Material";
    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        (player.transform.Find(subjectName).GetComponent(typeof(Subject)) as Subject).AddObserver(OBSERVERTYPES.MATERIAL, this);
    }

    public void UpdateObserver(string s)
    {
        GetComponent<TextMeshProUGUI>().text = s;
    }

    public void UpdateObserver<T>(T info)
    {
        string s = (info as string);
        GetComponent<TextMeshProUGUI>().text = s;
    }
}
