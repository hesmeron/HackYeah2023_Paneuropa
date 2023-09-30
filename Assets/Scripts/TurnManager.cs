using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
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
        activeTeam.Select(currentFighterIndex);
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
}
