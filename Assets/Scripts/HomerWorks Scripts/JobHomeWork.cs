using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public struct JobHomeWork : IJob
{
    public NativeArray<int> array;
        
    public void Execute()
    {
        for(int i = 0; i< array.Length; i++)
        {
            int value = (i+1) * 2;  
            array[i] = value;

            if(value > 10)
            {
                array[i] = 0;
            }

            Debug.Log(array[i]);
        }
    }
}

