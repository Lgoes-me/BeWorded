using TMPro;
using UnityEngine;

public class TextController : MonoBehaviour
{
    [field: SerializeField] private TextMeshProUGUI Text { get; set; }

    public void Init(string textKey, TextManager textManager)
    {
        Text.SetText(textManager.GetString(textKey));
    }
}