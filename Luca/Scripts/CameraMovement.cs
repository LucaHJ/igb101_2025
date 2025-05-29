using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public GameObject[] cameraNodes;
    public GameObject[] objects;

    private int cameraIndex = 0;

    private float proximity = 0.1f;
    public float moveSpeed = 5.0f;
    public float rotSpeed = 5.0f;
    private float adjRotSpeed;
    private Quaternion targetRotation;

    private int stepsToMove = 0;
    private int stepDirection = 0; // 1 for forward (W), -1 for backward (S)
    private bool isMoving = false;

    void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        HandleInput();
        Movement();
    }

    private void HandleInput()
    {
        if (!isMoving)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                stepDirection = 1;
                stepsToMove = 20;
                isMoving = true;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                stepDirection = -1;
                stepsToMove = 20;
                isMoving = true;
            }
        }
    }

    private void Movement()
    {
        if (!isMoving) return;

        if (Vector3.Distance(transform.position, cameraNodes[cameraIndex].transform.position) < proximity)
        {
            if (stepsToMove > 0)
            {
                int nextIndex = cameraIndex + stepDirection;
                // Clamp within bounds
                if (nextIndex >= 0 && nextIndex < cameraNodes.Length)
                {
                    cameraIndex = nextIndex;
                    stepsToMove--;
                }
                else
                {
                    // Stop if we hit an edge
                    stepsToMove = 0;
                    isMoving = false;
                }
            }
            else
            {
                // Finished all steps
                isMoving = false;
            }
        }

        // Movement
        transform.position = Vector3.MoveTowards(transform.position, cameraNodes[cameraIndex].transform.position, moveSpeed * Time.deltaTime);

        // Rotation
        if (objects[cameraIndex])
        {
            targetRotation = Quaternion.LookRotation(objects[cameraIndex].transform.position - transform.position);
            adjRotSpeed = Mathf.Min(rotSpeed * Time.deltaTime, 1);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, adjRotSpeed);
        }

        // Audio
        if (objects[cameraIndex].GetComponent<AudioSource>() != null)
        {
            if (!objects[cameraIndex].GetComponent<AudioSource>().isPlaying)
                objects[cameraIndex].GetComponent<AudioSource>().Play();
        }
    }
}
