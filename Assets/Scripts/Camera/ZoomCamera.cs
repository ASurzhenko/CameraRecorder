using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomCamera : MonoBehaviour
{
    [SerializeField] float Sensitivity = 0.03f;
    [SerializeField] float ZoomMin, ZoomMax;
    Camera Cam;
    private void Start() {
        Cam = GetComponent<Camera>();
    }
    private void Update() {
        if(Input.touchCount == 2)
        {
            Touch touchA = Input.GetTouch(0);
            Touch touchB = Input.GetTouch(1);
            
            Vector3 dirA = touchA.position - touchA.deltaPosition;
            Vector3 dirB = touchB.position - touchB.deltaPosition;

            float posDistance = Vector3.Distance(touchA.position, touchB.position);
            float dirDistance = Vector3.Distance(dirA, dirB);

            float zoom = posDistance - dirDistance;

            var currentZoom = Cam.fieldOfView - zoom * Sensitivity; 
            Cam.fieldOfView = Mathf.Clamp(currentZoom, ZoomMin, ZoomMax);
        }
    }
}
