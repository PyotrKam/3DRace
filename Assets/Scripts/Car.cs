using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] private WheelCollider[] wheelColliders;
    [SerializeField] private Transform[] wheelMeshs;
    [SerializeField] private float motorTroque;
    [SerializeField] private float brakeTroque;
    [SerializeField] private float steerAngle;

    private void Update()
    {
        for (int i = 0; i < wheelColliders.Length; i++)
        {
            wheelColliders[i].motorTorque = Input.GetAxis("Vertical") * motorTroque;
            wheelColliders[i].brakeTorque = Input.GetAxis("Jump") * brakeTroque;

            Vector3 position;
            Quaternion rotation;
            wheelColliders[i].GetWorldPose(out position, out rotation);

            wheelMeshs[i].position = position;
            wheelMeshs[i].rotation = rotation;
        }

        wheelColliders[0].steerAngle = Input.GetAxis("Horizontal") * steerAngle;
        wheelColliders[1].steerAngle = Input.GetAxis("Horizontal") * steerAngle;
    }
}
