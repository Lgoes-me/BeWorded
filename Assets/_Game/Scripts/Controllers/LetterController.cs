using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LetterController : MonoBehaviour, ICellData
{
    [field: SerializeField] public RectTransform Content { get; private set; }
    [field: SerializeField] public Canvas Canvas { get; private set; }
    [field: SerializeField] private Image Background { get; set; }
    [field: SerializeField] private TextMeshProUGUI Text { get; set; }

    private Func<Letter> CreateLetter { get; set; }
    public Letter Letter { get; private set; }
    private LetterState State { get; set; }

    public LetterController Init(Func<Letter> createLetter)
    {
        CreateLetter = createLetter;
        Letter = createLetter();
        State = LetterState.Neutral;

        Text.SetText(Letter.ToString());
        UpdateView();

        return this;
    }

    public void OnSelect()
    {
        State = State switch
        {
            LetterState.Clicked => LetterState.Neutral,
            _ => LetterState.Clicked
        };

        Canvas.overrideSorting = true;
        UpdateView();
    }

    public void OnDrag()
    {
        State = LetterState.Dragged;
        UpdateView();
    }

    public void OnError()
    {
        if (State is LetterState.Neutral)
            return;

        State = LetterState.Error;
        UpdateView();

        Invoke(nameof(ResetLetter), 0.1f);
    }

    public void ResetLetter()
    {
        State = LetterState.Neutral;
        Canvas.overrideSorting = false;
        UpdateView();
    }

    private void UpdateView()
    {
        Background.color = State switch
        {
            LetterState.Neutral => Color.grey,
            LetterState.Error => Color.red,
            LetterState.Clicked => Color.cyan,
            LetterState.Dragged => Color.blue,
            _ => Color.grey
        };
    }

    private enum LetterState
    {
        Neutral,
        Error,
        Clicked,
        Dragged
    }

    public void Deactivate()
    {
        Content.gameObject.SetActive(false);
    }

    public void ResetData()
    {
        Init(CreateLetter);
        Content.gameObject.SetActive(true);
    }

    public int GetSiblingIndex()
    {
        return transform.GetSiblingIndex();
    }

    public void SetSiblingIndex(int index)
    {
        transform.SetSiblingIndex(index);
    }
}