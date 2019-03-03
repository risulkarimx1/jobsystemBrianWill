using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;


public class MySystem :ComponentSystem
{
    protected override void OnCreateManager()
    {
        Debug.Log($"Created");
    }

    protected override void OnUpdate()
    {
        Debug.Log($"hi from update");
    }

    protected override void OnDestroyManager()
    {
        Debug.Log($"Destroyed");
    }
}


public class MySystem2 :ComponentSystem
{
    protected override void OnCreateManager()
    {
        Debug.Log($"Created2");
    }

    protected override void OnUpdate()
    {
        Debug.Log($"hi from update2");
    }

    protected override void OnDestroyManager()
    {
        Debug.Log($"Destroyed2");
    }
}
