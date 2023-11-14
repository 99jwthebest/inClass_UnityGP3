using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Speed Boost")]
public class SpeedBoost : Ability
{
    [SerializeField] float boostAmt;
    [SerializeField] float boostDuration;

    IMovementInterface mMovementInterface;
    public override void ActivateAbility()
    {
        if (!CommitAbility()) return;
        mMovementInterface = OwningAbilityComponent.GetComponent<IMovementInterface>();

        if(mMovementInterface != null)
        {

        }

    }

    IEnumerator ResetSpeed()
    {
        yield return new WaitForSeconds(boostDuration);
    }
}
