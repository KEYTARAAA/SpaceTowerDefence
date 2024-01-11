using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PauseManager
{

    public static void Pause()
    {
        Time.timeScale = 0;
    }
    public static void Play()
    {
        Time.timeScale = 1;
    }
}
