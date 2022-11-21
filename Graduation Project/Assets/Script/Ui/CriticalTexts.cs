using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CriticalTexts : MonoBehaviour
{
    [SerializeField] private CriticalText criticalText = null;

    private List<CriticalText> criticalTexts = new List<CriticalText>();

    private Vector3 startPos = new Vector3(290.0f, -60.0f, 0.0f);

    private int maxNum = 30;

    void Start()
    {        
        for (int i = 0; i < maxNum; i++)
        {
            CriticalText text = Instantiate(criticalText) as CriticalText;
            text.transform.SetParent(transform);
            text.transform.localPosition = startPos;
            text.gameObject.SetActive(false);
            criticalTexts.Add(text);
        }
    }

    public void HitCritical()
    {
        CriticalText text = GetUnusedText();
        text.gameObject.SetActive(true);
        text.HitCritical(startPos);
    }

    //사용할 수 있는 텍스트 찾기
    private CriticalText GetUnusedText()
    {
        foreach (var text in criticalTexts)
            if (text.gameObject.activeSelf == false)
                return text;

        return null;
    }
}
