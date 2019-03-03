using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;


public struct MyComponent : IComponentData
{
    public float Num;
}

public struct OtherComponent : IComponentData
{
    public int Num;
}

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

        EntityArchetype archetype = EntityManager.CreateArchetype(typeof(MyComponent), typeof(OtherComponent));

        Entity entity = em.CreateEntity(archetype);

        int index = entity.Index;
        int version = entity.Version;

        Debug.Log($"index {index} and version {version}");

    }

    protected override void OnDestroyManager()
    {
        Debug.Log($"Destroyed");
    }
}


