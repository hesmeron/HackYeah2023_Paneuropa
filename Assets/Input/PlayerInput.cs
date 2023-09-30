using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : TeamController
{

    private PlayerControls _playerControls;

    private void Awake()
    {
        Debug.Log("Chceck play controller");
        _playerControls = new PlayerControls();

        _playerControls.Enable();
    }

    public override void ExecuteTurn()
    {
        _playerControls.Controller.StandardAttack.performed += StandardAttackOnperformed;
        _playerControls.Controller.NextSelection.performed += NextSelectionOnperformed;
        _playerControls.Controller.PreviousSelection.performed += PreviousSelectionOnperformed;
    }

    protected override void OnRunOutOfFighters()
    {
        Debug.Log("Player lost");
    }

    public void EndTurn()
    {
        _playerControls.Controller.StandardAttack.performed -= StandardAttackOnperformed;
        _playerControls.Controller.NextSelection.performed -= NextSelectionOnperformed;
        _playerControls.Controller.PreviousSelection.performed -= PreviousSelectionOnperformed;
        _turnManager.EndTurn();
    }

    private void PreviousSelectionOnperformed(InputAction.CallbackContext obj)
    {
        _turnManager.PassiveTeam.PreviousSelection();
    }

    private void NextSelectionOnperformed(InputAction.CallbackContext obj)
    {
        _turnManager.PassiveTeam.NextSelection();
    }

    private void StandardAttackOnperformed(InputAction.CallbackContext obj)
    {
        _turnManager.PassiveTeam.CurrentSelectedFighter.TakeDamage(50);
        EndTurn();
    }
}
