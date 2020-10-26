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

    [SerializeField] private float spawnInterval = 0.5f;

    [SerializeField] private GameObject obstacle;

    private List<GameObject> enemyList;
    
    void Start()
    {
        enemyList = new List<GameObject>();
        InvokeRepeating("SpawnObject", 0, spawnInterval);
    }

    void Update()
    {
        foreach (GameObject gO in enemyList)
        {
            if (gO.transform.position.x <= endingX)
            {
                enemyList.Remove(gO);
                Destroy(gO);
            }
        }
    }

    private void SpawnObject()
    {
        Vector2 obstaclePos = new Vector2(startingX, Random.Range(minY, maxY));

        GameObject newObstacle = Instantiate(obstacle, obstaclePos, Quaternion.identity, transform);

        enemyList.Add(newObstacle);
    }

    public List<GameObject> GetEnemyList() => enemyList;
}
