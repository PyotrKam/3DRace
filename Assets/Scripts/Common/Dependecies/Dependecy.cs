using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Dependecy : MonoBehaviour
{
    protected virtual void Bind(MonoBehaviour mono)
    {

    }

    protected void FindAllObjectToBlind()
    {
        MonoBehaviour[] monoInScene = FindObjectsOfType<MonoBehaviour>();

        for (int i = 0; i < monoInScene.Length; i++)
        {
            Bind(monoInScene[i]);
        }
    }
}
