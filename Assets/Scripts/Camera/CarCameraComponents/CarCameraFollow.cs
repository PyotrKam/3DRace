using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCameraFollow : CarCameraComponent
{
    

    [Header("Offset")]
    [SerializeField] private float viewHeight;
    [SerializeField] private float height;
    [SerializeField] private float distance;

    [Header("Damping")]
    [SerializeField] private float rotationDamping;
    [SerializeField] private float heightDamping;
    [SerializeField] private float speedThreshold;

    private Transform target;
    private new Rigidbody rigidbody;

    private void FixedUpdate()
    {
        Vector3 velocity = rigidbody.velocity;
        Vector3 targetRotation = target.eulerAngles;

        if (velocity.magnitude > speedThreshold)
        {
            targetRotation = Quaternion.LookRotation(velocity, Vector3.up).eulerAngles;
        }

        float currentAngle = Mathf.LerpAngle(transform.eulerAngles.y, targetRotation.y, rotationDamping * Time.fixedDeltaTime);
        float currentHeight = Mathf.Lerp(transform.position.y, target.position.y + height, heightDamping * Time.fixedDeltaTime);

        //Position
        Vector3 positionOffset = Quaternion.Euler(0, currentAngle, 0) * Vector3.forward * distance;
        transform.position = target.position - positionOffset;
        transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);

        //ROtation
        transform.LookAt(target.position + new Vector3(0, viewHeight, 0) );
    }

    public override void SetProperties(Car car, Camera camera)
    {
        base.SetProperties(car, camera);

        target = car.transform;
        rigidbody = car.Rigidbody;

    }
}
