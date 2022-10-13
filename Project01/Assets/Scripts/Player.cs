using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : Health
{

    [SerializeField] MeshRenderer _body;
    [SerializeField] MeshRenderer gun;
    [SerializeField] Material _bodyMaterial;
    [SerializeField] Material invincibleMaterial;
    [SerializeField] AudioClip damageSound;
    [SerializeField] Image hpBar;
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

        hpBar.fillAmount = (float)healthModule.currentHealth / (float)healthModule.maxHealth;

        
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
        Hand h = other.GetComponent<Hand>();
        Eye e = other.GetComponent<Eye>();
        KillPlane kp = other.gameObject.GetComponent<KillPlane>();
        if (kp != null)
        {
            takeDamage(30);
        }
        if(h != null ^ e != null)
        {
            if (!_invincible)
            {
                healthModule.shaker.TriggerShake(1f);
                StartCoroutine(takeHit());
            }
        }
    }

}
