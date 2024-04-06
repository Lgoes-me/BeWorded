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
    private bool Tutorial { get; set; }
    
    private async void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        var gameConfig = Application.ConfigManager.GameConfig;

        Tutorial = Application.OnboardManager.CanShow("FirstMatch");
        
        Application.SaveManager.SaveData(SceneData.Player);
        Level = Application.ConfigManager.GetNextLevelConfig(SceneData.Player);

        ScoreToBeatText.SetText(Level.Score.ToString());
        Tentativas.SetText(Level.Tentativas.ToString());

        LettersGrid = new Grid<LetterController>(gameConfig.Height, gameConfig.Width, CreateLetterController);
        GameAreaController.Init(this, gameConfig.Height, gameConfig.Width);

        SwapButton.Init(SceneData.Player.Swaps, () => State.OnPowerUpClicked(SceneData.Player.Swaps));
        BombButton.Init(SceneData.Player.Bombs, () => State.OnPowerUpClicked(SceneData.Player.Bombs));
        ShuffleButton.Init(SceneData.Player.Shuffles, () => State.OnPowerUpClicked(SceneData.Player.Shuffles));

        foreach (var joker in SceneData.Player.Jokers)
        {
            Instantiate(JokerCardControllerPrefab, JokersContent).Init(joker);
        }

        State = new GameplayState(this);

        //await Task.Delay(1000);
        
        //if(!Tutorial)
            //return;

        /*var toHighlight = new List<RectTransform>()
        {
            LettersGrid.Get(2,1).transform as RectTransform,
            LettersGrid.Get(2,2).transform as RectTransform,
            LettersGrid.Get(2,3).transform as RectTransform,
            LettersGrid.Get(2,4).transform as RectTransform,
        };
        
        await Application.AlertManager.ShowTooltip("aaaaa", toHighlight);
        
        foreach (var letter in LettersGrid)
        { 
            var test = new List<RectTransform>()
            {
                letter.transform as RectTransform
            };
            
            await Application.AlertManager.ShowTooltip("aaaaa", test, false, 1);
        }*/
    }

    private LetterController CreateLetterController()
    {
        return Instantiate(LetterControllerPrefab, GameAreaController.transform)
            .Init(() => Application.ContentManager.GetRandomLetter(Tutorial));
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

    public void GetPrizes(List<LetterController> letterControllers)
    {
        var basePrize = 0;
        var baseMultiplier = letterControllers.Count;

        foreach (var letterController in letterControllers)
        {
            switch (letterController.Letter.Prize)
            {
                case ScorePrize scorePrize:
                    basePrize += scorePrize.Score;
                    break;
                case PowerUpPrize powerUpPrize:
                    SceneData.Player.GainPowerUp(powerUpPrize.PowerUp);
                    break;
            }
            
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
    
    public async Task ClearSelection(List<LetterController> letterControllers)
    {
        ChangeState(new AnimatingState());
        LettersGrid.ClearCells(letterControllers);
        await LettersGrid.SortEmpty();
        await LettersGrid.FillNewData();
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
}