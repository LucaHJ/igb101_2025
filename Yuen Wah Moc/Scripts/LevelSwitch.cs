using System.Collections;
using System.Collections.Generic; 
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwitch: MonoBehaviour {
    public GameManager gameManager;
    public string nextLevel;

    void Start(){
    }
    private void OnTriggerEnter(Collider otherObject){
        if(otherObject.transform.tag == "Player"){
            if(gameManager.levelComplete){ 
                SceneManager.LoadScene(nextLevel);
            }
        }
    }
}
