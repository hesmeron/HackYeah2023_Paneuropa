 using System;
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    [SerializeField] 
    private TeamController _teamController;
    [SerializeField]
    private int _maxHealth = 100;
    private int _currentHealth;

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

    public void TakeDamage(int damage)
    {
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
