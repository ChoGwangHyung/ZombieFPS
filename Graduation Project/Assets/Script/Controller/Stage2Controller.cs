using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage2Controller : StageController
{
    private static Stage2Controller stage2Controller;
    public static Stage2Controller instance
    {
        get
        {
            if (stage2Controller == null)
                stage2Controller = FindObjectOfType<Stage2Controller>();
            return stage2Controller;
        }
    }
    enum StageStep { Start, Step1, Step2, End }

    private StageStep curProgressStep = StageStep.Start;
    private StageStep maxProgressStep = StageStep.Step2;

    private string enemyKillText = "몰려 오는 실험체들을 모두 처치하시오!";
    private string getExperimentalDocumentsText = "실험 문서들을 획득하세요!";

    [SerializeField] private ExperimentDocument experimentDocument = null;

    private List<Enemy> enemys = new List<Enemy>();

    private int curEnemys = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        AudioController.instance.AddAudioSource(audioSource);
        nextSceneName = "Begin Boos Stage";
        GoNextStep();
        for (int i = 0; i < EnemyController.instance._enemys.Count; i++)
            allEnemys += EnemyController.instance._enemys[i].Count;

        miniMap.SetEnemysCount(allEnemys);
        SummonEnemy();
    }

    void FixedUpdate()
    {
        miniMap.MiniMapUpdate(Player.instance.transform, enemys);
        UpdateQuestView();
    }

    protected override void CheckEnemyDie()
    {
        int dieEnemysCount = 0;
        for (int i = 0; i < EnemyController.instance._enemys.Count; i++)
            dieEnemysCount += EnemyController.instance.HowManyEnemysDie(i);
        dieEnemys = dieEnemysCount;

        if (CanGoToNextStep() == true && curProgressStep == StageStep.Step1)
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
                goalText.text = enemyKillText;
                NextShowUi((int)curProgressStep, (int)maxProgressStep);
                break;
            case StageStep.Step2:
                goalText.text = getExperimentalDocumentsText;
                NextShowUi((int)curProgressStep, (int)maxProgressStep);
                experimentDocument.CanGetExperimentDocumentStart();
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
        StartCoroutine(SummonEnemyCoroutine());
    }

    private IEnumerator SummonEnemyCoroutine()
    {
        int summonindex_1 = 0;
        int summonindex_2 = 0;
        while (summonindex_1 < EnemyController.instance._enemys[0].Count || summonindex_2 < EnemyController.instance._enemys[1].Count)
        {
            int[] summonindex = new int[2] { Random.Range(0, 2), 0 };
            if (summonindex[0] == 0 && summonindex_1 < EnemyController.instance._enemys[0].Count)
                summonindex[1] = summonindex_1++;
            else if (summonindex[0] == 1 && summonindex_2 < EnemyController.instance._enemys[1].Count)
                summonindex[1] = summonindex_2++;

            EnemyController.instance._enemys[summonindex[0]][summonindex[1]].gameObject.SetActive(true);
            EnemyController.instance._enemys[summonindex[0]][summonindex[1]].SummonParticlePlay();

            enemys.Add(EnemyController.instance._enemys[summonindex[0]][summonindex[1]]);
            curEnemys++;
            miniMap.SetEnemysCount(curEnemys);
            yield return new WaitForSeconds(1.5f);
        }
    }
}
