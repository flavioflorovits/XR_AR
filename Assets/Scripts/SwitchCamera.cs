using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;

public class SwitchCamera : MonoBehaviour
{
    [SerializeField] private ARCameraManager cameraManager;
    
    public void SwapCamera()
    {
        if (cameraManager.currentFacingDirection == CameraFacingDirection.User)
        {
            cameraManager.requestedFacingDirection = CameraFacingDirection.World;
        }
        else if (cameraManager.currentFacingDirection == CameraFacingDirection.World)
        {
            cameraManager.requestedFacingDirection = CameraFacingDirection.User;
        }
        else
        {
            Debug.LogWarning("Camera error.");
        }
    }


}