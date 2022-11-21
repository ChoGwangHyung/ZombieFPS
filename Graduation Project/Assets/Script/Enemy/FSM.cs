using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

enum Pattern
{
    FixPos,
    Detect
}

enum State
{
    Detect,
    Avoid,
    Attack,
    Die
}

public class FSM : CharacterBase
{
    private NavMeshAgent enemyAi = null;
    public NavMeshAgent _enemyAi { get { return enemyAi; } }
    private List<Rigidbody> rigidBodys = new List<Rigidbody>();

    [SerializeField] private Pattern whatEnemyPattern = 0;

    private State curState = State.Detect;

    //정찰위치
    [SerializeField] private Transform[] patrolPoints = null;
    private int patrolCount = 0;

    //Fix enemy 정찰
    private float rotMax = 60;
    private float curRot = 0;
    private float rotVal = 0.5f;

    private Transform target = null;

    //피하기
    private Vector3 avoidDir;

    //시야각
    private float viewAngle = 220.0f;
    private float viewDistance = 30.0f;

    [SerializeField] private LayerMask targetMask = 0;

    [SerializeField] private EnemyWeaponController weapon = null;

    private float RangeAttackDistance = 30.0f;
    private float MeleeAttackDistance = 2.0f;
    private float MoveToTargetDistance = 50.0f;
    private float targetToDistance = 0.0f;

    [SerializeField] private ParticleSystem summonParticle = null;

    [SerializeField] private BulletItem dropItem = null;

    void Start()
    {
        enemyAi = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();
        col = GetComponent<CapsuleCollider>();
        enemyAi.speed = stat._walkSpeed;
        enemyAi.isStopped = false;

        dropItem.gameObject.SetActive(false);
        //StartCoroutine(FsmMain());
    }

    void Update()
    {
        ani.SetBool("IsFired", weapon.CanFire());
        WalkAnimatorSetting();
        HpBarLookAtCamera();
    }

    //Fsm State
    private IEnumerator FsmMain()
    {
        while (true)
        {
            HpBarLookAtCamera();
            WalkAnimatorSetting();

            if (isDead == true) ChangeState(State.Die);
            if (target != null)
                targetToDistance = Vector3.Distance(target.position, enemyAi.transform.position);

            switch (whatEnemyPattern)
            {
                case Pattern.Detect:
                    switch (curState)
                    {
                        case State.Detect:
                            if (IsInvoking("MoveToDetectionPoint") == false) Invoke("MoveToDetectionPoint", 1.0f);
                            break;
                        case State.Attack:
                            DetectAttackToTarget();
                            break;
                        case State.Avoid:
                            if (IsInvoking("SetAvoidDir") == false) Invoke("SetAvoidDir", 4.0f);
                            else Avoid();
                            enemyAi.transform.LookAt(target);
                            break;
                        case State.Die:
                            break;
                    }
                    break;

                case Pattern.FixPos:
                    switch (curState)
                    {
                        case State.Detect:
                            FixDetectToRotate();
                            break;
                        case State.Attack:
                            FixAttackToTarget();
                            break;
                        case State.Avoid:
                            if (IsInvoking("SetAvoidDir") == false) Invoke("SetAvoidDir", 4.0f);
                            else Avoid();
                            enemyAi.transform.LookAt(target);
                            break;
                        case State.Die:
                            break;
                    }
                    break;
            }

            yield return null;
        }
    }

    private void ChangeState(State change)
    {
        //FSM State Out
        switch (curState)
        {
            case State.Detect:
                break;
            case State.Attack:
                ani.SetBool("IsFired", false);
                break;
            case State.Avoid:
                CancelInvoke("SetAvoidDir");
                break;
            case State.Die:
                break;
        }

        //FSM State In
        switch (change)
        {
            case State.Detect:
                RemoveTarget();
                break;
            case State.Attack:
                break;
            case State.Avoid:
                SetAvoidDir();
                break;
            case State.Die:
                StopAllCoroutines();
                RandomDropItem();
                Invoke("DieToVanish", 5.0f);
                break;
        }

        curState = change;
    }

    private void WalkAnimatorSetting()
    {
        ani.SetFloat("Horizontal", enemyAi.velocity.z);
        ani.SetFloat("Vertical", -enemyAi.velocity.x);
        ani.SetBool("HaveDamaged", haveDamagedByBarrel || haveDamagedByBullet);
    }

