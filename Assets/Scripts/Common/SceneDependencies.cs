using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDependency<T>
{
    void Construct(T obj);
}

public class SceneDependencies : MonoBehaviour
{
    [SerializeField] private RaceStateTracker raceStatTracker;
    [SerializeField] private CarInputControl carInputControl;
    [SerializeField] private TrackpointCircuit trackpointCircuit;
    [SerializeField] private Car car;
    [SerializeField] private CarCameraController carCameraController;
    [SerializeField] private RaceTimeTracker raceTimeTracker;
    [SerializeField] private RaceResultTime raceResultTime;

    private void Bind(MonoBehaviour mono)
    {
        if (mono is IDependency<RaceStateTracker>) (mono as IDependency<RaceStateTracker>).Construct(raceStatTracker);
        if (mono is IDependency<CarInputControl>) (mono as IDependency<CarInputControl>).Construct(carInputControl);
        if (mono is IDependency<TrackpointCircuit>) (mono as IDependency<TrackpointCircuit>).Construct(trackpointCircuit);
        if (mono is IDependency<Car>) (mono as IDependency<Car>).Construct(car);
        if (mono is IDependency<CarCameraController>) (mono as IDependency<CarCameraController>).Construct(carCameraController);
        if (mono is IDependency<RaceTimeTracker>) (mono as IDependency<RaceTimeTracker>).Construct(raceTimeTracker);
        if (mono is IDependency<RaceResultTime>) (mono as IDependency<RaceResultTime>).Construct(raceResultTime);
    }

    private void Awake()
    {
        MonoBehaviour[] monoInScene = FindObjectsOfType<MonoBehaviour>();

        for (int i = 0; i < monoInScene.Length; i++)
        {
            Bind(monoInScene[i]);            
        }
    }
}
