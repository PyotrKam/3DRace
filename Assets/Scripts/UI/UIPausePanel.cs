using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPausePanel : MonoBehaviour, IDependency<Pauser>
{

    [SerializeField] private GameObject panel;

    private Pauser pauser;

    public void Construct(Pauser obj)
    {
        Debug.Log("UIPausePanel: get Pauser in SceneDependenciesContainer");
        pauser = obj;
    }

    private void Start()
    {       
        panel.SetActive(false);
        pauser.PauseStateChange += OnPauseStatChanged;
       
    }
    
    
    private void OnDestroy()
    {
        pauser.PauseStateChange -= OnPauseStatChanged;
    }
    

    private void OnPauseStatChanged(bool isPause)
    {
        panel.SetActive(isPause);
    }

    private void Update()
    {
        if (pauser == null)
        {
            Debug.LogWarning("UIPausePanel: pauser is NULL!");
            return;
        }


        if (Input.GetKeyDown(KeyCode.Escape) == true)
        {
            pauser.ChangePauseState();            
        }
    }

    //-----------------------------------
    private void OnEnable()
    {
        Debug.Log("UIPausePanel: Enabled");
    }
    private void OnDisable()
    {
        Debug.Log("UIPausePanel: Disabled");
    }

}
