using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    protected AudioSource audioSource = null;
    [SerializeField] protected AudioClip attackAudio = null;

    [SerializeField] protected MeleeWeaponStat weaponStat = null;
    public MeleeWeaponStat _weaponStat { get { return weaponStat; } }

    [SerializeField] protected Animator ani;

    protected string targetObjectName;

    //체크 변수
    protected bool isAttack = false;
    public bool _isAttack { get { return isAttack; } }
    public bool CanAttack() { return !_isAttack; }

    private bool isSwing = false;
    public bool _isSwing { get { return isSwing; } }

    public void Attack()
    {
        if (isAttack == false)
            StartCoroutine(AttackCoroutine());
    }

    //공격 코루틴
    private IEnumerator AttackCoroutine()
    {
        isAttack = true;

        ani.SetTrigger("IsAttackTrigger");
        ani.SetBool("IsAttack", true);
        yield return new WaitForSeconds(weaponStat._attackStartTime);
        isSwing = true;
        Hit();
        audioSource.PlayOneShot(attackAudio);

        yield return new WaitForSeconds(weaponStat._attackEndTime);
        isSwing = false;
        ani.SetBool("IsAttack", false);

        yield return new WaitForSeconds(weaponStat._attackCoolTime - weaponStat._attackStartTime - weaponStat._attackEndTime);
        isAttack = false;        
    }

    private void Hit()
    {
        if (isSwing == false) return;

        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, weaponStat._range))
        {
            if (hitInfo.collider.gameObject.tag == targetObjectName)
                ProcessHiting(hitInfo);
            isSwing = false;
        }
    }

    protected virtual void ProcessHiting(RaycastHit hitInfo)
    {
    }
}
