
using UnityEngine;
using System.Collections.Generic;
using System;

public class HealthManager : MonoBehaviour
{
    public int CurrentHealth { get { return _currentHealth; } set { _currentHealth = value; } }
    [SerializeField]
    private int _currentHealth;
    public int MaxHealth => _maxHealth;
    [SerializeField]
    private int _maxHealth;

    private List<Hurtbox> _assignedHurtboxes = new List<Hurtbox>();


    public Action<int, int> OnHealthUpdate;
    public Action OnDeath;
    private bool _isDead = false;

    private void OnEnable()
    {
        CurrentHealth = _maxHealth;
    }
    /// <summary>
    /// Generic function for changing the health value of the target
    /// </summary>
    /// <param name="value"></param>
    public void ChangeHealth(int value)
    {
        if (!_isDead)
        {
            CurrentHealth = Mathf.Clamp(CurrentHealth += value, 0, MaxHealth);
            OnHealthUpdate?.Invoke(CurrentHealth, value);

            if (CurrentHealth <= 0)
            {
                _isDead = true;
                OnDeath?.Invoke();
            }
        }


    }

    private void OnDisable()
    {
        if(_assignedHurtboxes.Count > 0)
        {
            foreach (Hurtbox box in _assignedHurtboxes)
            {
                box.OnTakeDamage -= ChangeHealth;
            }
        }
    }
    /// <summary>
    /// Store reference for all hurtboxes on a target so they change the central health value
    /// </summary>
    /// <param name="hurtbox"></param>
    public void AssignHurtbox(Hurtbox hurtbox)
    {
        hurtbox.OnTakeDamage += ChangeHealth;
        _assignedHurtboxes.Add(hurtbox);
    }
}
