using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject player;

    public int currentPickups = 0;
    public int maxPickups = 3;
    public bool levelComplete = false;

    public Text pickupText;

    private void LevelCompleteCheck()
    {
        if (currentPickups >= maxPickups)
            levelComplete = true;
        else
            levelComplete = false;
    }

    private void UpdateGUI()
    {
        pickupText.text = "Pickups: " + currentPickups + "/" + maxPickups;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LevelCompleteCheck();
        UpdateGUI();
    }
}
