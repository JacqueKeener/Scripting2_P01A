using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : Damageable
{
    [SerializeField] int health = 3;
    [SerializeField] ParticleSystem killParticles;
    [SerializeField] AudioClip killSound;
    public Shake shaker;
    public int currentHealth
    {
        get => health;
    }
    public int maxHealth = 3;

    private void Start()
    {
        maxHealth = health;
    }

    public override void takeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            kill();
        }
    }

    private void kill()
    {
        feedback();
        gameObject.SetActive(false);
    }

    private void feedback()
    {
        shaker.TriggerShake(3f);
        if (killParticles != null)
        {
            killParticles = Instantiate(killParticles, transform.position, Quaternion.identity);
        }
        //audio. TODO - consider Object Pooling for performance
        if (killSound != null)
        {
            AudioHelper.PlayClip2D(killSound, 1f);
        }
    }
}
