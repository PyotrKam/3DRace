using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneDependenciesContainer : Dependecy
{
    [SerializeField] private RaceStateTracker raceStatTracker;
    [SerializeField] private CarInputControl carInputControl;
    [SerializeField] private TrackpointCircuit trackpointCircuit;
    [SerializeField] private Car car;
    [SerializeField] private CarCameraController carCameraController;
    [SerializeField] private RaceTimeTracker raceTimeTracker;
    [SerializeField] private RaceResultTime raceResultTime;
    //SerializeField] private Pauser pauser;

    protected override void BindAll(MonoBehaviour monoBehaviourInScene)
    {
        Bind<RaceStateTracker>(raceStatTracker, monoBehaviourInScene);
        //Bind<RaceStateTracker>(raceStatTracker, monoBehaviourInScene);
        Bind<CarInputControl>(carInputControl, monoBehaviourInScene);
        Bind<TrackpointCircuit>(trackpointCircuit, monoBehaviourInScene);
        Bind<Car>(car, monoBehaviourInScene);
        Bind<CarCameraController>(carCameraController, monoBehaviourInScene);
        Bind<RaceTimeTracker>(raceTimeTracker, monoBehaviourInScene);
        Bind<RaceResultTime>(raceResultTime, monoBehaviourInScene);
        //Bind<Pauser>(pauser, monoBehaviourInScene);
    }

    private void Awake()
    {
        FindAllObjectToBlind();
    }
}
