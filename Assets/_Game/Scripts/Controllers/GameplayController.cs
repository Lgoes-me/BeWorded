using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    [field: SerializeField] private int Height { get; set; }
    [field: SerializeField] private int Width { get; set; }
    [field: SerializeField] private int Swaps { get; set; }
    [field: SerializeField] private int Bombs { get; set; }
    [field: SerializeField] private int Hints { get; set; }
    [field: SerializeField] private int Shuffles { get; set; }

    [field: SerializeField] public ContentManager ContentManager { get; private set; }
    [field: SerializeField] private GameAreaController GameAreaController { get; set; }
    [field: SerializeField] private LetterController LetterControllerPrefab { get; set; }
    [field: SerializeField] public TextMeshProUGUI Response { get; private set; }
    [field: SerializeField] private TextMeshProUGUI ScoreText { get; set; }

    [field: SerializeField] private PowerUpController SwapButton { get; set; }
    [field: SerializeField] private PowerUpController BombButton { get; set; }
    [field: SerializeField] private PowerUpController HintButton { get; set; }
    [field: SerializeField] private PowerUpController ShuffleButton { get; set; }

    public Grid<LetterController> LettersGrid { get; private set; }
    public GameState State { get; private set; }
    private int Score { get; set; }

    private void Awake()
    {
        LettersGrid = new Grid<LetterController>(Height, Width, CreateLetterController);
        
        GameAreaController.Init(this, Height, Width);

        SwapButton.Init(Swaps, () => { ChangeState(new SwapDragState(this)); });
        BombButton.Init(Bombs, () => { ChangeState(new BombState(this)); });
        HintButton.Init(Hints, () => { ChangeState(new HintState(this)); });
        ShuffleButton.Init(Shuffles, () => { ChangeState(new ShuffleState(this)); });

        State = new GameplayState(this);
    }

    private LetterController CreateLetterController()
    {
        return Instantiate(LetterControllerPrefab, GameAreaController.transform).Init(ContentManager.GetRandomLetter);
    }

    public void ChangeState(GameState state)
    {
        State.OnStateExit();
        State = state;
        State.OnStateEnter();
    }

    public void SetResponse(List<LetterController> letterControllers)
    {
        var response = string.Join("", letterControllers.Select(s => s.Letter));
        Response.SetText(response);
    }

    public bool CheckResponse(List<LetterController> letterControllers)
    {
        var response = string.Join("", letterControllers.Select(s => s.Letter));

        if (ContentManager.IsValidWord(response))
        {
            ClearSelection(letterControllers);
            return true;
        }

        return false;
    }

    public void ClearSelection(List<LetterController> letterControllers, bool getPrizes = true)
    {
        StartCoroutine(ClearSelectionCoroutine(letterControllers, getPrizes));
    }

    private IEnumerator ClearSelectionCoroutine(List<LetterController> letterControllers, bool getPrizes)
    {
        ChangeState(new AnimatingState());

        yield return new WaitForSeconds(0.15f);

        if (getPrizes)
        {
            foreach (var letterController in letterControllers)
            {
                yield return GetPrizes(letterController);
            }
        }

        LettersGrid.ClearCells(letterControllers);

        yield return LettersGrid.SortEmpty();

        LettersGrid.FillNewData();

        ChangeState(new GameplayState(this));
    }

    private IEnumerator GetPrizes(LetterController letterController)
    {
        var prize = letterController.Letter.Prize;

        if (prize is ScorePrize scorePrize)
        {
            yield return letterController.AnimatePrize(ScoreText.transform);
            Score += scorePrize.Score;
            ScoreText.SetText(Score.ToString());
        }
        else if (prize is PowerUpPrize powerUpPrize)
        {
            switch (powerUpPrize.PowerUp)
            {
                case PowerUp.Swap:
                    yield return letterController.AnimatePrize(SwapButton.transform);
                    SwapButton.GivePowerUpUse();
                    break;
                case PowerUp.Bomb:
                    yield return letterController.AnimatePrize(BombButton.transform);
                    BombButton.GivePowerUpUse();
                    break;
                case PowerUp.Hint:
                    yield return letterController.AnimatePrize(HintButton.transform);
                    HintButton.GivePowerUpUse();
                    break;
                case PowerUp.Shuffle:
                    yield return letterController.AnimatePrize(ShuffleButton.transform);
                    ShuffleButton.GivePowerUpUse();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}