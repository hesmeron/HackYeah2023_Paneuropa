using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

[System.Serializable]
public struct Move
{
    public AttackType AttackType;
    public float Damage;
}
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
    private Transform _cameraPosition;       
    [SerializeField] 
    private Transform _effectPosition;    
    [SerializeField] 
    private Transform _partialCameraPosition;
    [SerializeField]
    private List<Move> moves = new List<Move>();
    [SerializeField] 
    private GameObject _selectionLaser;    
    [SerializeField] 
    private SpriteRenderer _renderer;
    [SerializeField]
    private Color _deadColor = Color.black;    
    [SerializeField]
    private Color _petrifiedColor = Color.gray;
    [SerializeField] 
    private SpriteRenderer _vines;
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

    [SerializeField]
    private bool _isGuarding = false;
    [SerializeField]
    private bool _isEntangled = false;
    [SerializeField]
    private bool _isPetrified= false;

    public Transform CameraPosition => _cameraPosition;

    public Transform EffectPosition => _effectPosition;

    public Transform PartialCameraPosition => _partialCameraPosition;

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
            _isGuarding = false;
            _isEntangled = false;
            SetVinesColor(0f);
            _isPetrified = false;
            _renderer.color = Color.white;
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
        DefenseType defenseType = _defenseTypes[(int) attackType];
        if (defenseType == DefenseType.Weak && _isGuarding)
        {
            defenseType = DefenseType.Normal;
        }

        if (attackType == AttackType.Mental && _isEntangled)
        {
            return DefenseType.Weak;
        }       
        if (attackType == AttackType.Physical && _isPetrified)
        {
            return DefenseType.Weak;
        }
        return defenseType;
    }

    public DamageInfo TakeDamage(int damage, AttackType attackType = AttackType.Physical)
    {
        DamageInfo damageInfo = new DamageInfo();
        damageInfo.WasKnockedOutBefore = isKnockedOut;

        if (_isGuarding)
        {
            damage /= 2;
        }
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

    public void Guaard()
    {
        _isGuarding = true;
    }
    public void Entangle()
    {
        _isEntangled = true;
        SetVinesColor(1f);

    }
    public void Petrify()
    {
        _isPetrified = true;
        _renderer.color = _petrifiedColor;
    }   
    
    public void Showdown()
    {
        _isGuarding = false;
    }

    public Move RandomMove()
    {
        return moves[Random.Range(0, moves.Count)];
    }

    private void SetVinesColor(float alpha)
    {
        Color color = _vines.color;
        color.a = alpha;
        _vines.color = color;
    }
    
    private void Die()
    {
        _teamController.RemoveFighter(this);
        _renderer.color = _deadColor;
    }

    float HealthPercentage()
    {
        int percentage = (100 * _currentHealth) / _maxHealth;
        return percentage / 100f;
    }
}
