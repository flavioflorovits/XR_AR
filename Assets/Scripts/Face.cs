using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class Face : MonoBehaviour
{

    [SerializeField] private ARFaceManager faceManager;
    [SerializeField] private ARCameraManager cameraManager;

    [SerializeField] private TMPro.TextMeshProUGUI debugText;

    private void OnEnable() => faceManager.facesChanged += OnFaceChanged;
    private void OnDisable() => faceManager.facesChanged -= OnFaceChanged;

    private List<ARFace> faces = new List<ARFace>();

    private void Update()
    {
        if(cameraManager.currentFacingDirection == CameraFacingDirection.User)
        {
            if (faces.Count > 0)
            {
                Vector3 lowerLips = faces[0].vertices[14];
                debugText.text = lowerLips.ToString("F3");
            }
        }
    }

    private void OnFaceChanged(ARFacesChangedEventArgs eventArgs)
    {
        foreach(var newFace in eventArgs.added)
        {
            faces.Add(newFace);
        }

        foreach(var lostFace in eventArgs.removed)
        {
            faces.Remove(lostFace);
        }
    }

}
