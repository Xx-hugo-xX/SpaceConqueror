using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private float maxY;
    [SerializeField] private float minY;

    [SerializeField] private float constantMovSpeed;
    [SerializeField] private float incrementalMovSpeed;

    [SerializeField] private KeyCode incrementalUpButton;
    [SerializeField] private KeyCode incrementalDownButton;
    [SerializeField] private KeyCode shootButton;


    private UIManager uM;

    private SpriteRenderer sR;

    private int score = 0;

    private Transform canon;

    [SerializeField] private GameObject laser;

    private void Start()
    {
        sR = GetComponent<SpriteRenderer>();
        uM = GameObject.Find("Canvas").GetComponent<UIManager>();
        canon = transform.GetChild(0);
    }

    private void Update()
    {
        CheckConstantMove();
        CheckIncrementalMove();
        ShootLaser();
    }

    private void CheckConstantMove()
    {
        float wantedY = transform.position.y;

        wantedY += Input.GetAxisRaw("Vertical") * constantMovSpeed * Time.deltaTime;

        if (maxY >= wantedY && wantedY >= minY) transform.position = new Vector2(transform.position.x, wantedY);
    }

    private void CheckIncrementalMove()
    {
        float wantedY = transform.position.y;

        if (Input.GetKeyDown(incrementalUpButton)) wantedY += incrementalMovSpeed;
        else if (Input.GetKeyDown(incrementalDownButton)) wantedY -= incrementalMovSpeed;

        if (maxY >= wantedY && wantedY >= minY) transform.position = new Vector2(transform.position.x, wantedY);
    }

    private void ShootLaser()
    {
        if (Input.GetKeyDown(shootButton))
        {
            Instantiate(laser, canon.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("EnemyLaser")||
            col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            KillPlayer();
        }
    }

    private void KillPlayer()
    {

        sR.enabled = false;
        uM.ShowFinalScore();
    }

    public int GetScore() => score;

    public void KilledEnemy() { score++; }
}
