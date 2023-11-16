using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DurationDamager : MonoBehaviour
{
    float duration;
    float damage;

    public void Init(float duration, float damage, HealthComponent healthComp, GameObject instigator)
    {
        this.duration = duration;
        this.damage = damage;

        StartCoroutine(DamageCoroutine(healthComp, instigator));
    }

    IEnumerator DamageCoroutine(HealthComponent healthComp, GameObject instigator)
    {
        float timeElapsed = 0;
        float damageRate = damage / duration;

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            healthComp.ChangeHealth(-damageRate * Time.deltaTime, instigator);
            yield return new WaitForEndOfFrame();
        }

        Destroy(this);
    }

}
