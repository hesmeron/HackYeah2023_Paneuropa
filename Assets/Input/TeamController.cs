using System.Collections.Generic;
using UnityEngine;

public  abstract  class TeamController : MonoBehaviour
{
    [SerializeField]
    protected TurnManager _turnManager;

    [SerializeField] 
    private List<Fighter> _fighters = new List<Fighter>();

    private int currentSelectedFighterIndex =  0;
    private Fighter _currentSelectedFighter;

    public Fighter CurrentSelectedFighter => _currentSelectedFighter;

    public abstract void ExecuteTurn();

    public virtual void EndTurn()
    {
        _turnManager.EndTurn();
    }
    
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
        }
    }

    protected void PerformPhysicalAttack()
    {
        _turnManager.PassiveTeam.CurrentSelectedFighter.TakeDamage(CurrentSelectedFighter.Physical);
        EndTurn();
    }


}
