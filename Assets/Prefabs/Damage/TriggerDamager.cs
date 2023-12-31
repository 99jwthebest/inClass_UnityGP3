using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDamager : DamageComponent
{
    [SerializeField] float damage;
    [SerializeField] Collider trigger;

    private void Start()
    {
        trigger.enabled = false;
    }

    public void DoDamage()
    {
        StartCoroutine(DamageCoroutine());
    }

    IEnumerator DamageCoroutine()
    {
        trigger.enabled = true;
        yield return new WaitForFixedUpdate();
        trigger.enabled = false;

    }

    protected override void ApplyDamage(GameObject target, float damage, GameObject instigator)
    {
        HealthComponent healthComp = target.GetComponent<HealthComponent>();
        if(healthComp != null)
        {
            healthComp.ChangeHealth(damage, instigator);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        DamageTarget(other.gameObject, damage, gameObject);
    }
}
