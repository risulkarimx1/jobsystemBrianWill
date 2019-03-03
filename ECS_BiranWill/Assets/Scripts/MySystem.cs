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

        Entity entity = em.CreateEntity();

        int index = entity.Index;
        int version = entity.Version;

        Debug.Log($"index {index} and version {version}");
        /////////
        
        em.AddComponent(entity,typeof(MyComponent));

        MyComponent myComponent = em.GetComponentData<MyComponent>(entity);

        float f = myComponent.Num; // getting default value 0
        
        em.SetComponentData<MyComponent>(entity,new MyComponent(){Num = 3.0f}); // setting to 3

        Debug.Log($"value now is: {em.GetComponentData<MyComponent>(entity).Num}");
        em.RemoveComponent<MyComponent>(entity);

    }

    protected override void OnDestroyManager()
    {
        Debug.Log($"Destroyed");
    }
}


