using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FAPlayerBehaviour : MonoBehaviour
{
    [SerializeField] int[] ySnaps;

    [SerializeField] private KeyCode upButton;
    [SerializeField] private KeyCode downButton;
    [SerializeField] private KeyCode shootButton;

    private int currentYSnap;

    // Start is called before the first frame update
    void Start()
    {
        currentYSnap = 0;
        transform.position = new Vector2(transform.position.x, ySnaps[currentYSnap]);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMovement();
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
}
