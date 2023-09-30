using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : TeamController
{

    [SerializeField]
    private RectTransform _attackChoice;
    [SerializeField] 
    private Camera _camera;
    
    private PlayerControls _playerControls;

    private void Awake()
    {
        Debug.Log("Chceck play controller");
        _playerControls = new PlayerControls();
        _attackChoice.gameObject.SetActive(false);
        _playerControls.Enable();
    }

    public override void ExecuteTurn()
    {
        _playerControls.Controller.StandardAttack.performed += StandardAttackOnperformed;
        _playerControls.Controller.NextSelection.performed += NextSelectionOnperformed;
        _playerControls.Controller.PreviousSelection.performed += PreviousSelectionOnperformed;
        _playerControls.Controller.GaugeRefill.performed += GaugeRefillOnperformed;
        _playerControls.Controller.SpecialAttack.performed += SpecialAttackOnperformed;
        _attackChoice.gameObject.SetActive(true);
        MoveUI();
    }

    protected override void OnRunOutOfFighters()
    {
        _turnManager.Win();
    }

    public override void EndTurn()
    {
        _playerControls.Controller.StandardAttack.performed -= StandardAttackOnperformed;
        _playerControls.Controller.NextSelection.performed -= NextSelectionOnperformed;
        _playerControls.Controller.PreviousSelection.performed -= PreviousSelectionOnperformed;
        _attackChoice.gameObject.SetActive(false);
        base.EndTurn();
    }
    
#region Callbacks
    
    private void SpecialAttackOnperformed(InputAction.CallbackContext obj)
    {
        PerformSpecial();
    }
    private void PreviousSelectionOnperformed(InputAction.CallbackContext obj)
    {
        _turnManager.PassiveTeam.PreviousSelection();
        MoveUI();
    }

    private void NextSelectionOnperformed(InputAction.CallbackContext obj)
    {
        _turnManager.PassiveTeam.NextSelection();
        MoveUI();
    }

    private void StandardAttackOnperformed(InputAction.CallbackContext obj)
    {
        PerformPhysicalAttack();
    }
    private void GaugeRefillOnperformed(InputAction.CallbackContext obj)
    {
        PerformTaunt();
    }
#endregion

    private void MoveUI()
    {
        Vector3 fighterPosition = _turnManager.PassiveTeam.CurrentSelectedFighter.transform.position;
        _attackChoice.position = _camera.WorldToScreenPoint(fighterPosition);
    }
}
