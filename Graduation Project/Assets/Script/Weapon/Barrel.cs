using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    private float explosionRange = 4.0f;
    private bool isExplosion = false;
    public bool _isExplosion { get { return isExplosion; } }

    [SerializeField] private ParticleSystem explosion = null;

    private AudioSource audioSource = null;
    [SerializeField] private AudioClip explsionAudio = null;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Explosion()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider>().enabled = false;

        //폭발 범위 내 데미지 주기
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRange);
        foreach (Collider col in colliders)
        {
            if (col.gameObject.tag == "Player")
            {
                col.gameObject.GetComponent<CharacterBase>().ReceiveDamage(50);
                col.gameObject.GetComponent<CharacterBase>().ExplosionAction(300.0f, gameObject.transform.position, explosionRange * 5.0f);
            }
            if (col.gameObject.tag == "Enemy")
            {
                col.gameObject.GetComponent<EnemyHit>().HitByBarrel(50);
                col.gameObject.GetComponent<EnemyHit>()._enemyMain.GetComponent<CharacterBase>().ExplosionAction(300.0f, gameObject.transform.position, explosionRange * 5.0f);
            }
        }

        audioSource.PlayOneShot(explsionAudio);
        explosion.Play();
        isExplosion = true;
    }

    public void ReCreate()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        gameObject.GetComponent<MeshCollider>().enabled = true;

        isExplosion = false;
    }
}
