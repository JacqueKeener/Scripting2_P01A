using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUpBase : MonoBehaviour
{
    protected abstract void PowerUp(Player player);
    protected abstract IEnumerator PowerDown(Player player, float duration);

    [SerializeField] float powerupDuration;
    [SerializeField] ParticleSystem _impactParticles;
    [SerializeField] float _movementSpeed = 1;
    [SerializeField] AudioClip _powerUpSound;
    [SerializeField] AudioClip _powerDownSound;
    public AudioClip PowerDownSound
    {
        get => _powerDownSound;
    }

    MeshRenderer _meshRenderer;
    BoxCollider _boxCollider;
    Rigidbody _rb;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _boxCollider = GetComponent<BoxCollider>();
        _rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("alpha");
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            PowerUp(player);
            PowerUpFeedback();
            StartCoroutine(PowerDown(player, powerupDuration));
            _meshRenderer.enabled = false;
            _boxCollider.enabled = false;
        }
    }

    private void PowerUpFeedback()
    {
        //particles
        if (_impactParticles != null)
        {
            _impactParticles = Instantiate(_impactParticles, transform.position, Quaternion.identity);
        }
        //audio. TODO - consider Object Pooling for performance
        if (_powerUpSound != null)
        {
            AudioHelper.PlayClip2D(_powerUpSound, 1f);
        }
    }

    private void FixedUpdate()
    {
        Movement(_rb);
    }
    protected virtual void Movement(Rigidbody rb)
    {
        //calculate rotation
        Quaternion turnOffset = Quaternion.Euler(0, _movementSpeed, 0);
        rb.MoveRotation(_rb.rotation * turnOffset);
    }



}
