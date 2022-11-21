using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GameData
{
    public float soundVol;

    public GameData()
    {
        soundVol = 1.0f;
    }

    public void LoadData()
    {
        AudioController.instance.SetVolume(soundVol);
    }

    public void SaveData()
    {
        soundVol = AudioController.instance._volume;
    }
}
