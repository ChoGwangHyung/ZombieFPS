using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletItem : MonoBehaviour
{
    private int dropBulletsNum = 0;

    private bool isCoroutinePlay = false;

    void Update()
    {
        if (Vector3.Distance(Player.instance.transform.position, transform.position) < 10.0f && isCoroutinePlay == false)
        {
            StartCoroutine(ItemGoToPlayerCoroutine());
        }
    }

    private IEnumerator ItemGoToPlayerCoroutine()
    {
        isCoroutinePlay = true;
        Vector3 targetPos = new Vector3(0.0f, 0.8f, 0.0f);
        yield return new WaitForSeconds(1.0f);
        
        while (Vector3.Distance(Player.instance.transform.position + targetPos, transform.position) > 0.2f)
        {
            transform.position = Vector3.Slerp(transform.position, Player.instance.transform.position + targetPos, 0.05f);
            yield return null;
        }

        Player.instance._weaponController._curRangedWeapon.GetBullets(dropBulletsNum);
        isCoroutinePlay = false;
        gameObject.SetActive(false);
    }

    public void SetDropBulletsNum(int value)
    {
        dropBulletsNum = value;
    }
}
