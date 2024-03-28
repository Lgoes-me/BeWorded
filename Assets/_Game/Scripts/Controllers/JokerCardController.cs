using TMPro;
using UnityEngine;

public class JokerCardController : MonoBehaviour
{
    [field: SerializeField] private TextMeshProUGUI JokerName { get; set; }

    public JokerCardController Init(BaseJoker joker)
    {
        JokerName.SetText(joker.Id);
        return this;
    }
}