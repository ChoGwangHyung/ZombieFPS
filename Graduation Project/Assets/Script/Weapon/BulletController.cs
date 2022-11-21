using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private GameObject bullet = null;
    private List<GameObject> bullets = new List<GameObject>();

    //총알 설정
    public void SetBullet(int bulletNum, float accuracy, float range, float speed, float damage)
    {
        for (int i = 0; i < bulletNum; i++)
        {
            GameObject obj = Instantiate(bullet) as GameObject;          
            obj.GetComponent<Bullet>().SetBullet(accuracy, range, speed, damage);
            obj.transform.parent = transform;
            bullets.Add(obj);
        }
    }

    //사격
    public void Fire(GameObject dirObject, string owner)
    {
        GameObject obj = GetUnusedBullet();
        obj.SetActive(true);
        obj.GetComponent<Bullet>().Fire(dirObject, owner);
    }

    //사용할 수 있는 총알 찾기
    private GameObject GetUnusedBullet()
    {
        foreach (var obj in bullets)
            if (obj.activeSelf == false)
                return obj;

        return null;
    }
}