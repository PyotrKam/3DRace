using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceResultCanvas : MonoBehaviour, IDependency<RaceStateTracker>, IDependency<RaceResultTime>, IDependency<RaceTimeTracker>
{
    [SerializeField] private GameObject resultCanvas;
    [SerializeField] private Text finalTimeText;
    [SerializeField] private Text recordText;    

    private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker obj) => raceStateTracker = obj;

    private RaceResultTime raceResultTime;
    public void Construct(RaceResultTime obj) => raceResultTime = obj;

    private RaceTimeTracker raceTimeTracker;
    public void Construct(RaceTimeTracker obj) => raceTimeTracker = obj;



    private void Start()
    {
        resultCanvas.SetActive(false);
        raceStateTracker.Completed += OnRaceCompleted; 
    }

    private void OnDestroy()
    {
        raceStateTracker.Completed -= OnRaceCompleted;
    }

    private void OnRaceCompleted()
    {

        resultCanvas.SetActive(true);

        
        finalTimeText.text = $"Your Time: {StringTime.SecondToTimeString(raceTimeTracker.CurrentTime)}";

        if (raceResultTime.RecordWasSet)
        {
            recordText.text = $"Record: {StringTime.SecondToTimeString(raceResultTime.PlayerRecordTime)}";
        }
        else
        {
            recordText.text = "New record";
        }

        
    }
}
