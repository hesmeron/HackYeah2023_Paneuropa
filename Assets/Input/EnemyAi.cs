using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : TeamController
{
    public override void ExecuteTurn()
    {
        int targetIndex = Random.Range(0, _turnManager.PassiveTeam.FighterCount());
        _turnManager.PassiveTeam.Select(targetIndex);
        PerformPhysicalAttack();
    }
    

    protected override void OnRunOutOfFighters()
    {
        Debug.Log("Enemy lost");
    }
}
