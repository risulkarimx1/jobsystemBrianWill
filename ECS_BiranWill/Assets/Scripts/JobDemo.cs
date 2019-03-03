using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using Random = System.Random;

public class JobDemo : MonoBehaviour
{
    struct RandomNumberMaker: IJobParallelFor
    {
        public NativeArray<float> _array;
        public void Execute(int index)
        {
            _array[index] =  new System.Random().Next()*10f; //Mathf.Sin(_array[index]);
        }
    }

    private NativeArray<float> inputArray;
    private JobHandle _jobHandle;

    private void Update()
    {
        inputArray = new NativeArray<float>(100,Allocator.TempJob);
        for (int i = 0; i < inputArray.Length; i++)
        {
            inputArray[i] = i;
        }
        
        RandomNumberMaker rnJob =new RandomNumberMaker()
        {
            _array =  inputArray
        };
        _jobHandle = rnJob.Schedule(inputArray.Length, 32);
        JobHandle.ScheduleBatchedJobs();
    }

    private void LateUpdate()
    {
        _jobHandle.Complete();
        Debug.Log($"value of 5th index {inputArray[5]}");
        inputArray.Dispose();
    }
}
