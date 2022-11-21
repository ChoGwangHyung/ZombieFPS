using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BeginStageUi : MonoBehaviour
{
    [SerializeField] private Text explainText = null;

    [SerializeField] private string nextScene = null;
    [SerializeField] private List<string> text = new List<string>();
    private int textIndex = 0;

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        explainText.text = text[textIndex];
    }

    public void NextButton()
    {
        textIndex++;
        if (textIndex == text.Count)
            SceneManager.LoadScene(nextScene);
        else explainText.text = text[textIndex];
    }

    public void SkipButton()
    {
        SceneManager.LoadScene(nextScene);
    }
}