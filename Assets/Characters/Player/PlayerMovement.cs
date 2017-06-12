using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    ThirdPersonCharacter thirdPersonCharacter;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster cameraRaycaster;
    Vector3 currentClickTarget;

    [SerializeField] float moveStopWalkRadius = 0.2f;

    bool isInGamePadMode = false; 

       
    void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
        currentClickTarget = transform.position;
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

    //void ProcessMouseMovement()
    //{
    //    if (Input.GetMouseButton(0))
    //    {

    //        switch (cameraRaycaster.currentLayerHit) 
    //        {
    //            case Layer.Walkable:
    //                currentClickTarget = cameraRaycaster.hit.point;
    //                break;

    //            case Layer.Enemy:
    //                print("Not moving to enemy!");
    //                break;

    //            default:
    //                print("Unexpected Layer Found");
    //                return;
    //        }

    //    }
    //    var playerToClickPoint = currentClickTarget - transform.position;
    //    if (playerToClickPoint.magnitude >= moveStopWalkRadius)
    //    {
    //        thirdPersonCharacter.Move(playerToClickPoint, false, false);
    //    }
    //    else
    //    {
    //        thirdPersonCharacter.Move(Vector3.zero, false, false);
    //    }
    //}

    void OnDrawGizmos()
    {
        print("Draw Gizmos");
    }
}

