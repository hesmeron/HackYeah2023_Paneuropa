using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyAi : TeamController
{
    [SerializeField] 
    private List<EnemyWave> _enemyWaves;

    private int _waveCount = 0;

    private void Start()
    {
        SelectWave();
    }

    public override void ExecuteTurn()
    {
        SelectAndAttack();
    }

    public override void OneMore()
    {
        SelectAndAttack();
    }


    protected override void OnRunOutOfFighters()
    {
        _enemyWaves[_waveCount].gameObject.SetActive(false);
        _waveCount++;
        if (_waveCount < _enemyWaves.Count)
        {
            
            SelectWave();
        }
        else
        {
            _turnManager.Win();
        }
    }

    private void SelectWave()
    {
        _enemyWaves[_waveCount].gameObject.SetActive(true);
        _fighters = _enemyWaves[_waveCount].Figters;
    }

    private void SelectAndAttack()
    {
        int targetIndex = Random.Range(0, _turnManager.PassiveTeam.FighterCount());
        _turnManager.PassiveTeam.Select(targetIndex);
        Move move = CurrentSelectedFighter.RandomMove();
        switch (move.AttackType)
        {
            case AttackType.Physical:
                PerformPhysicalAttack();
                break;
            case AttackType.Mental:
                PerformTaunt();
                break;
            default:
                PerformSpecial();
                break;
        }
        
    }
}
