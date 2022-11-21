using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static GameController gameController;
    public static GameController instance
    {
        get
        {
            if (gameController == null)
                gameController = FindObjectOfType<GameController>();
            return gameController;
        }
    }

    public delegate void ControllGame();
    public ControllGame StopGame;
    public ControllGame PlayGame;

    public ControllGame StopController;
    public ControllGame PlayController;

    void Start()
    {
        StopController += Player.instance.StopPlayer;
        StopController += MainCamera.instance.StopCamera;

        PlayController += Player.instance.PlayPlayer;
        PlayController += MainCamera.instance.PlayCamera;

        StopGame += UiController.instance.StopUiController;
        StopGame += StopController;

        PlayGame += UiController.instance.PlayUiController;
        PlayGame += PlayController;
    }
}
