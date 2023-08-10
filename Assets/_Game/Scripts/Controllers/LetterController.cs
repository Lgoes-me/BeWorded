using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LetterController : MonoBehaviour, ICellData
{
    [field: SerializeField] private Image Background { get; set; }
    [field: SerializeField] private TextMeshProUGUI Text { get; set; }

    private Func<string> CreateLetter { get; set; }
    public string Letter { get; private set; }
    private LetterState State { get; set; }

    public LetterController Init(Func<string> createLetter)
    {
        CreateLetter = createLetter;
        Letter = createLetter();
        State = LetterState.Neutral;

        Text.SetText(Letter);
        UpdateView();

        return this;
    }

    public void OnPointerClick()
    {
        State = State switch
        {
            LetterState.Clicked => LetterState.Neutral,
            _ => LetterState.Clicked
        };

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
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void ResetData()
    {
        Init(CreateLetter);
        transform.GetChild(0).gameObject.SetActive(true);
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