using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarChassis : MonoBehaviour
{
    [SerializeField] private WheelAxle[] wheelAxles;

    public float MotorTroque;
    public float BrakeTroque;
    public float SteerAngle;

    private void FixedUpdate()
    {
        UpdateWheelAxles();
    }

    private void UpdateWheelAxles()
    {
        for (int i = 0; i < wheelAxles.Length; i++)
        {
            wheelAxles[i].Update();

            wheelAxles[i].ApplyMotorTorque(MotorTroque);
            wheelAxles[i].ApplySteerAngle(SteerAngle);
            wheelAxles[i].ApplyBreakTorque(BrakeTroque);
        }
    }
}
