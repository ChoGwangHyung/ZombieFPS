using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRangedWeapon : RangedWeapon
{
    //줌모드
    [SerializeField] private SkinnedMeshRenderer hand = null;
    [SerializeField] private GameObject weaponShape = null;
    private bool isZoomMode = false;

    //사격
    public override void Fire()
    {
        base.Fire();

        if (isZoomMode == false)
            flash.Play();

        Bullets.Fire(MainCamera.instance.gameObject, "Player");

        MainCamera.instance.DoRecoilAction(weaponStat._recoilActionForce);
    }

    //재장전
    public override void Reload()
    {
        base.Reload();

        StopZoom();
        StartCoroutine(ReloadCoroutine());
    }

    //줌 모드
    public void Zoom()
    {
        if (isReload == true) return;

        isZoomMode = !isZoomMode;

        if (isZoomMode == true)
        {
            MainCamera.instance.StartCameraZoom();
            CrossHair.instance.ActiveZoomMode();
        }
        else
        {
            MainCamera.instance.StopCameraZoom();
            CrossHair.instance.DeactiveZoomMode();
        }

        hand.enabled = !isZoomMode;
        weaponShape.SetActive(!isZoomMode);
    }

    //줌모드 취소
    public void StopZoom()
    {
        if (isZoomMode == true)
            Zoom();
    }

    public void DoWeaponChange()
    {
        if (isReload == true)
        {
            StopAllCoroutines();
            isReload = false;
        }

        if (isZoomMode == true)
        {
            StopZoom();
        }
    }

    public float GetReloadTime() { return weaponStat._reloadTime; }   
}
