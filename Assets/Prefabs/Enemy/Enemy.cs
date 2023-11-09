using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour, IMovementInterface, IBTTaskInterface, ITeamInterface
{
    [SerializeField] ValueGauge healthBarPrefab;
    [SerializeField] Transform healthBarAttachTransform;
    [SerializeField] DamageComponent damageComponent;
    HealthComponent healthComponent;

    MovementComponent movementComponent;

    ValueGauge healthBar;

    Animator animator;

    Vector3 prevLocation;
    Vector3 velocity;

    private void Awake()
    {
        healthComponent = GetComponent<HealthComponent>();
        healthComponent.onTakenDamage += TookDamage;
        healthComponent.onHealthEmpty += StartDeath;
        healthComponent.onHealthChanged += HealthChanged;

        healthBar = Instantiate(healthBarPrefab, FindObjectOfType<Canvas>().transform);
        UIAttachComponent attachmentComp = healthBar.AddComponent<UIAttachComponent>();
        attachmentComp.SetupAttachment(healthBarAttachTransform);
        movementComponent = GetComponent<MovementComponent>();
        animator = GetComponent<Animator>();
        damageComponent.SetTeamInterface(this);

    }

    //public int GetTeamID() { return };

    private void CalculateVelocity()
    {

    }



    private void HealthChanged(float currentHealth, float delta, float maxHealth)
    {
        healthBar.SetValue(currentHealth, maxHealth);
    }

    private void StartDeath(float delta, float maxHealth)
    {
        Destroy(healthBar.gameObject);
        animator.SetTrigger("die");
        GetComponent<AIController>().StopAILogic();
        Debug.Log("Dead!!!");
    }

    private void TookDamage(float currentHealth, float delta, float maxHealth, GameObject instigator)
    {
        Debug.Log($"Took Damage {delta}, now health is: {currentHealth}/{maxHealth}");

    }

   

    public void RotateTowards(Vector3 direction)
    {
        movementComponent.RotateTowards(direction);
    }

    public void RotateTowards(GameObject target)
    {
        movementComponent.RotateTowards(target.transform.position - transform.position);
    }

    public void AttackTarget(GameObject target)
    {
        animator.SetTrigger("attack");
    }

    public void AttackPoint()
    {
        damageComponent.DoDamage();
    }

    public void DeathAnimationFinished()
    {
        Destroy(gameObject);
    }

}
