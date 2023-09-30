using UnityEngine;

public enum AttackType{
    Physical,
    Mental,
    Entangelment,
    Petrification,
}
public class Fighter : MonoBehaviour
{
    [SerializeField] 
    private int _physical;    
    [SerializeField] 
    private int _mental;
    [SerializeField] 
    private int _special;    
    [SerializeField] 
    private AttackType _specialAttackType;
    [SerializeField] 
    private TeamController _teamController;
    [SerializeField]
    private int _maxHealth = 100;
    private int _currentHealth;

    public int Physical => _physical;

    public int Mental => _mental;

    public int Special => _special;

    public AttackType SpecialAttackType => _specialAttackType;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void Select()
    {
        transform.localScale = new Vector3(HealthPercentage(),1f,1) *2f;
    }   
    public void Deselect()
    {
        transform.localScale = new Vector3(HealthPercentage(),1f,1);
    }

    public void TakeDamage(int damage, AttackType attackType = AttackType.Physical)
    {
        Debug.Log("Receive damage" + damage);
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            Die();
        }
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
