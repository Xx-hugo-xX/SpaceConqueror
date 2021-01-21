using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    
    #region Positions

    [SerializeField] private float startingX;
    [SerializeField] private float endingX;
    [SerializeField] private float maxY;
    [SerializeField] private float minY;

    #endregion
    

    [SerializeField] private Transform[] spawnPoints;

    [SerializeField] private float spawnInterval = 0.5f;

    [SerializeField] private GameObject obstacle;

    private List<GameObject> enemyList;

    public bool canShoot = true;

    private PlayerBehaviour pB;
    private IntroLevelManager iLM;
    [SerializeField] private LevelManager lM;
    [SerializeField] private MainMenuManager mMM;

    private float enemyMoveSpeed;

    private int currentLevel = 1;

    void Start()
    {
        pB = GameObject.Find("Player").GetComponent<PlayerBehaviour>();
    }

    void Update()
    {
        foreach (GameObject gO in enemyList)
        {
            if (gO == null) continue;

            if (gO.transform.position.x <= endingX)
            {
                enemyList.Remove(gO);
                Destroy(gO);
                pB.AddScore(1);
            }
        }
        if (lM.hasLevelProgression && lM.enabled && currentLevel != lM.GetCurrentLevel()) UpdateValues();
    }

    private void SpawnObject()
    {
        int spawnInd = Random.Range(0, spawnPoints.Length);

        Vector2 enemyPos = spawnPoints[spawnInd].position;

        GameObject newObstacle = Instantiate(obstacle, enemyPos, Quaternion.identity, transform);

        enemyList.Add(newObstacle);

        newObstacle.GetComponent<EnemyBehaviour>().enabled = true;

        newObstacle.GetComponent<EnemyBehaviour>().lM = lM;



        if (enemyMoveSpeed != 0) newObstacle.GetComponent<EnemyBehaviour>().movSpeed = enemyMoveSpeed;
    }

    public List<GameObject> GetEnemyList() => enemyList;

    public void DestroyAllShips()
    {
        foreach (GameObject gO in enemyList)
        {
            Destroy(gO);
        }
    }

    private void UpdateValues()
    {
        print("CALLED");
        CancelInvoke();
        lM.DestroyAllShipsAndLasers(false);
        enemyMoveSpeed = lM.enemyMovSpeed;
        spawnInterval = lM.enemySpawnInterval;
        currentLevel++;
        InvokeRepeating("SpawnObject", 0, spawnInterval);
    }

    private void OnEnable()
    {
        //CancelInvoke();
        enemyList = new List<GameObject>();
        iLM = GameObject.Find("IntroLevelManager").GetComponent<IntroLevelManager>();

        if (iLM.enabled)
        {
            spawnInterval = iLM.enemySpawnInterval;
            canShoot = false;
            enemyMoveSpeed = mMM.tSp;
        }
        else
        {
            spawnInterval = lM.enemySpawnInterval;
            canShoot = true;
            enemyMoveSpeed = mMM.gSp;
        }

        InvokeRepeating("SpawnObject", 0, spawnInterval);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}
