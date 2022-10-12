using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] int _maxHealth = 3;
    [SerializeField] MeshRenderer _body;
    [SerializeField] Material _bodyMaterial;
    int _currentHealth;
    private bool _invincible = false;
    public bool Invincible
    {
        get => _invincible;
        set => _invincible = value;
    }

    TankController _tankController;


    // Start is called before the first frame update
    void Start()
    {
        _tankController = GetComponent<TankController>();
    }

    // Update is called once per frame
    void Update()
    {
        _currentHealth = _maxHealth;
    }


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

    public void Kill()
    {
        gameObject.SetActive(false);
        //play particles
        //play sounds
    }

    public void ApplyBodyMaterial(Material mat)
    {
        _body.material = mat;
    }

    public void ResetBodyMaterial()
    {
        _body.material = _bodyMaterial;
    }


}
