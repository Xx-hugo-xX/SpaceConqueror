using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    /*NORMAL MOVEMENT VARS
    [SerializeField] private float maxY;
    [SerializeField] private float minY;

    [SerializeField] private float constantMovSpeed;
    [SerializeField] private float incrementalMovSpeed;
    [SerializeField] private KeyCode incrementalUpButton;
    [SerializeField] private KeyCode incrementalDownButton;
    */

    [SerializeField] float[] ySnaps;

    [SerializeField] public KeyCode upButton;
    [SerializeField] public KeyCode downButton;
    [SerializeField] public KeyCode shootButton;

    int currentYSnap = 0;

    private UIManager uM;

    private SpriteRenderer sR;

    private int score = 0;

    private Transform canon;

    [SerializeField] private GameObject laser;

    public bool canShoot;

    private IntroLevelManager iLM;
    [SerializeField] private LevelManager lM;

    [SerializeField] private AudioSource thrusters;
    [SerializeField] private AudioSource laserShot;

    private void Start()
    {
        sR = GetComponent<SpriteRenderer>();
        uM = GameObject.Find("Canvas").GetComponent<UIManager>();
        canon = transform.GetChild(0);

        iLM = GameObject.Find("IntroLevelManager").GetComponent<IntroLevelManager>();
        if (uM.GetHandedness() == Handedness.Left) SwitchDirButtons();
    }

    private void Update()
    {
        if (iLM.enabled) canShoot = false;
        else canShoot = true;

        UpdateMovement();
        ShootLaser();
    }

    private void UpdateMovement()
    {
        int newYSnap = currentYSnap;

        if (Input.GetKeyDown(upButton))
        {
            if (currentYSnap + 1 <= ySnaps.Length - 1) newYSnap++;
        }

        else if (Input.GetKeyDown(downButton))
        {
            if (currentYSnap - 1 >= 0) newYSnap--;
        }

        if (newYSnap != currentYSnap)
        {
            currentYSnap = newYSnap;

            transform.position = new Vector2(transform.position.x, ySnaps[currentYSnap]);
        }
    }

    private void ShootLaser()
    {
        if (canShoot && Input.GetKeyDown(shootButton))
        {
            Instantiate(laser, canon.position, Quaternion.identity);
            laserShot.PlayOneShot(laserShot.clip);
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

    public void AddScore(int addition) => score += addition;

    public void KilledEnemy() { score += 2; }

    private void SwitchDirButtons()
    {
        KeyCode tempButton = upButton;

        upButton = downButton;
        downButton = tempButton;
    }

    private void ResetPlayer()
    {
        currentYSnap = 0;
        transform.position = new Vector2(transform.position.x, ySnaps[currentYSnap]);
    }

    public void StartThrusters()
    {
        thrusters.Play();
    }

    private void OnDisable()
    {
        //thrusters.Stop();
        ResetPlayer();
    }
}
