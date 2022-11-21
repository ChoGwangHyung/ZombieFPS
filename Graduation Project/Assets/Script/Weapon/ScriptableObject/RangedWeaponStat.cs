using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ranged", menuName = "Weapon Stat/Ranged")]
public class RangedWeaponStat : ScriptableObject
{
    [SerializeField] private string weaponName = null;
    public string _weaponName { get { return weaponName; } }

    [SerializeField] private float damage = default;
    public float _damage { get { return damage; } }

    [SerializeField] private float range = default;
    public float _range { get { return range; } }

    [SerializeField] private float speed = default;
    public float _speed { get { return speed; } }

    [SerializeField] private float accuracy = default;
    public float _accuracy { get { return accuracy; } }

    [SerializeField] private float fireCooltime = default;
    public float _fireColltime { get { return fireCooltime; } }

    [SerializeField] private float reloadTime = default;
    public float _reloadTime { get { return reloadTime; } }

    [SerializeField] private float recoilActionForce = default;
    public float _recoilActionForce { get { return recoilActionForce; } }

    [SerializeField] private int maxBulletInMagazine = default;
    public int _maxBulletInMagazine { get { return maxBulletInMagazine; } }
    [SerializeField] private int maxBulletInBag = default;
    public int _maxBulletInBag { get { return maxBulletInBag; } }
}