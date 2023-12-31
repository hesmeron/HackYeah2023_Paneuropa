using System;
using System.Collections.Generic;
using UnityEngine;

public  abstract  class TeamController : MonoBehaviour
{
    [SerializeField] private bool _isEnemy = false;
    [SerializeField]
    protected CameraManager _cameraManager;
    [SerializeField]
    private CombatPerformer _combatPerformer;
    [SerializeField]
    protected TurnManager _turnManager;

    [SerializeField] 
    protected List<Fighter> _fighters = new List<Fighter>();

    private int currentSelectedFighterIndex =  0;
    private Fighter _currentSelectedFighter;

    public List<Fighter> Fighters => _fighters;

    private int _kncokedOutCount = 0;

    public Fighter CurrentSelectedFighter => _currentSelectedFighter;

    public abstract void ExecuteTurn();

    public virtual void EndTurn()
    {
        _turnManager.EndTurn();
    }

    public abstract void OneMore();

    public int FighterCount()
    {
        return _fighters.Count;
    }

    public void RemoveFighter(Fighter fighter)
    {
        if(_fighters.Contains(fighter))
        {
            _fighters.Remove((fighter));
        }

        if (_fighters.Count == 0)
        {
            OnRunOutOfFighters();
        }
        else if(_currentSelectedFighter)
        {
            Select(0);
        }
        
    }

    protected abstract void OnRunOutOfFighters();
    public void NextSelection()
    {
        currentSelectedFighterIndex++;
        if (currentSelectedFighterIndex >= _fighters.Count)
        {
            currentSelectedFighterIndex = 0;
        }
        Select(currentSelectedFighterIndex);
    }

    public void PreviousSelection()
    {
        currentSelectedFighterIndex--;
        if (currentSelectedFighterIndex < 0)
        {
            currentSelectedFighterIndex = _fighters.Count-1;
        }
        
        Select(currentSelectedFighterIndex);
    }
    public void Select(int index)
    {

        if (_fighters.Count > index)
        {
            if (_currentSelectedFighter)
            {
                _currentSelectedFighter.Deselect();
            }
            currentSelectedFighterIndex = index;
            _currentSelectedFighter = _fighters[index];
            _currentSelectedFighter.Select();
            if (!_isEnemy)
            {
                _cameraManager.CloseUp(_currentSelectedFighter);
            }
            else
            {
                _cameraManager.CloseUpEnemy(_currentSelectedFighter);
            }
        }
    }
    public void SelectAndReady(int index)
    {
        Select(index);
        CurrentSelectedFighter.Cleanse();
    }

    public void AddKnockout()
    {
        _kncokedOutCount++;
    }
    
    public void RemoveKnockout()
    {
        _kncokedOutCount--;
    }

    protected void PerformPhysicalAttack()
    {
        _combatPerformer.PerformPhysicalAttack();
    }    
    protected void PerformTaunt()
    {
        _combatPerformer.PerformTaunt();
    }   
    protected void PerformSpecial()
    {
        AttackType attackType = _currentSelectedFighter.SpecialAttackType;

        if (attackType == AttackType.Guard)
        {
            foreach (var fighter in _fighters)
            {
                fighter.Guaard();
            }
            _combatPerformer.PerformGuardForAll();
        }
        else
        {
            _combatPerformer.PerformSpecialAttack();
        }

    }
    protected void PerformGuard()
    {
        _currentSelectedFighter.Guaard();
        _combatPerformer.PerformGuard();
    }


}
