using Sirenix.OdinInspector;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [SerializeField]
    private CombatPerformer _combatPerformer;
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
    private int _currentFighterIndex = 0;

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
        _combatPerformer.StartPerformance(activeTeam, passiveTeam);
        activeTeam.SelectAndReady(_currentFighterIndex);
        passiveTeam.Select(0);
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
        _currentFighterIndex = 100;
        activeTeam.EndTurn();
    }

    [Button]
    public void EndTurn()
    {
        SetTurnTeams();
        _currentFighterIndex++;
        if (_currentFighterIndex >= activeTeam.FighterCount())
        {
            isPlayersTurn = !isPlayersTurn;
            _currentFighterIndex = 0;
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
