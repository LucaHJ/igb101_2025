using UnityEngine;

public class Doortest : MonoBehaviour
{
    private Animation doorAnimation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        doorAnimation = GetComponent<Animation>();
        
    }

    // Update is called once per frame
    void Update(){

        if (Input.GetKeyDown(KeyCode.F)) // Used KeyCode for clarity
        {
            if (doorAnimation != null) // Ensure the animation component exists
            {
                doorAnimation.Play();
            }
            else
            {
                Debug.LogWarning("No Animation component found!");
            }
        }
    }
}
