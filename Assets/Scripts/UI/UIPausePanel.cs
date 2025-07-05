using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPausePanel : MonoBehaviour, IDependency<Pauser>
{
    private Pauser pauser;

    public void Construct(Pauser obj) => pauser = obj;
    

    //13:52
}
