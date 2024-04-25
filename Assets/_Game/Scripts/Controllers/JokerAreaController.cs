using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class JokerAreaController : MonoBehaviour
{
    [field: SerializeField] private RectTransform JokersContent { get; set; }
    [field: SerializeField] private JokerCardController JokerCardControllerPrefab { get; set; }

    private List<JokerCardController> JokerCardControllers { get; set; }

    public JokerAreaController Init(Player player)
    {
        JokerCardControllers = new List<JokerCardController>();

        foreach (var joker in player.Jokers.OrderBy(j => j.Index))
        {
            var card = Instantiate(JokerCardControllerPrefab, JokersContent).Init(joker, this);
            JokerCardControllers.Add(card);
        }

        return this;
    }

    public void ResetArea()
    {
        JokerCardControllers = JokerCardControllers.OrderBy(c => c.transform.position.x).ToList();

        for (var index = 0; index < JokerCardControllers.Count; index++)
        {
            var card = JokerCardControllers[index];
            card.transform.SetSiblingIndex(index);
            card.Joker.SetIndex(index);
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(JokersContent);
    }
}