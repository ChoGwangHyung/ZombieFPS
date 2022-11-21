using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterBase : MonoBehaviour
{
    [SerializeField] protected CharacterStat stat;
    public CharacterStat _stat { get { return stat; } }

    protected Rigidbody rb;
    protected CapsuleCollider col;
    protected Animator ani;

    protected AudioSource audioSource;
    [SerializeField] private AudioClip damagedAudio = null;

    [SerializeField] protected ParticleSystem bloodSpit;
    [SerializeField] protected Slider hpBar;

    [SerializeField] protected float curLife;

    protected bool haveDamagedByBullet = false;
    public bool _haveDamagedByBullet { get { return haveDamagedByBullet; } }
    protected bool haveDamagedByBarrel = false;
    public bool _haveDamagedByBarrel { get { return haveDamagedByBarrel; } }
    protected bool isDead = false;
    public bool _isDead { get { return isDead; } }

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        AudioController.instance.AddAudioSource(audioSource);
        curLife = stat._maxLife;
        if (hpBar != null)
            hpBar.value = curLife / stat._maxLife;
    }

    protected void HpBarLookAtCamera()
    {
        if (haveDamagedByBarrel || haveDamagedByBullet)
        {
            CancelInvoke("HideHpBar");
            if (isDead == false)
            {
                hpBar.gameObject.SetActive(true);
                Invoke("HideHpBar", 5.0f);
            }
            else
            {
                hpBar.gameObject.SetActive(false);
            }
        }
        else
        {
            if (hpBar.gameObject.activeSelf == true)
                hpBar.transform.LookAt(MainCamera.instance.transform);
        }
    }

    private void HideHpBar()
    {
        hpBar.gameObject.SetActive(false);
    }

    protected void HpReset()
    {
        isDead = false;
        curLife = stat._maxLife;
        if (hpBar != null)
            hpBar.value = curLife / stat._maxLife;
    }

    private void HaveDamagedByBulletInit() { haveDamagedByBullet = false; }
    private void HaveDamagedByBarrelInit() { haveDamagedByBarrel = false; }

    virtual protected void Dead()
    {
        isDead = true;
    }

    //총알 데미지
    public void ReceiveDamage(float damage, Vector3 damagePos)
    {
        bloodSpit.transform.position = damagePos;
        bloodSpit.transform.rotation = Quaternion.Euler(0, Random.Range(-90.0f, 90.0f), 90.0f);
        bloodSpit.Play();

        haveDamagedByBullet = true;
        if (IsInvoking("HaveDamagedByBulletInit") == true) CancelInvoke("HaveDamagedByBulletInit");
        else Invoke("HaveDamagedByBulletInit", 0.1f);

        DownLife(damage);
    }

    //베럴 데미지
    public void ReceiveDamage(float damage)
    {
        haveDamagedByBarrel = true;
        if (IsInvoking("HaveDamagedByBarrelInit") == true) CancelInvoke("HaveDamagedByBarrelInit");
        else Invoke("HaveDamagedByBarrelInit", 0.1f);

        DownLife(damage);
    }

    private void DownLife(float damage)
    {
        if (curLife <= 0) return;

        audioSource.PlayOneShot(damagedAudio);

        curLife -= damage;
        if (hpBar != null)
            hpBar.value = curLife / stat._maxLife;

        LifeFiguresCheck();

        if (curLife <= 0)
        {
            curLife = 0;
            Dead();
            return;
        }
    }

    virtual protected void LifeFiguresCheck() { }

    virtual public void ExplosionAction(float force, Vector3 explosionPosition, float explosionRadius)
    {
        rb.AddExplosionForce(force, explosionPosition, explosionRadius, 1.0f, ForceMode.Acceleration);
    }
}