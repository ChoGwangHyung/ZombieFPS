using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dummy : CharacterBase
{
    private bool isRevivaling = false;

    void Start()
    {
        hpBar.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        HpBarLookAtCamera();
    }

    public bool NeedRevivaling() { return isDead == true && isRevivaling == false; }
    public void StartRevival() { isRevivaling = true; }
    public void Revival()
    {
        HpReset();
        isRevivaling = false;
    }
}
