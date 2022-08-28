using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] protected Camera Cam;
    [SerializeField] protected float Sensitivity;
    [SerializeField] protected float RotationSpeed;
    protected float ZoomMin = 16f;
    protected float ZoomMax = 100f;
    protected Vector2 rotationXMinMax = new Vector2(0, 60);
    public bool rotate;
    protected virtual void Control(){}
}
