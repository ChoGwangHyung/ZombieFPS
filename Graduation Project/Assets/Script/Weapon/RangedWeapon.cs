using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : MonoBehaviour
{
    [SerializeField] protected RangedWeaponStat weaponStat;
    public RangedWeaponStat _weaponStat { get { return weaponStat; } }

    [SerializeField] protected ParticleSystem flash;

    //사격 속도    
    protected float curFireCooltime;

    //재장전
    protected bool isReload = false;
    public bool _isReload { get { return isReload; } }

    //총알 관련 변수
    [SerializeField] protected BulletController Bullets = null;
    protected int curBulletInMagazine;
    public int _curBulletInMagazine { get { return curBulletInMagazine; } }
    protected int curBulletInBag;
    public int _curBulletInBag { get { return curBulletInBag; } }

    void Awake()
    {
        Bullets.SetBullet(weaponStat._maxBulletInMagazine, weaponStat._accuracy, weaponStat._range, weaponStat._speed, weaponStat._damage);
        curBulletInMagazine = weaponStat._maxBulletInMagazine;
        curBulletInBag = weaponStat._maxBulletInBag;
    }

    void Update()
    {
        GunFireCooltimeCalc();
    }

    //연사속도 재계산
    private void GunFireCooltimeCalc()
    {
        if (curFireCooltime > 0)
            curFireCooltime -= Time.deltaTime;
    }

    public virtual void Fire()
    {
        if (curFireCooltime > 0 || isReload == true || (curBulletInBag <= 0 && curBulletInMagazine <= 0)) return;

        if (curBulletInMagazine <= 0) { Reload(); return; }

        curBulletInMagazine--;

        curFireCooltime = weaponStat._fireColltime;
    }

    public virtual void Reload() { if (isReload == true || curBulletInMagazine >= weaponStat._maxBulletInMagazine) return; }

    //재장전 코루틴
    protected IEnumerator ReloadCoroutine()
    {
        isReload = true;

        yield return new WaitForSeconds(weaponStat._reloadTime);

        //총알이있을때 장전
        curBulletInBag += curBulletInMagazine;

        if (curBulletInBag >= weaponStat._maxBulletInMagazine)
        {
            curBulletInMagazine = weaponStat._maxBulletInMagazine;
            curBulletInBag -= weaponStat._maxBulletInMagazine;
        }
        //현재 가지고 있는 총알의 개수가 탄창을 가득 채울수 없다면
        else
        {
            curBulletInMagazine = curBulletInBag;
            curBulletInBag = 0;
        }

        isReload = false;
    }

    //사격 가능여부
    public bool CanFire() { return isReload == false && curFireCooltime <= 0 && (curBulletInBag > 0 || curBulletInMagazine > 0); }

    public void GetBullets(int value)
    {
        if (curBulletInBag + value <= weaponStat._maxBulletInBag)
            curBulletInBag += value;
        else
            curBulletInBag = weaponStat._maxBulletInBag;
    }

    public bool IsTargerPointOfSight(GameObject startPoint, List<string> targetTagNames)
    {
        RaycastHit hitInfo;

        if (Physics.Raycast(startPoint.transform.position, startPoint.transform.forward, out hitInfo, weaponStat._range))
        {
            foreach (var targetTagName in targetTagNames)
                if (hitInfo.collider.gameObject.tag.Contains(targetTagName))
                    return true;
        }
        return false;
    }
}
