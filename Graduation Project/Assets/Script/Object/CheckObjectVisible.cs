using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckObjectVisible : MonoBehaviour
{
    private bool _isVisible = false;
    public bool isVisible { get { return _isVisible; } }

    //씬 카메라에서 보이는 것도 적용, 즉 모든 카메라 적용
    void OnBecameVisible() { _isVisible = true; }
    void OnBecameInvisible() { _isVisible = false;}    
}
