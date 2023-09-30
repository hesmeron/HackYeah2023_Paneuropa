using System;
using UnityEngine;

public enum AttackType{
    Physical,
    Mental,
    Entangelment,
    Petrification,
}

public enum DefenseType
{
    Weak,
    Normal, 
    Resist
}

public class Fighter : MonoBehaviour
{
    [SerializeField] 
    private TextSpawner _textSpawner;
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
        }
    }

    public void Select()
    {
        transform.localScale = new Vector3(HealthPercentage(),1f,1) *2f;
    }   
    public void Deselect()
    {
        transform.localScale = new Vector3(HealthPercentage(),1f,1);
    }

    public DefenseType TakeDamage(int damage, AttackType attackType = AttackType.Physical)
    {
        Debug.Log("Receive damage" + damage+ " " + attackType);
        DefenseType defenseType = _defenseTypes[(int) attackType];
        switch (defenseType)
        {
            case DefenseType.Weak:
                damage *= 2;
                string message = "Weak!";
                if (isKnockedOut)
                {
                    message = "Already down...";
                }
                else
                {
                    _teamController.AddKnockout();
                }

                isKnockedOut = true;
                _textSpawner.ShowText(message, transform.position + (Vector3.up/2f));

                break;
            case DefenseType.Normal:
                break;
            case DefenseType.Resist:
                _textSpawner.ShowText("Resist", transform.position + (Vector3.up/2f));
                damage = 0;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        _textSpawner.ShowText(damage.ToString(), transform.position);
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            Die();
            return DefenseType.Normal;
        }

        return defenseType;
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
