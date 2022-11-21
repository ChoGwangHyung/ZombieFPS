using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseSensitivity : MonoBehaviour
{
    [SerializeField] private Image backGround = null;
    [SerializeField] private Text mouseSensitivity = null;

    private bool isCoroutineStart = false;

    void Start()
    {
        backGround.gameObject.SetActive(false);
        mouseSensitivity.gameObject.SetActive(false);
    }

    public void ControlMouseSensitivity(float sensitivityVal)
    {
        if (isCoroutineStart == true)
            StopAllCoroutines();
        StartCoroutine(ShowAndVanishCouroutine(sensitivityVal));
    }

    private IEnumerator ShowAndVanishCouroutine(float sensitivityVal)
    {
        isCoroutineStart = true;
        backGround.gameObject.SetActive(true);
        mouseSensitivity.gameObject.SetActive(true);

        mouseSensitivity.text = "마우스 감도 : " + (int)sensitivityVal;

        Color backGroundColor = backGround.color;
        Color textColor = mouseSensitivity.color;
        backGroundColor.a = 1.0f;
        textColor.a = 1.0f;
        backGround.color = backGroundColor;
        mouseSensitivity.color = textColor;

        yield return new WaitForSeconds(2.0f);

        while (textColor.a > 0.0f || backGroundColor.a > 0.0f)
        {
            backGroundColor.a -= 0.005f;
            textColor.a -= 0.005f;

            backGround.color = backGroundColor;
            mouseSensitivity.color = textColor;

            yield return null;
        }

        isCoroutineStart = false;
        backGround.gameObject.SetActive(false);
        mouseSensitivity.gameObject.SetActive(false);
    }
}
