using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerEnemy : Enemy
{
    [SerializeField] private AudioClip explosionAudio = null;

    [SerializeField] private ParticleSystem explosion = null;
    [SerializeField] private ParticleSystem poison = null;

    [SerializeField] private SkinnedMeshRenderer shape = null;
    [SerializeField] private GameObject hitCol = null;
    private float readyToBoomTime = 0.5f;
    private float explosionRange = 6.0f;

    override protected void AnimatorSetting()
    {
        base.AnimatorSetting();
    }

    override protected void Die()
    {
        StartCoroutine(BoomCoroutine());
    }

    private IEnumerator BoomCoroutine()
    {
        enemyAi.enabled = false;
        if (readyToBoomTime > 0)
            ani.SetTrigger("BoomTrigger");

        yield return new WaitForSeconds(readyToBoomTime);
        hitCol.GetComponent<CapsuleCollider>().enabled = false;
        ani.enabled = false;

        Boom();
        Invoke("DieToVanish", 15.0f);
    }

    private void Boom()
    {
        //폭발 범위 내 데미지 주기
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRange);
        foreach (Collider col in colliders)
        {
            if (col.gameObject.tag == "Player")
            {
                col.gameObject.GetComponent<CharacterBase>().ReceiveDamage(50);
                col.gameObject.GetComponent<CharacterBase>().ExplosionAction(300.0f, gameObject.transform.position, explosionRange * 5.0f);
            }
        }
        shape.enabled = false;
        explosion.Play();
        audioSource.PlayOneShot(explosionAudio);

        poison.GetComponent<PoisonFog>().StartPoisioning();
        poison.Play();
    }

    public void StartRunState()
    {
        ani.SetBool("Closed", true);
        enemyAi.speed = stat._walkSpeed * 5.0f;
    }
    
    public void ImmediateBoom()
    {
        readyToBoomTime = 0;
        Dead();
    }
}
