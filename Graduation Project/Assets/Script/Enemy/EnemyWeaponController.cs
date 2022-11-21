using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponController : WeaponController
{
    private EnemyRangedWeapon curRangedWeapon = null;

    [SerializeField] private EnemyRangedWeapon rangeWeapon = null;

    void Awake()
    {
        curRangedWeapon = rangeWeapon;
    }

    public void Fire() { curRangedWeapon.Fire(); }
    public void Reload() { curRangedWeapon.Reload(); }
    public bool CanFire() { return curRangedWeapon.CanFire(); }
    public bool IsReload() { return curRangedWeapon._isReload; }
}
