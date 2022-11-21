using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeWeapon : MeleeWeapon
{   
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        AudioController.instance.AddAudioSource(audioSource);
        targetObjectName = "Player";
    }

    protected override void ProcessHiting(RaycastHit hitInfo)
    {
        hitInfo.collider.gameObject.GetComponent<CharacterBase>().ReceiveDamage(weaponStat._damage, hitInfo.point);
    }
}
