using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehaviour : MonoBehaviour
{
    [SerializeField] private float maxX;
    [SerializeField] private float minX;

    [SerializeField] private float speed;
    [SerializeField] public LaserUser lU;

    private PlayerBehaviour pB;


    private void Start()
    {
        pB = GameObject.Find("Player").GetComponent<PlayerBehaviour>();
        if (lU == LaserUser.Enemy) speed *= -1;
    }

    private void Update()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);
        CheckOutOfBounds();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (lU == LaserUser.Player &&
            col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            pB.KilledEnemy();
            Destroy(col.gameObject);
            col.GetComponent<EnemyBehaviour>().Kill();
            Destroy(transform.gameObject);
        }

        else if (col.gameObject.layer == LayerMask.NameToLayer("EnemyLaser")
            || col.gameObject.layer == LayerMask.NameToLayer("PlayerLaser"))
        {
            Destroy(col.gameObject);
            Destroy(transform.gameObject);
        }

    }

    private void CheckOutOfBounds()
    {
        if (transform.position.x <= minX || transform.position.x >= maxX)
            Destroy(transform.gameObject);

    }
}
