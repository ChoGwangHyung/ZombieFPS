using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private static EnemyController enemyController;
    public static EnemyController instance
    {
        get
        {
            if (enemyController == null)
                enemyController = FindObjectOfType<EnemyController>();
            return enemyController;
        }
    }

    [SerializeField] private GameObject enemysParent = null;
    private Dictionary<int, List<Enemy>> enemys = new Dictionary<int, List<Enemy>>();
    public Dictionary<int, List<Enemy>> _enemys { get { return enemys; } }

    void Awake()
    {
        SetEnemys();
    }

    private void SetEnemys()
    {
        enemys.Clear();
        for (int i = 0; i < enemysParent.transform.childCount; i++)
        {
            if (enemysParent.transform.GetChild(i).gameObject.name != (i + 1).ToString()) continue;

            List<Enemy> addEnemys = new List<Enemy>();

            for (int j = 0; j < enemysParent.transform.GetChild(i).childCount; j++)
            {
                GameObject enemyObj = enemysParent.transform.GetChild(i).GetChild(j).gameObject;
                if (enemyObj.tag == "Enemy")
                {
                    addEnemys.Add(enemyObj.GetComponent<Enemy>());
                    enemyObj.SetActive(false);
                }
            }
            enemys.Add(i, addEnemys);
        }        
    }

    public int HowManyEnemysDie(int enemysIndex)
    {
        int dieEnemysValue = 0;
        for (int i = 0; i < enemys[enemysIndex].Count; i++)
        {
            if (enemys[enemysIndex][i]._isDead == true)
                dieEnemysValue++;
        }

        return dieEnemysValue;
    }
}
