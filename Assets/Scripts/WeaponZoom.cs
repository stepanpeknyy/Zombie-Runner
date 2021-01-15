using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] Camera zoomCamera;
    [SerializeField] float zoomedOutFOV = 60f;
    [SerializeField] float zoomedInFOV = 20f;
    [SerializeField] float zoomedInSensitivity = 0.5f;
    [SerializeField] float zoomedOutSensitivity = 2f;
    [SerializeField] RigidbodyFirstPersonController fpController;
    
    bool zoomedInToggle = false;

    private void OnDisable()
    {
        ZoomOut();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (zoomedInToggle ==false)
            {
                ZoomIn();
            }
            else
            {
                ZoomOut();
            }
        }
    }

    private void ZoomOut()
    {
        zoomedInToggle = false;
        zoomCamera.fieldOfView = zoomedOutFOV;
        fpController.mouseLook.XSensitivity = zoomedOutSensitivity;
        fpController.mouseLook.YSensitivity = zoomedOutSensitivity;
    }

    private void ZoomIn()
    {
        zoomedInToggle = true;
        zoomCamera.fieldOfView = zoomedInFOV;
        fpController.mouseLook.XSensitivity = zoomedInSensitivity;
        fpController.mouseLook.YSensitivity = zoomedInSensitivity;
    }
}
