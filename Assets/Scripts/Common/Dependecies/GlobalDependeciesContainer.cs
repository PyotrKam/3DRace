using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalDependeciesContainer : Dependecy
{
    [SerializeField] private Pauser pauser;

    private static GlobalDependeciesContainer instance;

    //public static GlobalDependeciesContainer Instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    protected override void BindAll(MonoBehaviour monoBehaviourInScene)
    {
        //Debug.Log($"Chek BindAll: {monoBehaviourInScene.GetType().Name}");

        Bind<Pauser>(pauser, monoBehaviourInScene);
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        FindAllObjectToBlind();
    }
    
   

}
