using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public struct JobFinalPosition : IJobParallelFor
{
      public NativeArray<Vector3> positions;
      public NativeArray<Vector3> velocities;
      public NativeArray<Vector3> finalPositions;

    public void Execute(int index)
    {
        for (int i = 0; i <= finalPositions.Length * 2; i++)
        {
            if (i == index) continue;

            Vector3 velosity = velocities[index] + Vector3.forward;

            positions[index] = Vector3.forward;
            velocities[index] = velosity;
            finalPositions[index] = positions[index] + velocities[index];

            Debug.Log(finalPositions[index]);

            i++;          
        }
    }
}
