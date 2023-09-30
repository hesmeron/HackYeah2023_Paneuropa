using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi : TeamController
{
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
        _turnManager.Win();
    }

    private void SelectAndAttack()
    {
        int targetIndex = Random.Range(0, _turnManager.PassiveTeam.FighterCount());
        _turnManager.PassiveTeam.Select(targetIndex);
        PerformPhysicalAttack();
    }
}
