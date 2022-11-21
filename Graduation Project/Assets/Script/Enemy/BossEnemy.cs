using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BossAttackPattern { NONE, REST, SPIT, DASH, JUMP };

public class BossEnemy : Enemy
{
    public BossAttackPattern curPattern = BossAttackPattern.NONE;

    [SerializeField] private AudioClip jumpAttackAudio = null;
    [SerializeField] private AudioClip dashAttackAudio = null;
    [SerializeField] private AudioClip spitAttackAudio = null;

    [SerializeField] private Text hpVal = null;
    [SerializeField] private GameObject deadCam = null;

    [SerializeField] private float jumpPower = 1.0f;
    public float _jumpPower { get { return jumpPower; } }

    private float dashDamage = 50.0f;
    public float _dashDamage { get { return dashDamage; } }

    private float jumpAttackDamage = 70.0f;
    public float _jumpAttackDamage { get { return jumpAttackDamage; } }
    private float jumpTime;
    public float _jumpTime { get { return jumpTime; } }
    private float startJumpTime = 0.0f;
    public float _startJumpTime { get { return startJumpTime; } }
    private Vector3 goalPos;
    public Vector3 _goalPos { get { return goalPos; } }

    private bool isPatternEnd = true;
    public bool _isPatternEnd { get { return isPatternEnd; } }

    private bool canPatternSelect = true;
    public bool _canPatternSelect { get { return canPatternSelect; } }

    [SerializeField] private GameObject dashAttackCol = null;
    [SerializeField] private GameObject jumpAttackCol = null;
    [SerializeField] private GameObject acidAttackCol = null;
    [SerializeField] private GameObject acidParticle = null;

    public void PatternSelected() { canPatternSelect = false; }
    public void PatternStart() { isPatternEnd = false; }
    public void PatternEnd()
    {
        canPatternSelect = true;
        isPatternEnd = true;
    }

    //대쉬 어택
    public void DashAttackStart()
    {
        ani.SetBool("IsDashAttackOn", true);
        enemyAi.speed = stat._walkSpeed * 100.0f;
        dashAttackCol.SetActive(true);

        if (audioSource.clip != dashAttackAudio)
        {
            audioSource.clip = dashAttackAudio;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    public void DashAttackEnd()
    {
        ani.SetBool("IsDashAttackOn", false);
        enemyAi.speed = stat._walkSpeed;

        audioSource.clip = null;
        audioSource.loop = false;
        audioSource.Stop();

        dashAttackCol.SetActive(false);
        PatternEnd();       
    }

    //점프 어택
    public void JumpAttackStart(Vector3 goalPos, float startTime)
    {
        ani.SetBool("IsJumpAttackOn", true);

        jumpAttackCol.SetActive(true);
        col.enabled = false;

        jumpTime = ani.GetCurrentAnimatorStateInfo(0).length;
        startJumpTime = startTime;
        this.goalPos = goalPos;
    }

    public void JumpAttackEnd()
    {
        ani.SetBool("IsJumpAttackOn", false);

        jumpAttackCol.SetActive(false);
        col.enabled = true;

        PatternEnd();

        audioSource.PlayOneShot(jumpAttackAudio);
    }

    //산성 침
    public void AcidAttackStart()
    {
        ani.SetBool("IsSpitAttackOn", true);

        acidParticle.SetActive(true);
        acidAttackCol.GetComponent<BoxCollider>().enabled = true;

        audioSource.clip = spitAttackAudio;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void AcidAttackEnd()
    {
        ani.SetBool("IsSpitAttackOn", false);

        acidParticle.SetActive(false);
        acidAttackCol.GetComponent<BoxCollider>().enabled = false;

        audioSource.clip = null;
        audioSource.loop = false;
        audioSource.Stop();

        PatternEnd();
    }

    protected override void AnimatorSetting()
    {
    }

    override protected void Die()
    {
        DashAttackEnd();
        JumpAttackEnd();
        AcidAttackEnd();
        StartCoroutine(DeadCouroutine());
    }

    private IEnumerator DeadCouroutine()
    {
        GameController.instance.StopGame();
        deadCam.SetActive(true);
        ani.SetTrigger("Dead");

        yield return new WaitForSeconds(3.0f);

        GameController.instance.PlayGame();
        BossStageController.instance.BossDead();
        deadCam.SetActive(false);
        gameObject.SetActive(false);
    }

    override protected void HpBarSetting()
    {
        hpVal.text = (int)curLife + " / " + (int)stat._maxLife;
    }
}