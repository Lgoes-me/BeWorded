using System.Threading.Tasks;
using UnityEngine;

public abstract class AlertController<T> : MonoBehaviour
{
    private TaskCompletionSource<T> CompletionSource { get; set; }
    
    public virtual async Task<T> Show()
    {
        CompletionSource = new TaskCompletionSource<T>();
        return await CompletionSource.Task;
    }

    protected void Close(T result)
    {
        CompletionSource.SetResult(result);
        Destroy(this.gameObject);
    }
}