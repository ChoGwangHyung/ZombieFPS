using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Melee", menuName = "Weapon Stat/Melee")]
public class MeleeWeaponStat : ScriptableObject
{
    [SerializeField] private string weaponName = null;
    public string _weaponName { get { return weaponName; } }

    [SerializeField] private float damage = default;
    public float _damage { get { return damage; } }

    [SerializeField] private float range = default;
    public float _range { get { return range; } }

    [SerializeField] private float attackCoolTime = default;
    public float _attackCoolTime { get { return attackCoolTime; } }

    [SerializeField] private float attackStartTime = default;
    public float _attackStartTime { get { return attackStartTime; } }

    [SerializeField] private float attackEndTime = default;
    public float _attackEndTime { get { return attackEndTime; } }
}