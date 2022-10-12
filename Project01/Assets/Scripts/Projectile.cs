using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 20;
    [SerializeField] AudioClip hitSound;
    [SerializeField] ParticleSystem hitParticles;
    [SerializeField] float lifespan = 7f;
    public float Speed
    {
        get => speed;
        set => speed = value;
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(selfDestruct(lifespan));
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    private IEnumerator selfDestruct(float lifespan)
    {
        yield return new WaitForSeconds(lifespan);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Hand hand = other.GetComponent<Hand>();
        if(hand != null)
        {
            Feedback();
            hand.decreaseHealth();
            gameObject.SetActive(false);
        }
        Eye eye = other.GetComponent<Eye>();
        if(eye != null)
        {
            Feedback();
            eye.decreaseHealth();
            gameObject.SetActive(false);
        }
    }

    private void Feedback()
    {
        //particles
        if (hitParticles != null)
        {
            hitParticles = Instantiate(hitParticles, transform.position, Quaternion.identity);
        }
        //audio. TODO - consider Object Pooling for performance
        if (hitSound != null)
        {
            AudioHelper.PlayClip2D(hitSound, 1f);
        }
    }

}
