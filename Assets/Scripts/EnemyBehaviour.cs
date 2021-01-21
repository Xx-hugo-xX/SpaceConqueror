using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] public float movSpeed;

    [SerializeField] private Transform canon;
    [SerializeField] public float shotInterval;
    [SerializeField] public float shotSpeed;

    [SerializeField] private GameObject laser;
    [SerializeField] private AudioSource laserShot;
    [SerializeField] private MainMenuManager mMM;

    [HideInInspector] public LevelManager lM;

    private int currentLevel = 1;
    private EnemySpawner spawner;

    private void Start()
    {
        spawner = GetComponentInParent<EnemySpawner>();
    }

    private void Update()
    {
        transform.Translate(-movSpeed * Time.deltaTime, 0, 0);
        if (lM.hasLevelProgression && lM.enabled && currentLevel != lM.GetCurrentLevel()) UpdateValues();

    }

    private void ShootLaser()
    {
        if (spawner.canShoot)
        {
            GameObject newShot = Instantiate(laser, canon.position, Quaternion.identity);
            laserShot.PlayOneShot(laserShot.clip);

            newShot.GetComponent<LaserBehaviour>().speed = shotSpeed;
        }
    }


    public void Kill()
    {
        List<GameObject> enemyList = spawner.GetEnemyList();
        int indOfObj = enemyList.IndexOf(transform.gameObject);

        enemyList.RemoveAt(indOfObj);

        Destroy(transform.gameObject);
    }

    private void UpdateValues()
    {
        CancelInvoke();
        lM.DestroyAllShipsAndLasers(false);
        shotInterval /= mMM.levelMult;
        shotSpeed = lM.enemyMovSpeed + 1;
        currentLevel++;
        InvokeRepeating("SpawnObject", 0, shotInterval);
    }

    private void OnEnable()
    {
        if (mMM.gShI != 0) shotInterval = mMM.gShI;
        if (mMM.gShSp != 0) shotSpeed = mMM.gShSp;

        InvokeRepeating("ShootLaser", .05f, shotInterval);
    }
}
