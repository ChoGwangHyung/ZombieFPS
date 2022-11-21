using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage1Controller : StageController
{
    private static Stage1Controller stage1Controller;
    public static Stage1Controller instance
    {
        get
        {
            if (stage1Controller == null)
                stage1Controller = FindObjectOfType<Stage1Controller>();
            return stage1Controller;
        }
    }

    enum StageStep { Start, Step1, Step2, Step3, Step4, Step5, End }

    private StageStep curProgressStep = StageStep.Start;
    private StageStep maxProgressStep = StageStep.Step5;    

    [SerializeField] private Generator generator = null;
    
    private string enemyKillText = "맵에 있는 적을 모두 처치하시오!";
    private string generatorText = "발전기를 가동시켜 문을 여시오!";
    private string endStepText = "맵에 있는 적을 모두 처치 후 열린 문으로 이동하시오!";

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        AudioController.instance.AddAudioSource(audioSource);
        nextSceneName = "Begin Stage 2";
        goalText.text = enemyKillText;
        GoNextStep();
    }

    void FixedUpdate()
    {
        miniMap.MiniMapUpdate(Player.instance.transform, EnemyController.instance._enemys[enemySummonIndex]);
        UpdateQuestView();
    }

    protected override void CheckEnemyDie()
    {
        dieEnemys = EnemyController.instance.HowManyEnemysDie(enemySummonIndex);
        if (curProgressStep == StageStep.Step3 && dieEnemys == allEnemys)
            GoNextStep();
    }

    public override void UpdateQuestView()
    {
        CheckEnemyDie();
        remainEnemyText.text = dieEnemys + "/" + allEnemys;
    }

    public override void GoNextStep()
    {
        curProgressStep++;
        progressText.text = (int)curProgressStep + "/" + (int)maxProgressStep;

        switch (curProgressStep)
        {
            case StageStep.Step1:
                SummonEnemy();
                NextShowUi((int)curProgressStep, (int)maxProgressStep);
                break;
            case StageStep.Step2:
                SummonEnemy();
                NextShowUi((int)curProgressStep, (int)maxProgressStep);
                break;
            case StageStep.Step3:
                SummonEnemy();
                NextShowUi((int)curProgressStep, (int)maxProgressStep);
                break;
            case StageStep.Step4:
                generator.GeneratorStart();
                goalText.text = generatorText;
                remainEnemyText.enabled = false;
                NextShowUi((int)curProgressStep, (int)maxProgressStep);
                break;
            case StageStep.Step5:
                SummonEnemy();
                goalText.text = endStepText;
                remainEnemyText.enabled = true;
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

    protected override void SummonEnemy()
    {
        enemySummonIndex++;
        for (int i = 0; i < EnemyController.instance._enemys[enemySummonIndex].Count; i++)
        {
            EnemyController.instance._enemys[enemySummonIndex][i].gameObject.SetActive(true);
            EnemyController.instance._enemys[enemySummonIndex][i].SummonParticlePlay();
        }

        allEnemys = EnemyController.instance._enemys[enemySummonIndex].Count;
        miniMap.SetEnemysCount(allEnemys);
    }
}
