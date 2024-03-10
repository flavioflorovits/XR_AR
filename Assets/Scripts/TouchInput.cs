using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TouchInput : MonoBehaviour
{
    [SerializeField] private TMP_Text debugText;
    [SerializeField] private GameObject ballPrefab;
    private ARRaycastManager arrcm;

    [SerializeField] private ARCameraManager cameraManager;

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    TrackableType trackableTypes = TrackableType.PlaneWithinPolygon;

    private void Start()
    {
        arrcm = GetComponent<ARRaycastManager>();
    }

    public void SingleTap(InputAction.CallbackContext ctx)
    {
        if(ctx.phase == InputActionPhase.Performed) 
        {
            var touchPos = ctx.ReadValue<Vector2>();
            debugText.text = touchPos.ToString();

            if (arrcm.Raycast(touchPos, hits, trackableTypes))
            {
                var ball = Instantiate(ballPrefab, hits[0].pose.position, new Quaternion());
            }

        }
    }

    public void DoubleTap(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Performed)
        {
            debugText.text = "Changed camera";

            if(cameraManager.currentFacingDirection == CameraFacingDirection.World)
            {
                GetComponent<ARRaycastManager>().enabled = false;
                GetComponent<ARPlaneManager>().enabled = false;
                GetComponent<ARFaceManager>().enabled = true;
                cameraManager.requestedFacingDirection = CameraFacingDirection.User;

            }
            else if(cameraManager.currentFacingDirection == CameraFacingDirection.User) 
            {
                GetComponent<ARRaycastManager>().enabled = true;
                GetComponent<ARPlaneManager>().enabled = true;
                GetComponent<ARFaceManager>().enabled = false;
                cameraManager.requestedFacingDirection = CameraFacingDirection.World;
            }
            else
            {
                Debug.LogWarning("Camera facing direction not set.");
            }

        }
    }
}
