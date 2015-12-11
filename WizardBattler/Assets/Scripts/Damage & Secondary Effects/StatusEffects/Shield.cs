using UnityEngine;
using System.Collections;

public class Shield : StatusEffect
{

    public GameObject barrierImage;
    public GameObject barrierCopy;

    public Shield(Player _caster, Player _subject, EFFECT_TYPE _type, float _chanceToApply, float _duration)
        : base(_caster, _subject, _type, _chanceToApply)
    {
        baseDuration = _duration;
        icon = Resources.Load<Sprite>("fireballthumbnail");
        barrierImage = Resources.Load<GameObject>("barrier");
    }

    public override void OnApply()
    {
        base.OnApply();

        subject.Immune = true;
        barrierCopy = GameObject.Instantiate(barrierImage);
        barrierCopy.transform.position = subject.transform.position - new Vector3(0.0f, 0, 0.5f);
    }

    public override void OnFallOff()
    {
        base.OnFallOff();

        subject.Immune = false;
        GameObject.Destroy(barrierCopy);
    }
}
