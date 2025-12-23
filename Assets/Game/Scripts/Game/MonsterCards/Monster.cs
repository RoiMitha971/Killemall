using System;
using UnityEngine;

public class Monster
{
    public Sprite Sprite => _sprite;
    public string MonsterName => _monsterName;
    public DamageType Resistance => _resistance;
    public DamageType Weakness => _weakness;

    public int Strength => _strength;
    public int MaxHealth => _maxHealth;
    public int Health => _health;
    public int Damage => _damage;

    public event Action<DamageInstance> OnDamaged;
    public event Action OnDeath;

    private Sprite _sprite;
    private string _monsterName;
    private DamageType _resistance;
    private DamageType _weakness;
    private int _strength;
    private int _maxHealth;
    private int _health;
    private int _damage;

    public Monster(MonsterData data)
    {
        _sprite = data.Sprite;
        _monsterName = data.MonsterName;
        _resistance = data.Resistance;
        _weakness = data.Weakness;
        _strength = data.Strength;
        _health = data.Health;
        _maxHealth = data.Health;
        _damage = data.Damage;
    }

    public void TakeDamage(DamageInstance damage)
    {
        if(_resistance == damage.Type)
        {
            damage.Amount = Mathf.CeilToInt((float)damage.Amount / 2);
        }
        else if(_weakness == damage.Type)
        {
            damage.Amount *= 2;
        }
        _health = Mathf.Clamp(_health - damage.Amount, 0, _maxHealth);

        OnDamaged?.Invoke(damage);

        if(_health <= 0)
        {
            Die();
        }
    }
    
    private void Die()
    {
        OnDeath?.Invoke();
    }
}
