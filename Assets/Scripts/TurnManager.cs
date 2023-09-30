using Sirenix.OdinInspector;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [SerializeField] 
    private GameObject _vicotryPanel;
    [SerializeField] 
    private GameObject _defeatPanel;
    [SerializeField] 
    private PlayerInput _playerInput;
    [SerializeField]
    private TeamController _enemyTeamController;

    [SerializeField]
    [ReadOnly]
    private bool isPlayersTurn = true;
    [SerializeField]
    [ReadOnly]
    private int currentFighterIndex = 0;
    private int currentTargetrIndex = 0;
    private Fighter _currentFighter;

    [SerializeField]
    [ReadOnly]
    private TeamController activeTeam;
    [SerializeField]
    [ReadOnly]
    private TeamController passiveTeam;

    public TeamController ActiveTeam => activeTeam;

    public TeamController PassiveTeam => passiveTeam;

    private void Start()
    {
        StartTurn();
    }

    [Button]
    void StartTurn()
    {
        SetTurnTeams();
        activeTeam.SelectAndReady(currentFighterIndex);
        passiveTeam.Select(currentTargetrIndex);
        if (isPlayersTurn)
        {
            _playerInput.ExecuteTurn();
        }
        else
        {
            _enemyTeamController.ExecuteTurn();
        }
    }
    
    [Button]
    public void Skip()
    {
        currentFighterIndex = 100;
        activeTeam.EndTurn();
    }

    [Button]
    public void EndTurn()
    {
        SetTurnTeams();
        currentFighterIndex++;
        if (currentFighterIndex >= activeTeam.FighterCount())
        {
            isPlayersTurn = !isPlayersTurn;
            currentTargetrIndex = 0;
            currentFighterIndex = 0;
        }
        StartTurn();
    }

    private void SetTurnTeams()
    {
        activeTeam = isPlayersTurn ? _playerInput : _enemyTeamController;
        passiveTeam = isPlayersTurn ? _enemyTeamController : _playerInput;
    }

    public void Win()
    {
        _vicotryPanel.SetActive(true);
    }    
    public void Lose()
    {
        _defeatPanel.SetActive(true);
    }
}
