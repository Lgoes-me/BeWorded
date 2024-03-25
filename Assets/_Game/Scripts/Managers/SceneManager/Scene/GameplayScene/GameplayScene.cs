﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameplayScene : BaseScene<GameplaySceneData>
{
    [field: SerializeField] private GameAreaController GameAreaController { get; set; }
    [field: SerializeField] private LetterController LetterControllerPrefab { get; set; }

    [field: SerializeField] public TextMeshProUGUI Tentativas { get; private set; }
    [field: SerializeField] public TextMeshProUGUI Response { get; private set; }
    [field: SerializeField] public TextMeshProUGUI ResponseScore { get; private set; }
    [field: SerializeField] private TextMeshProUGUI ScoreToBeatText { get; set; }
    [field: SerializeField] public TextMeshProUGUI ScoreText { get; set; }

    [field: SerializeField] private PowerUpController SwapButton { get; set; }
    [field: SerializeField] private PowerUpController BombButton { get; set; }
    [field: SerializeField] public PowerUpController ShuffleButton { get; set; }

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

    public void SetResponse(List<LetterController> letterControllers)
    {
        var response = string.Join("", letterControllers.Select(s => s.Letter));
        Response.SetText(response);
        
        var soma = 0;

        foreach (var prize in letterControllers.Select(s => s.Letter.Prize))
        {
            if (prize is not ScorePrize scorePrize)
                continue;

            soma += scorePrize.Score;
        }
        
        ResponseScore.SetText($"{soma}x{letterControllers.Count}");
    }

    public bool CheckResponse(List<LetterController> letterControllers)
    {
        var response = string.Join("", letterControllers.Select(s => s.Letter));

        if (Application.ContentManager.IsValidWord(response))
        {
            Level.UsarTentativa();
            Tentativas.SetText(Level.Tentativas.ToString());
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

        if (Level.CurrentScore >= Level.Score)
        {
            yield return new WaitForSeconds(1f);

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
            yield return new WaitForSeconds(1f);
            ChangeState(new GameOverState(false, SceneData.Player, Application));
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
            Level.GiveScore(scorePrize.Score * multiplier);
            ScoreText.SetText(Level.CurrentScore.ToString());
        }
        else if (prize is PowerUpPrize powerUpPrize)
        {
            switch (powerUpPrize.PowerUp)
            {
                case PowerUpType.Troca:
                    yield return letterController.AnimatePrize(SwapButton.transform);
                    SwapButton.GivePowerUpUse();
                    break;
                case PowerUpType.Bomba:
                    yield return letterController.AnimatePrize(BombButton.transform);
                    BombButton.GivePowerUpUse();
                    break;
                case PowerUpType.Misturar:
                    yield return letterController.AnimatePrize(ShuffleButton.transform);
                    ShuffleButton.GivePowerUpUse();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}