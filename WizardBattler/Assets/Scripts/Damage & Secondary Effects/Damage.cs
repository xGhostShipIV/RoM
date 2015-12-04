using UnityEngine;
using System.Collections;

public enum DAMAGE_TYPE
{
    DAMAGE_TYPE_FIRE,
    DAMAGE_TYPE_AIR,
    DAMAGE_TYPE_WATER,
    DAMAGE_TYPE_EARTH,
    DAMAGE_TYPE_LIFE,
    DAMAGE_TYPE_DEATH
}

public class Damage {

    public float damageAmount;
    public DAMAGE_TYPE damageType;
    public bool interups;
    public StatusEffect statusEffect;

    public Damage(float _damageAmount, bool _interupts, DAMAGE_TYPE _type, StatusEffect _se)
    {
        damageAmount = _damageAmount;
        interups = _interupts;
        damageType = _type;
        statusEffect = _se;
    }
}
