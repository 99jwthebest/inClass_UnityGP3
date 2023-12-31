using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Ability/Fire")]
public class Fire : Ability
{
    [SerializeField] TargetScanner scannerPrefab;
    [SerializeField] float range = 3;
    [SerializeField] float scanDuration = 0.5f;

    [SerializeField] float damage = 40f;
    [SerializeField] float damageDuration = 3f;

    [SerializeField] GameObject ScanVFX;
    [SerializeField] GameObject damageVFX;
    TargetScanner scanner;
    public override void ActivateAbility()
    {
        if(!CommitAbility()) 
            return;

        scanner = Instantiate(scannerPrefab);
        scanner.Init(range, scanDuration, ScanVFX == null ? null : Instantiate(ScanVFX));
        scanner.SetupAttachment(OwningAbilityComponent.gameObject.transform);
        scanner.StartScan();
        scanner.onNewTargetFound += ApplyDamage;
    }

    private void ApplyDamage(GameObject target)
    {
        HealthComponent targetHealthComp = target.GetComponent<HealthComponent>();
        if (targetHealthComp == null)
            return;

        if(target.GetComponent<ITeamInterface>().GetRelationTowards(OwningAbilityComponent.gameObject) != TeamRelation.Hostile) 
            return;

        // do the coroutine?
        DurationDamager damager = targetHealthComp.gameObject.AddComponent<DurationDamager>();
        damager.Init(damageDuration, damage, targetHealthComp, OwningAbilityComponent.gameObject, damageVFX);

    }
}
