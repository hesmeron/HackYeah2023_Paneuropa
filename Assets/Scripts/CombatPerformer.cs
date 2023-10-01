using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class CombatPerformer : MonoBehaviour
{
    [SerializeField] private float _effectVariation = 0.3f;
    [SerializeField] 
    private TextSpawner _textSpawner;
    [SerializeField]
    private TurnManager _turnManager;
    [SerializeField] 
    private DamageEffect[] _damageEffects = new DamageEffect[4];

    [SerializeField]
    [ReadOnly]
    private TeamController _attacking;
    [SerializeField]
    [ReadOnly]
    private TeamController _target;

    public void StartPerformance(TeamController attacking, TeamController target)
    {
        _attacking = attacking;
        _target = target;
    }

    public void PerformPhysicalAttack()
    {
        Debug.Log("Perform physical attack");
        int baseDamage = _attacking.CurrentSelectedFighter.Physical; 
        DamageInfo info = _target.CurrentSelectedFighter.TakeDamage(baseDamage, AttackType.Physical);
        StartCoroutine(AnimateAttack(info));
    }  
    
    public void PerformTaunt()
    {
        Debug.Log("Perform physical attack");
        int baseDamage = _attacking.CurrentSelectedFighter.Taunt; 
        DamageInfo info = _target.CurrentSelectedFighter.TakeDamage(baseDamage, AttackType.Mental);
        StartCoroutine(AnimateAttack(info));
    }    
    
    public void PerformSpecialAttack()
    {
        Debug.Log("Perform physical attack");
        int baseDamage = _attacking.CurrentSelectedFighter.Special;
        AttackType attackType = _attacking.CurrentSelectedFighter.SpecialAttackType;
        DamageInfo info = _target.CurrentSelectedFighter.TakeDamage(baseDamage, attackType);
        StartCoroutine(AnimateAttack(info));
    }

    public void PerformGuard()
    {
        InstantiateEffect(_damageEffects[(int) DefenseType.Resist], _attacking.CurrentSelectedFighter.transform.position);
        _turnManager.EndTurn();
    }

    IEnumerator AnimateAttack(DamageInfo info)
    {

        Vector3 targetPosition = _target.CurrentSelectedFighter.transform.position;
        bool doNextTurn = false;
        int repetitions = Mathf.CeilToInt(info.DamageGiven / 25f);
        DamageEffect effect = _damageEffects[(int) info.DefenseType];
        for (int i = 0; i < repetitions; i++)
        {
            InstantiateEffect(effect, targetPosition);
            yield return new WaitForSeconds(0.3f);
        }
        switch (info.DefenseType)
        {

            case DefenseType.Weak:
                string message = "Weak!";
                if (info.WasKnockedOutBefore)
                { 
                    message = "Already down...";
                }
                else
                {
                    doNextTurn = true;
                }
                _textSpawner.ShowText(message, targetPosition + (Vector3.up/2f));
                break;
            case DefenseType.Resist:
                _textSpawner.ShowText("Resist", targetPosition + (Vector3.up/2f));
                break;
        }
        _textSpawner.ShowText(info.DamageGiven.ToString(), targetPosition);
        yield return new WaitForSeconds(1.5f);
        if (info.DefenseType == DefenseType.Resist)
        {
            _turnManager.Skip();
        }
        else
        {
            EvaluateTurn(doNextTurn);
        }

    }

    private void InstantiateEffect(DamageEffect effect, Vector3 targetPosition)
    {
        float x = Random.Range(-_effectVariation, +_effectVariation);
        float y = Random.Range(-_effectVariation, +_effectVariation);
        Instantiate(effect, targetPosition +  new Vector3(x,y,0), Quaternion.identity);

    }

    private void EvaluateTurn(bool oneMore)
    {
        if (!oneMore)
        {
            _turnManager.EndTurn();
        }
        else
        {
            _attacking.OneMore();
        }

    }
}
