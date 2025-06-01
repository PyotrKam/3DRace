using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CarChassis))]
public class Car : MonoBehaviour
{
    

   
    [SerializeField] private float maxSteerAngle;
    [SerializeField] private float maxBrakeTorque;

    [Header("Engine")]
    [SerializeField] private AnimationCurve engineTorqueCurve;
    [SerializeField] private float engineMaxTorque;
    [SerializeField] private float engineTorque;
    [SerializeField] private float engineRpm;
    [SerializeField] private float engineMinRpm;
    [SerializeField] private float engineMaxRpm;


    [Header("Gearbox")]
    [SerializeField] private float[] gears;
    [SerializeField] private float finalDriveRatio;
    [SerializeField] private int selectedGearIndex;


    // DEBUG
    [SerializeField] private float selectedGear;
    [SerializeField] private float rearGear;

    [SerializeField] private float upShiftEngineRpm;
    [SerializeField] private float downShiftEngineRpm;


        
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

        UpdateEnineTorque();

        AutoGearShift();

        if (LinearVelecity >= maxSpeed)
        {
            engineTorque = 0;
        }

        chassis.MotorTroque = engineTorque * ThrottleControl;
        chassis.SteerAngle = maxSteerAngle * SteerControl;
        chassis.BrakeTroque = maxBrakeTorque * BrakeControl;
    } 

    //Gearbox

    private void AutoGearShift()
    {
        
        if (selectedGear < 0) return;

        if (engineRpm >= upShiftEngineRpm)
        {
            
            UpGear();
        }

        if (engineRpm < downShiftEngineRpm)
            DownGear();
                
    }

    public void UpGear()
    {
        
        ShiftGear(selectedGearIndex + 1);
    }

    public void DownGear()
    {
        ShiftGear(selectedGearIndex - 1);
    }

    public void SheftToReverseGear()
    {
        selectedGear = rearGear;      
    }

    public void ShiftToFirstGear()
    {
        ShiftGear(0);
    }

    public void ShiftToNetral()
    {
        selectedGear = 0;
    }

    public void ShiftToReversGear()
    {
        selectedGear = rearGear;
    }

    private void ShiftGear(int gearIndex)
    {
        
        gearIndex = Mathf.Clamp(gearIndex,  0, gears.Length - 1);
        selectedGear = gears[gearIndex];
        selectedGearIndex = gearIndex;
        Debug.Log($"{selectedGear}");
    }

    private void UpdateEnineTorque()
    {
        engineRpm = engineMinRpm + Mathf.Abs(chassis.GetAverageRpm() * selectedGear * finalDriveRatio);
        engineRpm = Mathf.Clamp(engineRpm, engineMinRpm, engineMaxRpm);

        engineTorque = engineTorqueCurve.Evaluate(engineRpm / engineMaxRpm) * engineMaxTorque * finalDriveRatio * Mathf.Sign(selectedGear) * gears[0];
    }
}
