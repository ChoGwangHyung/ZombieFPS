using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStageController : StageController
{
    private static BossStageController bossStageController;
    public static BossStageController instance
    {
        get
        {
            if (bossStageController == null)
                bossStageController = FindObjectOfType<BossStageController>();
            return bossStageController;
        }
    }
    enum StageStep { Start, Step1, Step2, End }

    private StageStep curProgressStep = StageStep.Start;
    private StageStep maxProgressStep = StageStep.Step2;

    private string enemyKillText = "길을 막고 있는 실험체를 처치하세요!";
    private string escapeText = "컨테이너를 통해 탈출하세요!";

    [SerializeField] private BossEnemy bossEnemy = null;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        AudioController.instance.AddAudioSource(audioSource);
        nextSceneName = "End";

        allEnemys = 1;        
        remainEnemyText.text = dieEnemys + "/" + allEnemys;

        bossEnemy.gameObject.SetActive(false);

        GoNextStep();
    }

    public void SummonBoss()
    {
        bossEnemy.gameObject.SetActive(true);
    }

    public void BossDead()
    {
        dieEnemys = 1;
        GoNextStep();
    }

    public override void GoNextStep()
    {
        curProgressStep++;
        progressText.text = (int)curProgressStep + "/" + (int)maxProgressStep;

        switch (curProgressStep)
        {
            case StageStep.Step1:
                goalText.text = enemyKillText;
                NextShowUi((int)curProgressStep, (int)maxProgressStep);
                break;
            case StageStep.Step2:
                goalText.text = escapeText;
                NextShowUi((int)curProgressStep, (int)maxProgressStep);
                break;
            case StageStep.End:
                EndStage();
                break;
        }
    }

    protected override void EndStage()
    {
        base.EndStage();
    }
}
