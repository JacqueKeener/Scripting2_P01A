using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GunController : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] ParticleSystem shootParticles;
    [SerializeField] AudioClip shootSound;
    [SerializeField] float[] cooldown = { .05f, .05f, .5f };
    private IEnumerator burstTrack;
    private int cooldownIndex = 0;
    private bool canShoot = true;
    private bool canBurst = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey("space") & canShoot & canBurst)
        {
            float cooldownSum = 0f;
            for (int i = 0; i < cooldown.Length; i++)
            {
                cooldownSum += cooldown[i];
            }

            canShoot = false;
            Instantiate(projectile, transform.position, transform.rotation);
            StartCoroutine(shotDelay(cooldown[cooldownIndex]));
            
            if(cooldownIndex == 0)
            {
                burstTrack = burstDelay(cooldownSum);
                StartCoroutine(burstTrack);
            }
            if(cooldownIndex == cooldown.Length - 1)
            {
                canBurst = false;
            }
        }else if (!canBurst & burstTrack == null)
        {
            canBurst = true;
            cooldownIndex = 0;
            canShoot = true;
        }
    }

    private IEnumerator shotDelay(float cd)
    {
        yield return new WaitForSeconds(cd);
        canShoot = true;
        cooldownIndex = Mathf.Min((cooldownIndex + 1),cooldown.Length-1);
    }

    private IEnumerator burstDelay(float burstCooldown)
    {
        yield return new WaitForSeconds(burstCooldown + 0.01f);
        canBurst = true;
        canShoot = true;
        cooldownIndex = 0;
        burstTrack = null;
    }

    private void Feedback()
    {
        //particles
        if (shootParticles != null)
        {
            shootParticles = Instantiate(shootParticles, transform.position, Quaternion.identity);
        }
        //audio. TODO - consider Object Pooling for performance
        if (shootSound != null)
        {
            AudioHelper.PlayClip2D(shootSound, 1f);
        }
    }


}
