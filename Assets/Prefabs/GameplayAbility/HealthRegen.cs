using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Ability/Health Regen")]
public class HealthRegen : Ability
{


    // how much health to recover
    [SerializeField] float regenAmount = 20f;
    // in how long of a period to recover
    [SerializeField] float regenDuration = 3f;



    public override void ActivateAbility()
    {

        HealthComponent healthComp = OwningAbilityComponent.GetComponent<HealthComponent>();

        // grab the health comp from owner
        if (healthComp == null || healthComp.IsFull())
            return;

        if (!CommitAbility())
            return;

        StartCoroutine(RegenHealth(healthComp));


        // do a coroutine to recover the health
    }

    // coroutine function
    private IEnumerator RegenHealth(HealthComponent healthComp)
    {
        float timeLeft = regenDuration;
        float regenRate = regenAmount / regenDuration;

        while(timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            float deltaTime = Time.deltaTime;
            if (timeLeft < 0)
            {
                deltaTime += timeLeft;
            }
            healthComp.ChangeHealth(regenRate * Time.deltaTime, OwningAbilityComponent.gameObject);
            yield return new WaitForEndOfFrame();
        }

    }

}
