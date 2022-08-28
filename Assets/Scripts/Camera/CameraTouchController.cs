using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraTouchController : Controller
{
    [SerializeField] private bool rotateY;
    float rotateX;
    float rotateY_;
    float rotX;
    void Update() => Control();
    protected override void Control()
    {
        if (Input.touchCount == 1)
        {
            Touch screenTouch = Input.GetTouch(0);
            
            if (screenTouch.phase == TouchPhase.Moved)
            {
                if (rotateY)
                {
                    transform.Rotate(screenTouch.deltaPosition.y, 0f, 0f * Time.deltaTime * RotationSpeed);
                    rotX = transform.localRotation.eulerAngles.x;
                    ClampRotation();
                }
                else
                    transform.Rotate(0f, screenTouch.deltaPosition.x, 0f * Time.deltaTime * RotationSpeed);
            }
        }
        else if (Input.touchCount == 2)
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
    private void ClampRotation()
    {
        // We do this so that now the angle rotX is guaranteed to be somewhere between min and max value
        if (rotX >= 270)
        {
            rotX -= 360;
        }

        if (rotX > rotationXMinMax.y)
        {
            rotX = rotationXMinMax.y;
        }
        else if (rotX < rotationXMinMax.x)
        {
            rotX = rotationXMinMax.x;
        }
        else
        {
            rotX += rotateY_;
        }

        transform.localRotation = Quaternion.AngleAxis(rotX, Vector3.right);
    }
}
