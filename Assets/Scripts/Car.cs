using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CarChassis))]
public class Car : MonoBehaviour
{
    

   
    [SerializeField] private float maxSteerAngle;
    [SerializeField] private float maxBrakeTorque;

    [SerializeField] private AnimationCurve engineTorqueCurve;
    [SerializeField] private float maxMotorTorque;
    [SerializeField] private float maxSpeed;
    

    

    public float LinearVelecity => chassis.LinearVelocity;
    public float WheelSpeed => chassis.GetWheelSpeed();
    public float MaxSpeed => maxSpeed;



    private CarChassis chassis;

    [SerializeField] public float linearVelecity;

    public float ThrottleControl;
    public float SteerControl;
    public float BrakeControl;
    

    private void Start()
    {
        chassis = GetComponent<CarChassis>(); 
    }

    private void Update()
    {
        linearVelecity = LinearVelecity;

        float engineTorque = engineTorqueCurve.Evaluate(LinearVelecity / maxSpeed) * maxMotorTorque;

        if (LinearVelecity >= maxSpeed)
        {
            engineTorque = 0;
        }

        chassis.MotorTroque = engineTorque * ThrottleControl;
        chassis.SteerAngle = maxSteerAngle * SteerControl;
        chassis.BrakeTroque = maxBrakeTorque * BrakeControl;
    }  
}
