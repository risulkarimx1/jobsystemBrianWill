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
        World world = World.Active;
        EntityManager em = world.GetOrCreateManager<EntityManager>();

        Entity entity = em.CreateEntity();

        int index = entity.Index;
        int version = entity.Version;

        Debug.Log($"index {index} and version {version}");
    }

    protected override void OnDestroyManager()
    {
        Debug.Log($"Destroyed");
    }
}


