using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LetterController : MonoBehaviour, ICellData
{
    [field: SerializeField] public RectTransform Content { get; private set; }
    [field: SerializeField] private Canvas Canvas { get; set; }
    [field: SerializeField] private Image Background { get; set; }
    [field: SerializeField] private TextMeshProUGUI Text { get; set; }
    [field: SerializeField] private TextMeshProUGUI PrizeText { get; set; }

    [field: SerializeField] private Color DefaultColor { get; set; }
    [field: SerializeField] private Color HighlightedColor { get; set; }
    [field: SerializeField] private Color SelectedColor { get; set; }
    [field: SerializeField] private Color ErrorColor { get; set; }
    private Func<Letter> CreateLetter { get; set; }
    public Letter Letter { get; private set; }
    private LetterState State { get; set; }

    public LetterController Init(Func<Letter> createLetter)
    {
        CreateLetter = createLetter;
        Letter = createLetter();
        State = LetterState.Neutral;

        Text.SetText(Letter.ToString());
        PrizeText.SetText(Letter.Prize.ToString());
        UpdateView();

        return this;
    }

    public void OnSelect(bool overrideSorting = true)
    {
        State = State switch
        {
            LetterState.Clicked => LetterState.Neutral,
            _ => LetterState.Clicked
        };

        Canvas.overrideSorting = overrideSorting;
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

        Invoke(nameof(ResetLetter), 0.15f);
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
            LetterState.Neutral => DefaultColor,
            LetterState.Error => ErrorColor,
            LetterState.Clicked => SelectedColor,
            LetterState.Dragged => HighlightedColor,
            _ => Color.white
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

    public void AnimateFall()
    {
        Content.anchoredPosition = new Vector3(0, 45);
        Content.DOMove(transform.position, 0.2f);
    }
}