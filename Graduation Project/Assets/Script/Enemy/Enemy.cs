using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : CharacterBase
{
    protected NavMeshAgent enemyAi = null;
    public NavMeshAgent _enemyAi { get { return enemyAi; } }
    protected List<Rigidbody> rigidBodys = new List<Rigidbody>();

    [SerializeField] private ParticleSystem summonParticle = null;
    [SerializeField] protected BulletItem dropItem = null;

    void Start()
    {
        enemyAi = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();
        col = GetComponent<CapsuleCollider>();
        enemyAi.speed = stat._walkSpeed;
        rb = GetComponent<Rigidbody>();

        if (dropItem != null)
            dropItem.gameObject.SetActive(false);
        if (hpBar != null && gameObject.tag != "Boss")
            hpBar.gameObject.SetActive(false);
    }

    void FixedUpdate()
    {
        AnimatorSetting();
        HpBarSetting();
    }

    protected virtual void AnimatorSetting()
    {
        ani.SetFloat("Horizontal", enemyAi.velocity.normalized.x);
        ani.SetFloat("Vertical", -enemyAi.velocity.normalized.z);
    }

    virtual protected void HpBarSetting()
    {
        if (hpBar != null) HpBarLookAtCamera();
    }

    protected void DieToVanish() { gameObject.SetActive(false); }

    override protected void Dead()
    {
        GetComponent<BehaviorExecutor>().enabled = false;

        base.Dead();

        Die();
        if (dropItem != null) RandomDropItem();
    }

    virtual protected void Die()
    {
    }

    public void AddRigidBody(Rigidbody rb) { rigidBodys.Add(rb); }
    public void SummonParticlePlay() { summonParticle.Play(); }

    private void RandomDropItem()
    {
        int randomVal = Random.Range(0, 100);

        if (randomVal > 70)
        {
            dropItem.gameObject.SetActive(true);
            dropItem.SetDropBulletsNum(Random.Range(5, 20));
        }
    }

    public override void ExplosionAction(float force, Vector3 explosionPosition, float explosionRadius)
    {
        if (isDead == true) return;
        enemyAi.updatePosition = false;
        enemyAi.updateRotation = false;
        enemyAi.isStopped = true;
        base.ExplosionAction(force, explosionPosition, explosionRadius);
        enemyAi.velocity = rb.velocity;
        StartCoroutine(PushBackCoroutine());
    }

    private IEnumerator PushBackCoroutine()
    {
        enemyAi.velocity = rb.velocity;
        yield return new WaitForSeconds(1.0f);

        if (isDead == false)
        {
            enemyAi.isStopped = false;
            enemyAi.updatePosition = true;
            enemyAi.updateRotation = true;
        }
    }
}