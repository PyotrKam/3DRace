using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuspensionArm : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float factor;

    private float baseOffset;

    private void Start()
    {
        baseOffset = target.localPosition.y;

    }

    private void Update()
    {
        transform.localEulerAngles = new Vector3(0, 0, (target.localPosition.y - baseOffset) * factor);
    }
}
