using UnityEngine;
using UnityEngine.UI;

public class LoadingController : MonoBehaviour
{
    [field: SerializeField] private Image Image { get; set; }

    public void Show(float value)
    {
        Image.fillAmount = value;
    }
    
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}