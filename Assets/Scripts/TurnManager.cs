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
    private bool isPlayersTurn = true;
    [SerializeField]
    private int _currentFighterIndex = 0;

    [SerializeField]
    private TeamController activeTeam;
    [SerializeField]
    private TeamController passiveTeam;

    public TeamController ActiveTeam => activeTeam;

    public TeamController PassiveTeam => passiveTeam;

    private void Start()
    {
        StartTurn();
    }
    
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
    
    public void Skip()
    {
        _currentFighterIndex = 100;
        activeTeam.EndTurn();
    }
    
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
