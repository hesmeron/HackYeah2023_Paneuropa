using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : TeamController
{
    [SerializeField]
    private RectTransform _attackChoice;  
    [SerializeField]
    private RectTransform _selectChoice;
    [SerializeField] 
    private Camera _camera;

    private bool _isChoosingSelf = false;
    
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
        _playerControls.Controller.Guard.performed += GuardOnperformed;
        _attackChoice.gameObject.SetActive(true);
        MoveUI();
    }

    private void GuardOnperformed(InputAction.CallbackContext obj)
    {
        PerformGuard();
    }

    protected override void OnRunOutOfFighters()
    {
        _turnManager.Lose();
    }
    
    public override void EndTurn()
    {
        _playerControls.Controller.StandardAttack.performed -= StandardAttackOnperformed;
        _playerControls.Controller.NextSelection.performed -= NextSelectionOnperformed;
        _playerControls.Controller.PreviousSelection.performed -= PreviousSelectionOnperformed;
        _playerControls.Controller.GaugeRefill.performed -= GaugeRefillOnperformed;
        _playerControls.Controller.SpecialAttack.performed -= SpecialAttackOnperformed;
        _playerControls.Controller.Guard.performed -= GuardOnperformed;
        _attackChoice.gameObject.SetActive(false);
        base.EndTurn();
    }

    public override void OneMore()
    {
        _isChoosingSelf = true;
        _attackChoice.gameObject.SetActive(false);
        _selectChoice.gameObject.SetActive(true);
    }

    #region Callbacks
    
    private void SpecialAttackOnperformed(InputAction.CallbackContext obj)
    {
        PerformSpecial();
    }
    private void PreviousSelectionOnperformed(InputAction.CallbackContext obj)
    {
        if (_isChoosingSelf)
        {
            PreviousSelection();
        }
        else
        {
            _turnManager.PassiveTeam.PreviousSelection();
        }
        MoveUI();
    }

    private void NextSelectionOnperformed(InputAction.CallbackContext obj)
    {
        if (_isChoosingSelf)
        {
            NextSelection();
        }
        else
        {
            _turnManager.PassiveTeam.NextSelection();
        }
        MoveUI();
    }

    private void StandardAttackOnperformed(InputAction.CallbackContext obj)
    {
        if (_isChoosingSelf)
        {
            _isChoosingSelf = false;
            _attackChoice.gameObject.SetActive(true);
            _selectChoice.gameObject.SetActive(false);
            MoveUI();
        }
        else
        {
            PerformPhysicalAttack();
        }

    }
    private void GaugeRefillOnperformed(InputAction.CallbackContext obj)
    {
        PerformTaunt();
    }
#endregion

    private void MoveUI()
    {
        if (_isChoosingSelf)
        {
            Vector3 fighterPosition = CurrentSelectedFighter.transform.position;
            _selectChoice.position = _camera.WorldToScreenPoint(fighterPosition);
        }
        else
        {
            Vector3 fighterPosition = _turnManager.PassiveTeam.CurrentSelectedFighter.transform.position;
            _attackChoice.position = _camera.WorldToScreenPoint(fighterPosition);
        }
    }
}
