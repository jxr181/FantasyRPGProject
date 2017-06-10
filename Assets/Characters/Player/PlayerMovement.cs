using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    ThirdPersonCharacter m_Character;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster cameraRaycaster;
    Vector3 currentClickTarget;

    bool isInGamePadMode = false; // TODO consider making static

    [SerializeField] float moveStopWalkRadius = 0.2f;
        
    void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        m_Character = GetComponent<ThirdPersonCharacter>();
        currentClickTarget = transform.position;
    }

    // Fixed update is called in sync with physics
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.G)) // G for gamepad. TODO Allow player to remap later
        {
            isInGamePadMode = !isInGamePadMode; // Toggle mode
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
        Vector3 m_CamForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 m_Move = v * m_CamForward + h * Camera.main.transform.right;

        m_Character.Move(m_Move, false, false);
    }

    void ProcessMouseMovement()
    {
        if (Input.GetMouseButton(0))
        {

            switch (cameraRaycaster.layerHit) 
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
            m_Character.Move(playerToClickPoint - transform.position, false, false);
        }
        else
        {
            m_Character.Move(Vector3.zero, false, false);
        }
    }
}

