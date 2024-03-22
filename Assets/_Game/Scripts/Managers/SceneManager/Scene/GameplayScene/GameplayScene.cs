using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayScene : BaseScene<GameplaySceneData>
{
    [field: SerializeField] private GameAreaController GameAreaController { get; set; }
    [field: SerializeField] private LetterController LetterControllerPrefab { get; set; }
    [field: SerializeField] public TextMeshProUGUI Response { get; private set; }
    [field: SerializeField] private TextMeshProUGUI ScoreText { get; set; }

    [field: SerializeField] private PowerUpController SwapButton { get; set; }
    [field: SerializeField] private PowerUpController BombButton { get; set; }
    [field: SerializeField] private PowerUpController ShuffleButton { get; set; }
    [field: SerializeField] public Button ResetButton { get; private set; }

    public Grid<LetterController> LettersGrid { get; private set; }
    public GameState State { get; private set; }
    private int Score { get; set; }
    
    private LevelConfig LevelConfig { get; set; }
    private Player Player { get; set; }

    private void Start()
    {
        LevelConfig = new LevelConfig();
        Player = new Player();

        LettersGrid = new Grid<LetterController>(SceneData.GameConfig.Height, SceneData.GameConfig.Width, CreateLetterController);
        GameAreaController.Init(this, SceneData.GameConfig.Height, SceneData.GameConfig.Width);

        SwapButton.Init(Player.Swaps, () => { ChangeState(new SwapDragState(this)); });
        BombButton.Init(Player.Bombs, () => { ChangeState(new BombState(this)); });
        ShuffleButton.Init(Player.Shuffles, () => { ChangeState(new ShuffleState(this)); });

        ResetButton.onClick.AddListener(ResetGame);
        State = new GameplayState(this);
    }

    public void ResetGame()
    {
        SceneManager.ChangeMainScene(SceneData);
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
                yield return GetPrizes(letterController, letterControllers.Count);
            }
        }

        LettersGrid.ClearCells(letterControllers);

        yield return LettersGrid.SortEmpty();

        LettersGrid.FillNewData();

        if (Score >= LevelConfig.Score)
        {
            yield return new WaitForSeconds(1f);
            ChangeState(new CompletedLevelState(this));  
        }
        else
        {
            ChangeState(new GameplayState(this));
        }
    }

    private IEnumerator GetPrizes(LetterController letterController, int multiplier)
    {
        var prize = letterController.Letter.Prize;

        if (prize is ScorePrize scorePrize)
        {
            yield return letterController.AnimatePrize(ScoreText.transform);
            Score += scorePrize.Score * multiplier;
            ScoreText.SetText(Score.ToString());
        }
        else if (prize is PowerUpPrize powerUpPrize)
        {
            switch (powerUpPrize.PowerUp)
            {
                case PowerUp.Troca:
                    yield return letterController.AnimatePrize(SwapButton.transform);
                    SwapButton.GivePowerUpUse();
                    break;
                case PowerUp.Bomba:
                    yield return letterController.AnimatePrize(BombButton.transform);
                    BombButton.GivePowerUpUse();
                    break;
                case PowerUp.Misturar:
                    yield return letterController.AnimatePrize(ShuffleButton.transform);
                    ShuffleButton.GivePowerUpUse();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}