    private void MoveToDetectionPoint()
    {
        View();

        if (enemyAi.velocity == Vector3.zero && enemyAi.pathPending == false)
        {
            SetDestination(patrolPoints[patrolCount++].position);

            if (patrolCount >= patrolPoints.Length)
                patrolCount = 0;
        }
    }

    private void DetectAttackToTarget()
    {
        //플레이어와 근접하게 이동
        if (targetToDistance <= MoveToTargetDistance && targetToDistance > RangeAttackDistance - 5.0f)
        {
            SetDestination(target.position);
        }
        else
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + transform.up, (target.position - transform.position).normalized, out hit, RangeAttackDistance))
            {
                if (hit.transform.tag == "Player")
                {
                    enemyAi.isStopped = true;

                    //장거리 공격
                    if (targetToDistance <= RangeAttackDistance && targetToDistance > MeleeAttackDistance)
                        Attack();
                    //근접 공격
                    else if (targetToDistance <= MeleeAttackDistance)
                        Attack();
                }
                else
                {
                    SetDestination(target.position);
                }
            }
        }
    }

    private void FixDetectToRotate()
    {
        View();

        if (curRot >= rotMax || curRot <= -rotMax) rotVal = -rotVal;
        curRot += rotVal;

        enemyAi.transform.rotation = enemyAi.transform.rotation * Quaternion.Euler(0.0f, rotVal, 0.0f);
    }

    private void FixAttackToTarget()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + transform.up, (target.position - transform.position).normalized, out hit, RangeAttackDistance * 2))
        {
            if (hit.transform.tag == "Player")
            {
                enemyAi.isStopped = true;

                //장거리 공격
                if (targetToDistance <= RangeAttackDistance * 2)
                    Attack();
            }
            else
            {
                SetDestination(target.position);
            }
        }
    }

    private void Attack()
    {
        enemyAi.transform.LookAt(target);
        weapon.Fire();
        ani.SetBool("IsFired", true);

        if (weapon.CanFire() == false) ChangeState(State.Avoid);
        if (targetToDistance > MoveToTargetDistance) ChangeState(State.Detect);
    }

    private void SetAvoidDir()
    {
        int moveDir = Random.Range(1, 3);
        avoidDir = Vector3.zero;
        switch (moveDir)
        {
            case 1:
                avoidDir = enemyAi.transform.right;
                break;
            case 2:
                avoidDir = -enemyAi.transform.right;
                break;
        }
    }

    private void Avoid()
    {
        if (weapon.CanFire() == true) ChangeState(State.Attack);
        if (targetToDistance > MoveToTargetDistance) ChangeState(State.Detect);

        SetDestination(enemyAi.transform.position + avoidDir);
    }

    //시야각 관련 함수
    private void View()
    {
        Vector3 leftBoundary = BoundaryAngle(-viewAngle * 0.5f);
        Vector3 rightBoundary = BoundaryAngle(viewAngle * 0.5f);

        Collider[] target = Physics.OverlapSphere(transform.position, viewDistance * 2, targetMask);

        if (target.Length < 1) return;

        Transform findTarget = target[0].transform;

        Vector3 direction = (findTarget.position - transform.position).normalized;
        float angle = Vector3.Angle(direction, transform.forward);

        if ((angle < viewAngle * 0.5f && angle > -viewAngle * 0.5f) || haveDamagedByBarrel == true || haveDamagedByBullet == true)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position + transform.up, direction, out hit, viewDistance, targetMask))
                if (hit.transform.tag == "Player")
                {
                    SetTarget(hit.transform);
                    ChangeState(State.Attack);
                }
        }
    }

    private Vector3 BoundaryAngle(float angle)
    {
        angle += transform.eulerAngles.y;
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0.0f, Mathf.Cos(angle * Mathf.Deg2Rad));
    }

    //타겟 설정 변경
    private void SetTarget(Transform target) { this.target = target; }
    private void RemoveTarget() { this.target = null; }

    //이동 설정
    private void SetDestination(Vector3 target)
    {
        if (enemyAi.enabled == false) return;
        enemyAi.isStopped = false;
        enemyAi.SetDestination(target);
    }

    private void DieToVanish() { gameObject.SetActive(false); }

    override protected void Dead()
    {
        GetComponent<BehaviorExecutor>().enabled = false;

        base.Dead();

        enemyAi.enabled = false;
        hpBar.gameObject.SetActive(false);

        foreach (Rigidbody rb in rigidBodys)
            rb.isKinematic = false;
        ani.enabled = false;
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
}