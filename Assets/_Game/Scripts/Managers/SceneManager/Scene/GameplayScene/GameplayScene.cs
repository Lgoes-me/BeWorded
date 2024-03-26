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

    [field: SerializeField] private TextMeshProUGUI Tentativas { get; set; }
    [field: SerializeField] private TextMeshProUGUI Response { get; set; }
    [field: SerializeField] private TextMeshProUGUI ResponseScore { get; set; }
    [field: SerializeField] private TextMeshProUGUI ScoreToBeatText { get; set; }
    [field: SerializeField] private TextMeshProUGUI ScoreText { get; set; }

    [field: SerializeField] private PowerUpController SwapButton { get; set; }
    [field: SerializeField] private PowerUpController BombButton { get; set; }
    [field: SerializeField] private PowerUpController ShuffleButton { get; set; }

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

        SwapButton.Init(player.Swaps, () => { ChangeState(new SwapDragState(this)); });
        BombButton.Init(player.Bombs, () => { ChangeState(new BombState(this)); });
        ShuffleButton.Init(player.Shuffles, () => { ChangeState(new ShuffleState(this)); });

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
        var letters = letterControllers.Select(l => l.Letter).ToList();
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

    public async Task GetPrizes(List<LetterController> letterControllers)
    {
        var basePrize = 0;
        var baseMultiplier = letterControllers.Count;
        
        foreach (var letterController in letterControllers)
        {
            basePrize += await AnimatePrize(letterController.Letter.Prize, letterController);
            SceneData.Player.OnLetterPrizeCredited(ref basePrize, ref baseMultiplier, letterController.Letter);
        }
        
        Level.GiveScore(basePrize * baseMultiplier);
        ScoreText.SetText(Level.CurrentScore.ToString());
    }

    private async Task<int> AnimatePrize(IPrize prize, LetterController letterController)
    {
        if (prize is ScorePrize scorePrize)
        {
            await letterController.AnimatePrize(ScoreText.transform.position);
            return scorePrize.Score;
        }

        if (prize is PowerUpPrize powerUpPrize)
        {
            switch (powerUpPrize.PowerUp)
            {
                case PowerUpType.Troca:
                    await letterController.AnimatePrize(SwapButton.transform.position);
                    SwapButton.GivePowerUpUse();
                    break;
                case PowerUpType.Bomba:
                    await letterController.AnimatePrize(BombButton.transform.position);
                    BombButton.GivePowerUpUse();
                    break;
                case PowerUpType.Misturar:
                    await letterController.AnimatePrize(ShuffleButton.transform.position);
                    ShuffleButton.GivePowerUpUse();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        return 0;
    }
}