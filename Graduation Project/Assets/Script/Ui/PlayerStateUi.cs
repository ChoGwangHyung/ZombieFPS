using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Condition { Moribund, Poision };

public class PlayerStateUi : MonoBehaviour
{
    [System.Serializable]
    private class ConditionInfo
    {
        public Image image = null;
        public bool isStartCouroutineOn = false;
        public bool isEndCouroutineOn = false;

        public float startVal = 1.0f;
        public float endVal = 0.5f;
        public float changeVal = 0.01f;
    }

    [SerializeField] private ConditionInfo[] conditionInfos = null;

    void Start()
    {
        foreach (var conditionInfo in conditionInfos)
            conditionInfo.image.gameObject.SetActive(false);
    }

    public void StartConditionState(Condition condition)
    {
        conditionInfos[(int)condition].image.gameObject.SetActive(true);
        if (conditionInfos[(int)condition].isEndCouroutineOn)
        {
            conditionInfos[(int)condition].isEndCouroutineOn = false;
            StopCoroutine(HighlightCoroutine(conditionInfos[(int)condition]));
        }
        StartCoroutine(HighlightCoroutine(conditionInfos[(int)condition]));
    }

    public void EndConditionState(Condition condition)
    {
        if (conditionInfos[(int)condition].isStartCouroutineOn == false) return;
        conditionInfos[(int)condition].isStartCouroutineOn = false;
        StartCoroutine(StopHighLightCoroutine(conditionInfos[(int)condition]));
    }

    private IEnumerator HighlightCoroutine(ConditionInfo conditionInfo)
    {
        conditionInfo.isStartCouroutineOn = true;

        Color alpha = conditionInfo.image.color;
        alpha.a = conditionInfo.startVal;

        while (conditionInfo.isStartCouroutineOn)
        {
            while (alpha.a >= conditionInfo.endVal)
            {
                conditionInfo.image.color = alpha;
                alpha.a -= conditionInfo.changeVal;
                yield return null;
            }

            while (alpha.a <= conditionInfo.startVal)
            {
                conditionInfo.image.color = alpha;
                alpha.a += conditionInfo.changeVal;
                yield return null;
            }

            yield return null;
        }
    }

    private IEnumerator StopHighLightCoroutine(ConditionInfo conditionInfo)
    {
        conditionInfo.isStartCouroutineOn = false;
        conditionInfo.isEndCouroutineOn = true;

        Color alpha = conditionInfo.image.color;

        while (alpha.a >= 0)
        {
            conditionInfo.image.color = alpha;
            alpha.a -= 0.02f;
            yield return null;
        }

        conditionInfo.image.gameObject.SetActive(false);
        conditionInfo.isEndCouroutineOn = false;
    }
}
