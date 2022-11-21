using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    private static UiController uiController;
    public static UiController instance
    {
        get
        {
            if (uiController == null)
                uiController = FindObjectOfType<UiController>();
            return uiController;
        }
    }

    //Ui 관련
    [SerializeField] private BulletHud bulletHud = null;
    [SerializeField] private CriticalTexts criticalText = null;
    [SerializeField] private Option option = null;
    [SerializeField] private PlayerStateUi playerStateUi = null;
    public PlayerStateUi _playerStateUi { get { return playerStateUi; } }
    [SerializeField] private PlayerDead playerDeadUi = null;
    public PlayerDead _playerDeadUi { get { return playerDeadUi; } }

    private bool canOptionOpen = true;

    void Update()
    {
        WeaponUi();
        OpenOptionWindow();
    }

    private void WeaponUi()
    {
        bulletHud.UpdateText(Player.instance._weaponController._curRangedWeapon._curBulletInMagazine, Player.instance._weaponController._curRangedWeapon._curBulletInBag);
    }

    private void OpenOptionWindow()
    {
        if (Input.GetKeyDown(KeyCode.P) && canOptionOpen == true)
        {
            option.gameObject.SetActive(!option.gameObject.activeSelf);
            if (option.gameObject.activeSelf == true)
                GameController.instance.StopController();
            else
                GameController.instance.PlayController();
        }
    }

    public void HitCritical() { criticalText.HitCritical(); }
    public void ChangeWeaponImage(string name) { bulletHud.ChangeWeapon(name); }

    public void StopUiController() { canOptionOpen = false; }
    public void PlayUiController() { canOptionOpen = true; }

    public IEnumerator ImageAlphaColorChangeCoroutine(Image changeImage, float goalValue, float changeValue)
    {
        Color temp = changeImage.color;
        temp.a = 0.0f;
        changeImage.color = temp;

        while (temp.a < goalValue)
        {            
            temp.a += changeValue;
            changeImage.color = temp;
            yield return null;
        }        
    }

    public IEnumerator TextAlphaColorChangeCoroutine(Text changeText, float goalValue, float changeValue)
    {
        Color temp = changeText.color;
        temp.a = 0.0f;
        changeText.color = temp;

        while (temp.a < goalValue)
        {
            temp.a += changeValue;
            changeText.color = temp;
            yield return null;
        }
    }
}
