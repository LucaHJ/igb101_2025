using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour{

    public Animator anim;

    public float rotSpeed = 10;

    private float yaw; // Current accumulated yaw angle
    public float mouseSensitivity = 5f;

    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){

        ForwardMovement();

        Turning();

        Actions();

    }

    private void ForwardMovement(){
        if(Input.GetKey("w")){
            anim.SetBool("Walking", true);
            if (Input.GetKey(KeyCode.LeftShift)){
                anim.SetBool("Running", true);
            } else{
                anim.SetBool("Running", false);
            }
        } else if (Input.GetKeyUp("w")) {
            anim.SetBool("Walking", false);
            anim.SetBool("Running", false);
        }
    }

    private void Turning()
    {
        // Step 1: Accumulate horizontal mouse input
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;

        // Step 2: Create target rotation only on Y axis
        Quaternion targetRotation = Quaternion.Euler(0f, yaw, 0f);

        // Step 3: Smoothly rotate player model
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotSpeed);
    }

    private void Actions(){
        if(Input.GetKeyDown("e")){
            anim.SetBool("Waving", true);
        } else if(Input.GetKeyUp("e")){
            anim.SetBool("Waving", false);
        }
    }
}
