using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleUi : MonoBehaviour
{
    [SerializeField] private GameObject OptionWindow = null;

    void Start()
    {
        OptionWindow.SetActive(false);
    }

    public void StartButton()
    {
        SceneManager.LoadScene("Begin Stage 1");
    }

    public void TraniningButton()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void OptionButton()
    {
        OptionWindow.SetActive(true);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
