using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelSwitch : MonoBehaviour
{
    GameManager gameManager;
    public string nextLevel;

    private void OnTriggerEnter(Collider otherObject)
    {
        if(otherObject.transform.tag == "Player")
        {
            if (gameManager.levelComplete)
            {
                SceneManager.LoadScene(nextLevel);
            }
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
