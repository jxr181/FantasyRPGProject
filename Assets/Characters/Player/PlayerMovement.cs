using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    ThirdPersonCharacter thirdPersonCharacter;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster cameraRaycaster;
    Vector3 currentClickTarget;


    bool isInGamePadMode = false; 

    [SerializeField] float moveStopWalkRadius = 0.2f;
        
    void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
        currentClickTarget = transform.position;
    }

    // Fixed update is called in sync with physics
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.G)) // G for gamepad. TODO Allow player to remap later
        {
            isInGamePadMode = !isInGamePadMode; // Toggle mode
            currentClickTarget = transform.position; // Clear clickTarget
        }

        if (isInGamePadMode)
        {
            ProcessGamePadMovement();
        }
        else
        {
            ProcessMouseMovement();
        }
        
    }

    void ProcessGamePadMovement()
    {
        // read inputs
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // calculate camera relative direction to move:
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 movement = v * cameraForward + h * Camera.main.transform.right;

        thirdPersonCharacter.Move(movement, false, false);
    }

    void ProcessMouseMovement()
    {
        if (Input.GetMouseButton(0))
        {

            switch (cameraRaycaster.currentLayerHit) 
            {
                case Layer.Walkable:
                    currentClickTarget = cameraRaycaster.hit.point;
                    break;

                case Layer.Enemy:
                    print("Not moving to enemy!");
                    break;

                default:
                    print("DON'T KNOW WHAT TO DO");
                    return;
            }

        }
        var playerToClickPoint = currentClickTarget - transform.position;
        if (playerToClickPoint.magnitude >= moveStopWalkRadius)
        {
            thirdPersonCharacter.Move(playerToClickPoint - transform.position, false, false);
        }
        else
        {
            thirdPersonCharacter.Move(Vector3.zero, false, false);
        }
    }
}

