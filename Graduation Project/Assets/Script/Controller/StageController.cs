using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageController : MonoBehaviour
{
    protected int dieEnemys = 0;
    protected int allEnemys = 0;

    protected int enemySummonIndex = -1;
    public int _enemySummonIndex { get { return enemySummonIndex; } }

    [SerializeField] protected MiniMap miniMap = null;

    protected string nextSceneName = null;

    [SerializeField] protected GameObject show = null;
    [SerializeField] protected Text progressText = null;
    [SerializeField] protected Text remainEnemyText = null;
    [SerializeField] protected Text goalText = null;

    [SerializeField] protected GameObject nextShow = null;
    [SerializeField] protected Text nextShowGoalText = null;
    [SerializeField] protected Text nextShowProgressText = null;
    [SerializeField] protected Text nextShowProgressTitleText = null;
    [SerializeField] protected Image nextShowBackground = null;

    protected AudioSource audioSource = null;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
            GoNextStep();
        if (Input.GetKeyDown(KeyCode.F2))
            EndStage();
    }

    protected virtual void CheckEnemyDie()
    {        
    }

    private IEnumerator NextShowUiCoroutine()
    {
        show.SetActive(false);
        nextShow.SetActive(true);       
        nextShowGoalText.text = goalText.text;
        yield return new WaitForSeconds(1.0f);

        Color[] alpha = new Color[4];
        alpha[0] = nextShowGoalText.color;
        alpha[1] = nextShowProgressText.color;
        alpha[2] = nextShowProgressTitleText.color;
        alpha[3] = nextShowBackground.color;

        float alphaValue = 1.0f;
        while (alphaValue > 0)
        {
            for (int i = 0; i < alpha.Length; i++)
                alpha[i].a = alphaValue;

            nextShowGoalText.color = alpha[0];
            nextShowProgressText.color = alpha[1];
            nextShowProgressTitleText.color = alpha[2];
            nextShowBackground.color = alpha[3];

            alphaValue -= Time.deltaTime;
            yield return null;
        }

        nextShow.SetActive(false);
        for (int i = 0; i < alpha.Length; i++)
            alpha[i].a = 1.0f;
        nextShowGoalText.color = alpha[0];
        nextShowProgressText.color = alpha[1];
        nextShowProgressTitleText.color = alpha[2];
        nextShowBackground.color = alpha[3];

        show.SetActive(true);
    }

    protected void NextShowUi(int curStep, int maxStep)
    {
        nextShowProgressText.text = curStep + "/" + maxStep;
        StartCoroutine(NextShowUiCoroutine());
    }

    public virtual void UpdateQuestView()
    {
    }

    public virtual void GoNextStep()
    {        
    }

    protected virtual void SummonEnemy()
    {
    }

    protected virtual void EndStage()
    {
        SceneManager.LoadScene(nextSceneName);
    }

    public bool CanGoToNextStep() { return dieEnemys == allEnemys; }
}
