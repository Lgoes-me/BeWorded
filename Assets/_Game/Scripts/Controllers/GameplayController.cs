using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour
{
    [field: SerializeField] private int Height { get; set; }
    [field: SerializeField] private int Width { get; set; }
    
    [field: SerializeField] private ContentManager ContentManager { get; set; }
    [field: SerializeField] private GameAreaController GameAreaController { get; set; }
    [field: SerializeField] private LetterController LetterControllerPrefab { get; set; }
    [field: SerializeField] private TextMeshProUGUI Response { get; set; }
    
    [field: SerializeField] private Button SwapButton { get; set; }
    [field: SerializeField] private Button BombButton { get; set; }
    
    public Grid<LetterController> LettersGrid { get; private set; }
    public GameStateMachine StateMachine { get; private set; }
    
    private void Awake()
    {
        LettersGrid = new Grid<LetterController>(Height, Width, CreateLetterController);
        StateMachine = new GameStateMachine(this);

        GameAreaController.Init(this, Height, Width);
        
        SwapButton.onClick.AddListener(() =>
        {
            StateMachine.ChangeState(new SwapDragState(this));
        });
        
        BombButton.onClick.AddListener(() =>
        {
            StateMachine.ChangeState(new BombState(this));
        });
    }
    
    private LetterController CreateLetterController()
    {
        return Instantiate(LetterControllerPrefab, GameAreaController.transform).Init(ContentManager.GetRandomLetter);
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

    public void ClearSelection(List<LetterController> letterControllers)
    {
        StartCoroutine(ClearSelectionCoroutine(letterControllers));
    }
    
    private IEnumerator ClearSelectionCoroutine(List<LetterController> letterControllers)
    {
        StateMachine.ChangeState(new AnimatingState());

        LettersGrid.ClearCells(letterControllers);

        yield return LettersGrid.SortEmpty();
        yield return LettersGrid.FillNewData();

        StateMachine.ChangeState(new GameplayState(this));
    }
}