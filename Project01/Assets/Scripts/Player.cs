using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Health
{

    [SerializeField] MeshRenderer _body;
    [SerializeField] MeshRenderer gun;
    [SerializeField] Material _bodyMaterial;
    [SerializeField] Material invincibleMaterial;
    [SerializeField] AudioClip damageSound;
    private Health healthModule;
    private bool _invincible = false;
    public bool Invincible
    {
        get => _invincible;
    }

    TankController _tankController;


    // Start is called before the first frame update
    void Start()
    {
        _tankController = GetComponent<TankController>();
        healthModule = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown("backspace"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    /*
    public void IncreaseHealth(int amount)
    {
        _currentHealth += amount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        Debug.Log("Player's health: " + _currentHealth);
    }

    public void DecreaseHealth(int amount)
    {
        _currentHealth -= amount;
        Debug.Log("Player's health: " + _currentHealth);
        if(_currentHealth <= 0)
        {
            Kill();
        }

    }
    */

    public void ApplyBodyMaterial(Material mat)
    {
        _body.material = mat;
    }

    public void ResetBodyMaterial()
    {
        _body.material = _bodyMaterial;
    }

    public IEnumerator takeHit()
    {
        healthModule.takeDamage(1);
        if (damageSound != null)
        {
            AudioHelper.PlayClip2D(damageSound, 1f);
        }
        _invincible = true;
        _body.material = invincibleMaterial;
        gun.material = invincibleMaterial;
        yield return new WaitForSeconds(.5f);
        _body.material = _bodyMaterial;
        gun.material = _bodyMaterial;
        yield return new WaitForSeconds(.5f);
        _body.material = invincibleMaterial;
        gun.material = invincibleMaterial;
        yield return new WaitForSeconds(.5f);
        _body.material = _bodyMaterial;
        gun.material = _bodyMaterial;
        yield return new WaitForSeconds(.5f);
        _body.material = invincibleMaterial;
        gun.material = invincibleMaterial;
        yield return new WaitForSeconds(.5f);
        _body.material = _bodyMaterial;
        gun.material = _bodyMaterial;
        _invincible = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Hand hand = other.gameObject.GetComponent<Hand>();
        Eye eye = other.gameObject.GetComponent<Eye>();
        if (hand != null ^ eye != null)
        {
            if (Invincible == false)
            {
                StartCoroutine(takeHit());
                //ImpactFeedback();
            }
        }
    }

}
