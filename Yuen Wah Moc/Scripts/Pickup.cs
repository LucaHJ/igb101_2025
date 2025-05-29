using System.Collections;
using System.Collections.Generic; 
using UnityEngine;

public class Pickup: MonoBehaviour{

    public GameManager gameManager;
    public ParticleSystem myParticleSystem;
    void Start(){

    }

    private void OnTriggerEnter(Collider otherObject){ 
        if(otherObject.transform.tag == "Player"){
            gameManager.currentPickups += 1;
            myParticleSystem.transform.localPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            myParticleSystem.Play();
            Destroy(this.gameObject);
        }
    }
}