using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private float movSpeed;

    [SerializeField] private Transform canon;
    [SerializeField] private float shotInterval;

    [SerializeField] private GameObject laser;

    private EnemySpawner spawner;

    private void Start()
    {
        spawner = GetComponentInParent<EnemySpawner>();

        InvokeRepeating("ShootLaser", 0.5f, shotInterval);
    }

    private void Update()
    {
        transform.Translate(-movSpeed * Time.deltaTime, 0, 0);
    }

    private void ShootLaser()
    {
        if (spawner.canShoot)
        {
            GameObject newObstacle = Instantiate(laser, canon.position, Quaternion.identity);
        }
    }


    public void Kill()
    {
        List<GameObject> enemyList = spawner.GetEnemyList();
        int indOfObj = enemyList.IndexOf(transform.gameObject);

        enemyList.RemoveAt(indOfObj);

        Destroy(transform.gameObject);
    }
}
