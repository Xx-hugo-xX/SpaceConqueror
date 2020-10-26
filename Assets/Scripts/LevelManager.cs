using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int levelStartTimerDuration;

    [HideInInspector] public bool isCountingDown = false;

    [HideInInspector] public int preMatchTimer;


    [SerializeField] public PlayerBehaviour pB;
    [SerializeField] private EnemySpawner eS;
    void Start()
    {
        StartCoroutine(LevelStartCountdown());
    }

    void Update()
    {
        
    }

    private IEnumerator LevelStartCountdown()
    {
        isCountingDown = true;

        pB.enabled = false;
        eS.enabled = false;
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

    public void DestroyAllShipsAndLasers()
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

        Destroy(player.gameObject);
        Destroy(spawner.gameObject);
    }
}
