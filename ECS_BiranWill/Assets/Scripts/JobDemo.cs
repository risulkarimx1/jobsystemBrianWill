using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public class JobDemo : MonoBehaviour
{
   struct TestJob : IJob
   {
      public int x;
      public int y;

      public NativeArray<int> array;
      
      public void Execute()
      {
         array[0] = x + y;
         Debug.Log($"Hi I am from job: {array[0]}"); // smaggle out via native container
      }
   }

   private NativeArray<int> array;
   private JobHandle jobHandle;
   private JobHandle jobHandle2;

   private void Update()
   {
      array = new NativeArray<int>(1,Allocator.TempJob);
      var tj = new TestJob()
      {
         x = 3,
         y = 5,
         array = array// deletes them from memory if they live longer than 4 frames
         
      };
      var tj2 = new TestJob()
      {
         x = 11,
         y = 12,
         array = array// deletes them from memory if they live longer than 4 frames
         
      };
      jobHandle = tj.Schedule();
      jobHandle2 = tj2.Schedule();
      JobHandle.ScheduleBatchedJobs();

//      JobHandle combinedJobholder = JobHandle.CombineDependencies(jobHandle, jobHandle2);
//      combinedJobholder.Complete();


   }

   private void LateUpdate()
   {
      //jobHandle.Complete();
//      jobHandle2.Complete();
      array.Dispose();
   }
}
