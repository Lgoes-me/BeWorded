﻿using System.Threading.Tasks;
using UnityEngine;

public abstract class AlertController<T> : MonoBehaviour
{
    private TaskCompletionSource<T> CompletionSource { get; set; }
    
    protected Task<T> InternalShow()
    {
        CompletionSource = new TaskCompletionSource<T>();
        return CompletionSource.Task;
    }

    protected void Close(T result)
    {
        CompletionSource.SetResult(result);
        Destroy(this.gameObject);
    }
}