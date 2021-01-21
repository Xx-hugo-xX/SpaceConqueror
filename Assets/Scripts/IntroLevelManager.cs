using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroLevelManager : MonoBehaviour
{
    [SerializeField] private LevelManager lM;
    private UIManager uM;

    [SerializeField] private int levelStartTimerDuration;

    [HideInInspector] public bool isCountingDown = false;

    [HideInInspector] public int preMatchTimer;

    [HideInInspector] public PlayerBehaviour pB;
    private EnemySpawner eS;
    [SerializeField] private MainMenuManager mMM;

    public float enemyMovSpeed;
    public float enemySpawnInterval;

    void Start()
    {
        pB = GameObject.Find("Player").GetComponent<PlayerBehaviour>();
        eS = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        uM = GameObject.Find("Canvas").GetComponent<UIManager>();

        pB.enabled = false;
        eS.enabled = false;
        StartCoroutine(LevelStartCountdown());
    }

    void Update()
    {
        if (!isCountingDown)
        {
            if (Input.GetKeyDown(KeyCode.P) || (Input.GetKey(pB.upButton) && Input.GetKey(pB.downButton) && Input.GetKey(pB.shootButton)))
            {
                pB.AddScore(-pB.GetScore());
                uM.HideIntroMessagePanel();
                pB.enabled = false;
                eS.enabled = false;
                DestroyEnemyShipsAndLasers();
                lM.gameObject.SetActive(true);
            }
        }
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

    public void DestroyEnemyShipsAndLasers()
    {
        LaserBehaviour[] lasers = (LaserBehaviour[])FindObjectsOfType(typeof(LaserBehaviour));

        foreach (LaserBehaviour laser in lasers)
        {
            Destroy(laser.gameObject);
        }

        eS.DestroyAllShips();
    }

    private void OnEnable()
    {
        if (mMM.tSp != 0) enemyMovSpeed = mMM.tSp;
        if (mMM.tSpI != 0) enemySpawnInterval = mMM.tSpI;
    }
}
