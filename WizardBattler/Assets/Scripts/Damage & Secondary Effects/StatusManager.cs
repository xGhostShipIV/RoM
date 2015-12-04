using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StatusManager
{
    //A list to hold all effects that are on the player
    public List<StatusEffect> effects;

    public StatusManager()
    {
        effects = new List<StatusEffect>();
    }

    //Calls the update of each effect and removes it if its time to fall off
    public void UpdateEffects()
    {
        for (int i = effects.Count - 1; i >= 0; i--)
        {
            if (!effects[i].Update())
            {
                effects.Remove(effects[i]);
            }
        }
    }

    //Attempts to add an effect to the list. If the effect already exists on the player
    //the duration of the effect is refreshed. 
    public void Add(StatusEffect _effect)
    {
        
        for (int i = 0; i < effects.Count; i++)
        {
            if (effects[i].Equals(_effect))
            {
                effects[i].elapsedTime = 0;
                return;
            }
        }

        _effect.OnApply();
        effects.Add(_effect);
    }
}
