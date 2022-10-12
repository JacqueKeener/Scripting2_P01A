using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] ParticleSystem shootParticles;
    [SerializeField] AudioClip shootSound;
    [SerializeField] float[] cooldown = { .05f, .05f, .5f };
    private int cooldownIndex = 0;
    private bool canShoot = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("space") & canShoot)
        {
            canShoot = false;
            Instantiate(projectile, transform.position, transform.rotation);
            StartCoroutine(shootDelay(cooldown[cooldownIndex]));
        }
    }

    private IEnumerator shootDelay(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        canShoot = true;
        if (!Input.GetKey("space"))
        {
            cooldownIndex = 0;
        }
        else
        {
            cooldownIndex = (cooldownIndex + 1) % 3;
        }
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
