using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class CookieEat : MonoBehaviour
{

    [SerializeField] private ARFaceManager faceManager;
    [SerializeField] private ARCameraManager cameraManager;
    [SerializeField] private TMPro.TextMeshProUGUI cookieText;

    [SerializeField] private TMPro.TextMeshProUGUI debugText;


    private GameObject cookie;

    private void OnEnable() => faceManager.facesChanged += OnFaceChanged;
    private void OnDisable() => faceManager.facesChanged -= OnFaceChanged;

    private List<ARFace> faces = new List<ARFace>();

    private int cookieEatCount = 0;
    private bool debounce = false;

    private void Update()
    {
        if(cameraManager.currentFacingDirection == CameraFacingDirection.User)
        {
            if (faces.Count > 0)
            {
                Vector3 lowerLips = faces[0].vertices[14];
                debugText.text = lowerLips.y.ToString();
                if (lowerLips.y < -0.055f && !debounce)
                {
                    cookie = faces[0].transform.Find("Cookie").gameObject;
                    debounce = true;
                    cookie.SetActive(true);
                    cookieEatCount++;
                }
                else if (lowerLips.y > -0.045f && debounce)
                {
                    debounce = false;
                    cookie.SetActive(false);
                    cookieText.text = "Total cookies eaten: " + cookieEatCount.ToString();
                }
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
            debounce = false;
        }
    }

}
