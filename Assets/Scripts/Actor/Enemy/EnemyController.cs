using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IPool
{
    public bool Waiting { get; set; }
    public void SetUp(Transform parent)
    {
        
    }

    public void IsUseSetUp()
    {
        
    }

    public bool Execute()
    {
        return true;
    }

    public void Delete()
    {
        
    }
}