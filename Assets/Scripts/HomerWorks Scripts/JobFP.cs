using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public class JobFP : MonoBehaviour
{
    void Start()
    {
        NativeArray<Vector3> positions = new NativeArray<Vector3>(10, Allocator.Persistent);
        NativeArray<Vector3> velocities = new NativeArray<Vector3>(10, Allocator.Persistent);
        NativeArray<Vector3> finalPositions = new NativeArray<Vector3>(10, Allocator.Persistent);

        JobFinalPosition job = new JobFinalPosition();
        job.positions = positions;
        job.velocities = velocities;
        job.finalPositions = finalPositions;

        JobHandle jobHandle = job.Schedule(1, 2);
        jobHandle.Complete();

        if(jobHandle.IsCompleted)
        {
            Debug.Log("Job is completed");
        }

        positions.Dispose();
        velocities.Dispose();
        finalPositions.Dispose();
    }
        
}
