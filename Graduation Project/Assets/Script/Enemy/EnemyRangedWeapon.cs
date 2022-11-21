using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedWeapon : RangedWeapon
{
    [SerializeField] private GameObject dirObject = null;

    //사격
    public override void Fire()
    {
        base.Fire();

        flash.Play();
        Bullets.Fire(dirObject, "Enemy");
    }

    //재장전
    public override void Reload()
    {
        base.Reload();

        StartCoroutine(ReloadCoroutine());
    }
}
