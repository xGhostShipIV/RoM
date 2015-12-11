using UnityEngine;
using System.Collections;

public class Crush : StatusEffect {

    float damagePerTick;
    float DamagePerTick
    {
        get { return damagePerTick + (damagePerTick * caster.Malice); }
    }

    Damage damage;

    public Crush(Player _caster, Player _subject, EFFECT_TYPE _type, float _chanceToApply, float _damage, float _duration)
        : base(_caster, _subject, _type, _chanceToApply)
    {
        damagePerTick = _damage;
        baseDuration = _duration;

        damage = new Damage(damagePerTick, false, DAMAGE_TYPE.DAMAGE_TYPE_EARTH, null);

        icon = Resources.Load<Sprite>("rockthumbnail");
    }

    public override void OnTick()
    {
        damage.damageAmount = DamagePerTick / (1.0f / Time.deltaTime);
        subject.onDamage(damage);
    }
}
