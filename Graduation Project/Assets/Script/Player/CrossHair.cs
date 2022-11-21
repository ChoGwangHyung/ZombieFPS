using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossHair : MonoBehaviour
{
    private static CrossHair crossHair;
    public static CrossHair instance
    {
        get
        {
            if (crossHair == null)
                crossHair = FindObjectOfType<CrossHair>();
            return crossHair;
        }
    }

    private Animator ani;
    [SerializeField] private Image zoomMode = null;
    [SerializeField] private GameObject originMode = null;

    [SerializeField] private Image[] originModeImage = null;
    [SerializeField] private Image zoomModeImage = null;

    void Start()
    {
        ani = originMode.GetComponent<Animator>();
        zoomMode.gameObject.SetActive(false);
    }

    public void SetCrouchAnimation(bool state) { ani.SetBool("IsCrouch", state); }
    public void SetWalkAnimation(bool state) { ani.SetBool("IsWalk", state); }
    public void SetRunAnimation(bool state) { ani.SetBool("IsRun", state); }

    public void StartFireAnimation() { ani.SetBool("IsFire", true); }
    public void StopFireAnimation() { ani.SetBool("IsFire", false); }

    public void ActiveZoomMode()
    {
        zoomMode.gameObject.SetActive(true);
        originMode.SetActive(false);
    }

    public void DeactiveZoomMode()
    {
        zoomMode.gameObject.SetActive(false);
        originMode.SetActive(true);
    }

    public void IsEnemyLocateCrosshair(bool isLocated)
    {
        if (isLocated == false)
        {
            if (originMode.activeSelf == false)
                zoomModeImage.color = Color.white;
            for (int index = 0; index < originModeImage.Length; index++)
                originModeImage[index].color = Color.white;
        }
        else
        {            
            if (originMode.activeSelf == false)
                zoomModeImage.color = Color.red;
            for (int index = 0; index < originModeImage.Length; index++)
                originModeImage[index].color = Color.red;
        }
    }
}
