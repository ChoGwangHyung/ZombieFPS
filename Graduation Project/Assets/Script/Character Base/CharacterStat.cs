using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character Stat")]
public class CharacterStat : ScriptableObject
{
    [SerializeField] private float maxLife = default;
    public float _maxLife { get { return maxLife; } }

    [SerializeField] private float walkSpeed = default;
    public float _walkSpeed { get { return walkSpeed; } }
}
