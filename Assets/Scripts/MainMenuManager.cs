using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private float[] easyParams = new float[6];
    [SerializeField] private float[] mediumParams = new float[6];
    [SerializeField] private float[] hardParams = new float[6];

    [SerializeField] private Toggle easyToggle;
    [SerializeField] private Toggle mediumToggle;
    [SerializeField] private Toggle hardToggle;
    [SerializeField] private Toggle levelToggle;


    [SerializeField] private AudioSource menuSound;

    [HideInInspector] public float tSp;
    [HideInInspector] public float tSpI;
    [HideInInspector] public float gSp;
    [HideInInspector] public float gSpI;
    [HideInInspector] public float gShI;
    [HideInInspector] public float gShSp;

    public bool useLevels;
    public float levelMult;
    public int pointsPerLevel;

    private void Start()
    {
        StartMusic();
    }
    public void StartGame()
    {
        if (easyToggle.isOn)
        {
            tSp   = easyParams[0];
            tSpI  = easyParams[1];
            gSp   = easyParams[2];
            gSpI  = easyParams[3];
            gShI  = easyParams[4];
            gShSp = easyParams[5];
        }

        else if (mediumToggle.isOn)
        {
            tSp   = mediumParams[0];
            tSpI  = mediumParams[1];
            gSp   = mediumParams[2];
            gSpI  = mediumParams[3];
            gShI  = mediumParams[4];
            gShSp = mediumParams[5];
        }
        
        else if (hardToggle.isOn)
        {
            tSp   = hardParams[0];
            tSpI  = hardParams[1];
            gSp   = hardParams[2];
            gSpI  = hardParams[3];
            gShI  = hardParams[4];
            gShSp = hardParams[5];
        }

        useLevels = levelToggle.isOn;

        //print("Tut Enemy Speed: "           + tSp);
        //print("Tut Enemy Spawn Interval: "  + tSpI);
        //print("Game Enemy Speed: "          + gSp);
        //print("Game Enemy Spawn Interval: " + gSpI);
        //print("Game Enemy Shot Interval: "  + gShI);
        //print("Game Enemy Shot Speed: "     + gShSp);

    }
    public void StartMusic(){ menuSound.Play(); }
    public void StopMusic(){ menuSound.Stop(); }
}
