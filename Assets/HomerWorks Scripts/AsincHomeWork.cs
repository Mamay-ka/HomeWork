using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class AsincHomeWork : MonoBehaviour
{
    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
    
    void Start()
    {
        CancellationToken cancellationToken = cancellationTokenSource.Token;

        AllTasksAsync(cancellationToken);

        cancellationTokenSource.Cancel();
        cancellationTokenSource.Dispose();
    }

    async void AllTasksAsync(CancellationToken cancellationToken)
    {
        await Task.WhenAll(Task1Async(cancellationToken), Task2Async(cancellationToken));
        Debug.Log("All tasks completed");
    }
    async Task Task1Async(CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            Debug.Log("Operation has canceled by Token"); 
            return;
        }
        await Task.Delay(1000);
        Debug.Log("Task1 completed");
    }

    async Task Task2Async(CancellationToken cancellationToken)
    {
        for (int i = 60; i <= 0; i--)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                Debug.Log("Operation has canceled by Token");
                return;
            }

            i--;
            await Task.Yield();
        }
        Debug.Log("Task2 completed");
    }
}
