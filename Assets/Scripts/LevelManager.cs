using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int levelStartTimerDuration;

    [HideInInspector] public bool isCountingDown = false;

    [HideInInspector] public int preMatchTimer;


    [HideInInspector]  public PlayerBehaviour pB;
    private EnemySpawner eS;
    private IntroLevelManager iLM;
    [SerializeField] private MainMenuManager mMM;
    private UIManager uM;


    [HideInInspector] public bool hasLevelProgression;

    private int level = 1;

    public float enemyMovSpeed;
    public float enemySpawnInterval;

    void Start()
    {
        pB = GameObject.Find("Player").GetComponent<PlayerBehaviour>();
        eS = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        iLM = GameObject.Find("IntroLevelManager").GetComponent<IntroLevelManager>();
        uM = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (hasLevelProgression) uM.ShowLevelPanel();

        pB.enabled = false;
        eS.enabled = false;
        iLM.enabled = false;
        StartCoroutine(LevelStartCountdown());
        pB.canShoot = true;
    }

    void Update()
    {
        if (hasLevelProgression) ChangeLevel();
    }

    private IEnumerator LevelStartCountdown()
    {
        isCountingDown = true;

        preMatchTimer = levelStartTimerDuration;

        while (preMatchTimer > 0)
        {
            yield return new WaitForSeconds(1);
            preMatchTimer--;
        }
        isCountingDown = false;

        pB.enabled = true;
        eS.enabled = true;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void DestroyAllShipsAndLasers(bool endGame)
    {
        LaserBehaviour[] lasers = (LaserBehaviour[])FindObjectsOfType(typeof(LaserBehaviour));
        PlayerBehaviour player = (PlayerBehaviour)FindObjectOfType(typeof(PlayerBehaviour));
        EnemySpawner spawner = (EnemySpawner)FindObjectOfType(typeof(EnemySpawner));

        foreach (LaserBehaviour laser in lasers)
        {
            Destroy(laser.gameObject);
        }

        foreach (GameObject enemy in spawner.GetEnemyList())
        {
            Destroy(enemy);
        }

        if (endGame)
        {
            Destroy(player.gameObject);
            Destroy(spawner.gameObject);
        }
    }




    private void ChangeLevel()
    {
        if (pB.GetScore() >= GetRequiredPoints())
        {
            level++;
            enemyMovSpeed *= mMM.levelMult;
            enemySpawnInterval /= mMM.levelMult;
        }
    }

    private int GetRequiredPoints()
    {
        int levelPoints = level * mMM.pointsPerLevel;
        int previousPoints = 0;
        for (int i = 1; i < level; i++)
        {
            previousPoints += i * mMM.pointsPerLevel;
        }
        return previousPoints + levelPoints;
    }

    private void OnEnable()
    {
        if (mMM.gSp != 0) enemyMovSpeed = mMM.gSp;
        if (mMM.gSpI != 0) enemySpawnInterval = mMM.gSpI;

        hasLevelProgression = mMM.useLevels;
    }

    public int GetCurrentLevel() => level;
}
