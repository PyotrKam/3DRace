using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideInfoCanvas : MonoBehaviour
{
    public GameObject infoCanvas; 

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            infoCanvas.SetActive(false); 
        }
    }
}
