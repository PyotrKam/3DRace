using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceKeyboardStarter : MonoBehaviour
{
    [SerializeField] private RaceStateTracker reaceStateTracker;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) == true)
        {
            reaceStateTracker.LaunchPreparationStart();
        }
    }
}
