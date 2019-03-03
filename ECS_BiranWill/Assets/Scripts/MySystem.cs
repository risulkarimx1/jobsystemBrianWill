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

public struct SomeComponent : IComponentData
{
    public int Num;
}

[UpdateBefore(typeof(MyBarrier))]
public class MySystem :ComponentSystem
{
    private ComponentGroup _group;
    protected override void OnCreateManager()
    {
        Debug.Log($"Created");
        
        
        World world = World.Active;
        EntityManager em = world.GetOrCreateManager<EntityManager>();

        EntityArchetype archetype1 = EntityManager.CreateArchetype(typeof(MyComponent), typeof(OtherComponent));
        EntityArchetype archetype2 = EntityManager.CreateArchetype(typeof(MyComponent), typeof(OtherComponent),typeof(SomeComponent));


        for (int i = 0; i < 10; i++)
        {
            em.CreateEntity(archetype1);
        }

        for (int i = 0; i < 20; i++)
        {
            em.CreateEntity(archetype2);
        }

        _group = GetComponentGroup(typeof(MyComponent), typeof(OtherComponent));

    }

    [Inject] private MyBarrier _myBarrier;
    protected override void OnUpdate()
    {
        ComponentDataArray<MyComponent> myComponents = _group.GetComponentDataArray<MyComponent>();
        ComponentDataArray<OtherComponent> otherComponents = _group.GetComponentDataArray<OtherComponent>();

        EntityArray entities = _group.GetEntityArray();

        EntityCommandBuffer cmds = _myBarrier.CreateCommandBuffer();
        
        for (int i = 0; i < myComponents.Length; i++)
        {
            // we can read component data
            Entity entity = entities[i];
            MyComponent myComponent = myComponents[i];
            OtherComponent otherComponent = otherComponents[i];
           
            cmds.DestroyEntity(entity);
           
        }
    }

    protected override void OnDestroyManager()
    {
        Debug.Log($"Destroyed");
    }
}

public class MyBarrier : BarrierSystem
{
}


