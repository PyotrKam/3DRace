using System;
using UnityEngine;
using UnityEngine.Events;


public enum RaceState
{
    Preparation,
    CountDown,
    Race,
    Passed
}

public class RaceStateTracker : MonoBehaviour, IDependency<TrackpointCircuit>
{
    public event UnityAction PreparationStarted;
    public event UnityAction Started;
    public event UnityAction Completed;
    public event UnityAction<TrackPoint> TrackPointPassed;
    public event UnityAction<int> LapCompleted;

    private TrackpointCircuit trackPointCircuit;
    [SerializeField] private Timer countdownTimer;
    [SerializeField] private int lapsToComplete;

    public Timer CountDownTimer => countdownTimer;


    private RaceState state;
    public RaceState State => state;

    private void StartState(RaceState state)
    {
        this.state = state;
    }

    public void Construct(TrackpointCircuit trackpointCircuit)
    {
        this.trackPointCircuit = trackpointCircuit; 
    }

    private void Start()
    {
        StartState(RaceState.Preparation);

        countdownTimer.enabled = false;
        countdownTimer.Finished += OnCountdownnTimerFinished;

        trackPointCircuit.TrackPointTriggered += OnTrackPointTriggered;
        trackPointCircuit.LapCompleted += OnLapCompleted;
    }

    

    private void OnDestroy()
    {
        countdownTimer.Finished -= OnCountdownnTimerFinished;

        trackPointCircuit.TrackPointTriggered -= OnTrackPointTriggered;
        trackPointCircuit.LapCompleted -= OnLapCompleted;
    }

    private void OnTrackPointTriggered(TrackPoint trackPoint)
    {
        TrackPointPassed?.Invoke(trackPoint);
    }

    private void OnCountdownnTimerFinished()
    {
        StartRace();
    }

    

    private void OnLapCompleted(int lapAmount)
    {
        if (trackPointCircuit.Type == TrackType.Sprint)
        {
            CompleteRace();
        }

        if (trackPointCircuit.Type == TrackType.Circular)
        {
            if (lapAmount == lapsToComplete)
            {
                CompleteRace();
            }
            else
                CompleteLap(lapAmount);
        }
    }

    public void LaunchPreparationStart()
    {
        if (state != RaceState.Preparation) return;
        StartState(RaceState.CountDown);

        countdownTimer.enabled = true;

        PreparationStarted?.Invoke();
    }

    private void StartRace()
    {
        if (state != RaceState.CountDown) return;
        StartState(RaceState.Race);

        Started?.Invoke();
    }
    private void CompleteRace()
    {
        if (state != RaceState.Race) return;
        StartState(RaceState.Passed);

        Completed?.Invoke();
    }
    private void CompleteLap(int lapAmount)
    {
        LapCompleted?.Invoke(lapAmount);
    }
}
