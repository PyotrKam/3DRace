using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CarChassis))]
public class Car : MonoBehaviour
{
    

    [SerializeField] private float maxMotorTorque;
    [SerializeField] private float maxSteerAngle;
    [SerializeField] private float maxBrakeTorque;

    private CarChassis chassis;
    public float ThrottleControl;
    public float SteerControl;
    public float BrakeControl;
    

    private void Start()
    {
        chassis = GetComponent<CarChassis>(); 
    }

    private void Update()
    {
        chassis.MotorTroque = maxMotorTorque * ThrottleControl;
        chassis.SteerAngle = maxSteerAngle * SteerControl;
        chassis.BrakeTroque = maxBrakeTorque * BrakeControl;
    }  
}
