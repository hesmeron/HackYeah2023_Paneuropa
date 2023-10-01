using System;
using Sirenix.OdinInspector;
using UnityEngine;

public enum AttackType{
    Physical,
    Mental,
    Entangelment,
    Petrification,
    Showdown,
    Guard
}

public enum DefenseType
{
    Weak,
    Normal, 
    Resist,
}

public struct DamageInfo
{
    public DefenseType DefenseType;
    public int DamageGiven;
    public bool WasKnockedOutBefore;
}

public class Fighter : MonoBehaviour
{
    [SerializeField] 
    private GameObject _selectionLaser;
    [SerializeField] 
    private GameObject _knockoutEffect;
    [SerializeField]
    private int _physical;    
    [SerializeField] 
    private int _taunt;
    [SerializeField] 
    private int _special;    
    [SerializeField] 
    private AttackType _specialAttackType;
    [SerializeField]
    private DefenseType[] _defenseTypes = new DefenseType[4];
    [SerializeField] 
    private TeamController _teamController;
    [SerializeField]
    private int _maxHealth = 100;
    
    private int _currentHealth;
    [SerializeField]
    [ReadOnly]
    private bool isKnockedOut = false;
    
    public int Physical => _physical;

    public int Taunt => _taunt;

    public int Special => _special;

    public AttackType SpecialAttackType => _specialAttackType;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void Cleanse()
    {
        if (isKnockedOut)
        {
            isKnockedOut = false;
            _teamController.RemoveKnockout();
            _knockoutEffect.SetActive(false);
        }
    }

    public void Select()
    {
        transform.localScale = new Vector3(Mathf.Sign(transform.localScale.x),1f,1) *1.1f;
        _selectionLaser.gameObject.SetActive(true);
    }   
    public void Deselect()
    {
        transform.localScale = new Vector3(transform.localScale.x,1f,1);
        _selectionLaser.gameObject.SetActive(false);
    }

    public DefenseType GetDefenseType(AttackType attackType)
    {
        return _defenseTypes[(int) attackType];
    }

    public DamageInfo TakeDamage(int damage, AttackType attackType = AttackType.Physical)
    {
        DamageInfo damageInfo = new DamageInfo();
        damageInfo.WasKnockedOutBefore = isKnockedOut;

        DefenseType defenseType = GetDefenseType(attackType);
        switch (defenseType)
        {
            case DefenseType.Weak:
                damage *= 2;
                isKnockedOut = true;
                _knockoutEffect.SetActive(true);
                break;
            case DefenseType.Normal:
                break;
            case DefenseType.Resist:
                damage = 0;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        damageInfo.DamageGiven= damage;
        damageInfo.DefenseType = defenseType;
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            Die();
        }

        return damageInfo;
    }

    private void Die()
    {
        _teamController.RemoveFighter(this);
    }

    float HealthPercentage()
    {
        int percentage = (100 * _currentHealth) / _maxHealth;
        return percentage / 100f;
    }
}
