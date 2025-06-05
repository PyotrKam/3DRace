using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CarChassis))]
public class Car : MonoBehaviour
{
    public event UnityAction<string> GearChanged;

   
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
    public float NormalizeLinearVelocity => chassis.LinearVelocity / maxSpeed;
    public float WheelSpeed => chassis.GetWheelSpeed();
    public float MaxSpeed => maxSpeed;

    public float EngineRpm => engineRpm;
    public float EngineMaxRpm => engineMaxRpm;



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
    public string GetSelectedGearName()
    {
        if (selectedGear == rearGear) return "r";

        if (selectedGear == 0) return "n";

        return (selectedGearIndex + 1).ToString();
    }
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

    public void ShiftToReverseGear()
    {
        selectedGear = rearGear;
        GearChanged?.Invoke(GetSelectedGearName());
    }

    public void ShiftToFirstGear()
    {
        ShiftGear(0);
    }

    public void ShiftToNeutral()
    {
        selectedGear = 0;
        GearChanged?.Invoke(GetSelectedGearName());
    }

    public void ShiftToReversGear()
    {
        selectedGear = rearGear;
        GearChanged?.Invoke(GetSelectedGearName());
    }

    private void ShiftGear(int gearIndex)
    {
        
        gearIndex = Mathf.Clamp(gearIndex,  0, gears.Length - 1);
        selectedGear = gears[gearIndex];
        selectedGearIndex = gearIndex;
        GearChanged?.Invoke(GetSelectedGearName());
    }

    private void UpdateEnineTorque()
    {
        engineRpm = engineMinRpm + Mathf.Abs(chassis.GetAverageRpm() * selectedGear * finalDriveRatio);
        engineRpm = Mathf.Clamp(engineRpm, engineMinRpm, engineMaxRpm);

        engineTorque = engineTorqueCurve.Evaluate(engineRpm / engineMaxRpm) * engineMaxTorque * finalDriveRatio * Mathf.Sign(selectedGear) * gears[0];
    }
}
