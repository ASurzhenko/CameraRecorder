using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMouseController : Controller
{
    [SerializeField] private Transform target;
    private float rotationY;
    private float rotationX;
    private float distanceFromTarget;
    private Vector3 currentRotation;
    private Vector3 smoothVelocity = Vector3.zero;
    private float zoomSensitivity = 10f;
    private void Start()
    {
        distanceFromTarget = Vector3.Distance(transform.position, target.position);
    }
    void Update()
    {
        if(Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            return;
        
        Control();
    }
    protected override void Control()
    {
        if (Input.GetMouseButtonDown(0))
        {
            rotate = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            rotate = false;
        }

        if (rotate)
        {
            float mouseX = Input.GetAxis("Mouse X") * Sensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * Sensitivity;

            rotationY += mouseX;
            rotationX += mouseY;

            // Apply clamping for x rotation 
            rotationX = Mathf.Clamp(rotationX, rotationXMinMax.x, rotationXMinMax.y);
            Vector3 nextRotation = new Vector3(rotationX, rotationY);

            // Apply damping between rotation changes
            currentRotation = Vector3.SmoothDamp(currentRotation, nextRotation, ref smoothVelocity, RotationSpeed);
            transform.localEulerAngles = currentRotation;
        }

        // Zoom
        float fov = Cam.fieldOfView;
        fov += Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity;
        fov = Mathf.Clamp(fov, ZoomMin, ZoomMax);
        Cam.fieldOfView = fov;
    }
}
