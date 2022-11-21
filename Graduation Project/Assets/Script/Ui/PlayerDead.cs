using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerDead : MonoBehaviour
{
    [SerializeField] private GameObject images = null;

    [SerializeField] private Image backGround = null;

    [SerializeField] private Image restartButton = null;
    [SerializeField] private Text restartText = null;

    [SerializeField] private Image goTitleButton = null;
    [SerializeField] private Text goTitleText = null;

    [SerializeField] private Image failTextBackGround = null;
    [SerializeField] private Image failTextFrame = null;
    [SerializeField] private Text failText = null;

    void Start()
    {
        InitImageAlphaColor(backGround);
        InitImageAlphaColor(restartButton);
        InitTextAlphaColor(restartText);
        InitImageAlphaColor(goTitleButton);
        InitTextAlphaColor(goTitleText);
        InitImageAlphaColor(failTextBackGround);
        InitImageAlphaColor(failTextFrame);
        InitTextAlphaColor(failText);        
        images.SetActive(false);
    }

    private void InitTextAlphaColor(Text text)
    {
        Color temp = text.color;
        temp.a = 0.0f;
        text.color = temp;
    }

    private void InitImageAlphaColor(Image image)
    {
        Color temp = image.color;
        temp.a = 0.0f;
        image.color = temp;
    }

    public void PlayerDeadUiOn()
    {
        images.SetActive(true);

        StartCoroutine(UiController.instance.ImageAlphaColorChangeCoroutine(backGround, 1.0f, 0.01f));

        StartCoroutine(UiController.instance.ImageAlphaColorChangeCoroutine(restartButton, 1.0f, 0.01f));
        StartCoroutine(UiController.instance.TextAlphaColorChangeCoroutine(restartText, 1.0f, 0.01f));

        StartCoroutine(UiController.instance.ImageAlphaColorChangeCoroutine(goTitleButton, 1.0f, 0.01f));
        StartCoroutine(UiController.instance.TextAlphaColorChangeCoroutine(goTitleText, 1.0f, 0.01f));

        StartCoroutine(UiController.instance.ImageAlphaColorChangeCoroutine(failTextBackGround, 1.0f, 0.01f));
        StartCoroutine(UiController.instance.ImageAlphaColorChangeCoroutine(failTextFrame, 1.0f, 0.01f));
        StartCoroutine(UiController.instance.TextAlphaColorChangeCoroutine(failText, 1.0f, 0.01f));
    }

    public void PlayerDeadAndRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PlayerDeadAndGoTitle()
    {
        SceneManager.LoadScene("Title");
    }
}
