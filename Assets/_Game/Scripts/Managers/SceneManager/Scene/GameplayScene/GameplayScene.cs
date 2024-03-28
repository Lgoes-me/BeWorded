using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class GameplayScene : BaseScene<GameplaySceneData>
{
    [field: SerializeField] private GameAreaController GameAreaController { get; set; }
    [field: SerializeField] private LetterController LetterControllerPrefab { get; set; }

    [field: SerializeField] private Transform JokersContent { get; set; }
    [field: SerializeField] private JokerCardController JokerCardControllerPrefab { get; set; }

    [field: SerializeField] private TextMeshProUGUI Tentativas { get; set; }
    [field: SerializeField] private TextMeshProUGUI Response { get; set; }
    [field: SerializeField] private TextMeshProUGUI ResponseScore { get; set; }
    [field: SerializeField] private TextMeshProUGUI ScoreToBeatText { get; set; }
    [field: SerializeField] private TextMeshProUGUI ScoreText { get; set; }

    [field: SerializeField] public PowerUpController SwapButton { get; private set; }
    [field: SerializeField] public PowerUpController BombButton { get; private  set; }
    [field: SerializeField] public PowerUpController ShuffleButton { get; private set; }

    public Grid<LetterController> LettersGrid { get; private set; }
    public GameState State { get; private set; }
    private Level Level { get; set; }

    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        var gameConfig = Application.ConfigManager.GameConfig;
        var player = SceneData.Player;

        Application.SaveManager.SaveData(player);
        Level = Application.ConfigManager.GetNextLevelConfig(player);

        ScoreToBeatText.SetText(Level.Score.ToString());
        Tentativas.SetText(Level.Tentativas.ToString());

        LettersGrid = new Grid<LetterController>(gameConfig.Height, gameConfig.Width, CreateLetterController);
        GameAreaController.Init(this, gameConfig.Height, gameConfig.Width);

        SwapButton.Init(player.Swaps, () => State.OnPowerUpClicked(player.Swaps));
        BombButton.Init(player.Bombs, () => State.OnPowerUpClicked(player.Bombs));
        ShuffleButton.Init(player.Shuffles, () => State.OnPowerUpClicked(player.Shuffles));

        foreach (var joker in player.Jokers)
        {
            Instantiate(JokerCardControllerPrefab, JokersContent).Init(joker);
        }

        State = new GameplayState(this);
    }

    private LetterController CreateLetterController()
    {
        return Instantiate(LetterControllerPrefab, GameAreaController.transform)
            .Init(Application.ContentManager.GetRandomLetter);
    }

    public void ChangeState(GameState state)
    {
        State.OnStateExit();
        State = state;
        State.OnStateEnter();
    }

    public void ClearResponseText()
    {
        Response.SetText(string.Empty);
        ResponseScore.SetText(string.Empty);
    }

    public void SetResponse(List<LetterController> letterControllers)
    {
        var letters = letterControllers.Select(l => l.Letter).ToList();
        var response = string.Join("", letters);
        Response.SetText(response);

        var soma = 0;

        foreach (var prize in letters.Select(l => l.Prize))
        {
            if (prize is not ScorePrize scorePrize)
                continue;

            soma += scorePrize.Score;
        }

        ResponseScore.SetText($"{soma}x{letters.Count}");
    }

    public bool CheckResponse(List<LetterController> letterControllers)
    {
        var letters = letterControllers.Select(l => l.Letter);
        var response = string.Join("", letters);

        if (Application.ContentManager.IsValidWord(response))
        {
            Level.UsarTentativa();
            Tentativas.SetText(Level.Tentativas.ToString());
            return true;
        }

        return false;
    }

    public async Task ClearSelection(List<LetterController> letterControllers)
    {
        ChangeState(new AnimatingState());
        LettersGrid.ClearCells(letterControllers);
        await LettersGrid.SortEmpty();
        LettersGrid.FillNewData();
    }

    public void CheckIfGameEnded()
    {
        if (Level.CurrentScore >= Level.Score)
        {
            if (Level.IsFinalLevel)
            {
                ChangeState(new GameOverState(true, SceneData.Player, Application));
            }
            else
            {
                ChangeState(new CompletedLevelState(SceneData.Player, Level, Application));
            }
        }
        else if (Level.Tentativas == 0)
        {
            ChangeState(new GameOverState(false, SceneData.Player, Application));
        }
        else
        {
            ChangeState(new GameplayState(this));
        }
    }

    public void GetPrizes(List<LetterController> letterControllers)
    {
        var basePrize = 0;
        var baseMultiplier = letterControllers.Count;

        foreach (var letterController in letterControllers)
        {
            basePrize += GetScore(letterController.Letter.Prize, letterController);
            SceneData.Player.OnLetterScored(ref basePrize, ref baseMultiplier, letterController.Letter);
        }

        var word = string.Join("", letterControllers.Select(l => l.Letter));
        SceneData.Player.OnWordScored(ref basePrize, ref baseMultiplier, word);

        SwapButton.UpdateButton();
        BombButton.UpdateButton();
        ShuffleButton.UpdateButton();

        Level.GiveScore(basePrize * baseMultiplier);
        ScoreText.SetText(Level.CurrentScore.ToString());
    }

    private int GetScore(IPrize prize, LetterController letterController)
    {
        if (prize is ScorePrize scorePrize)
        {
            return scorePrize.Score;
        }

        if (prize is PowerUpPrize powerUpPrize)
        {
            switch (powerUpPrize.PowerUp)
            {
                case PowerUpType.Troca:
                    SceneData.Player.Swaps.Gain();
                    break;
                case PowerUpType.Bomba:
                    SceneData.Player.Bombs.Gain();
                    break;
                case PowerUpType.Misturar:
                    SceneData.Player.Shuffles.Gain();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        return 0;
    }
}