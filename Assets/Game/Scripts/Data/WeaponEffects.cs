using System;
using UnityEngine;

[Serializable]
public abstract class WeaponEffect
{
    public int name;
}

[Serializable]
public class WeaponPoisonEffect : WeaponEffect
{
    public int poisonTickDamage;
    public int poisonDuration;
}

[Serializable]
public class WeaponBurningEffect : WeaponEffect
{
    public int burnTickDamage;
    public int burnDuration;
}
