using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletHud : MonoBehaviour
{
    [SerializeField] private Text loadedBullet = null;
    [SerializeField] private Text remainBullet = null;

    [SerializeField] private Image weaponImage = null;
    [SerializeField] private Sprite akImage = null;
    [SerializeField] private Sprite sniperImage = null;

    public void UpdateText(int loadedBulletNum, int remainBulletNum)
    {
        loadedBullet.text = loadedBulletNum.ToString();
        remainBullet.text = remainBulletNum.ToString();
    }

    public void ChangeWeapon(string name)
    {
        switch(name)
        {
            case "AK":
                weaponImage.sprite = akImage;
                break;
            case "Sniper":
                weaponImage.sprite = sniperImage;
                break;
        }        
    }
}
