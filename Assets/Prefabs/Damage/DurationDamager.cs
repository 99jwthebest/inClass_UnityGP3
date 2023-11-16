using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DurationDamager : MonoBehaviour
{
    float duration;
    float damage;

    public void Init(float duration, float damage, HealthComponent healthComp, GameObject instigator, GameObject damageVFXPrefab)
    {
        this.duration = duration;
        this.damage = damage;

        GameObject damageVFX = Instantiate(damageVFXPrefab, healthComp.transform);

        StartCoroutine(DamageCoroutine(healthComp, instigator, damageVFX));
    }

    IEnumerator DamageCoroutine(HealthComponent healthComp, GameObject instigator, GameObject damageVFX)
    {
        float timeElapsed = 0;
        float damageRate = damage / duration;

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            healthComp.ChangeHealth(-damageRate * Time.deltaTime, instigator);
            yield return new WaitForEndOfFrame();
        }

        Destroy(damageVFX);
        Destroy(this);
    }

}
