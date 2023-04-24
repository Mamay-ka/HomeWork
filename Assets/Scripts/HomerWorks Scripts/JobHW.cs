using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public class JobHW : MonoBehaviour
{
    
    private void Start()
    {
        NativeArray<int> array = new NativeArray<int>(10, Allocator.TempJob);
        JobHomeWork job = new JobHomeWork();
        job.array = array;

        JobHandle handle = job.Schedule();
        handle.Complete();

        if(handle.IsCompleted)
        {
            Debug.Log("Job is completed");
        }

        array.Dispose();
    }
}
