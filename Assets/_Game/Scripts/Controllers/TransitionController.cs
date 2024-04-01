using System.Threading.Tasks;
using UnityEngine;

public class TransitionController : MonoBehaviour
{
    [field: SerializeField]
    private Animation Animation { get; set; }
    private TaskCompletionSource<bool> CompletionSource { get; set; }
    
    public Task<bool> Show()
    {
        gameObject.SetActive(true);
        Animation.Play();
        
        CompletionSource = new TaskCompletionSource<bool>();
        return CompletionSource.Task;
    }

    public void Close()
    {
        CompletionSource.SetResult(true);
        gameObject.SetActive(false);
    }
}