using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilityPowerup : PowerUpBase
{

    [SerializeField] Material _bodyMaterial;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void PowerUp(Player player)
    {
        player.Invincible = true;
        player.ApplyBodyMaterial(_bodyMaterial);
    }

    protected override IEnumerator PowerDown(Player player, float duration)
    {
        yield return new WaitForSeconds(duration);
        player.Invincible = false;
        player.ResetBodyMaterial();
        AudioHelper.PlayClip2D(PowerDownSound, 1f);
        gameObject.SetActive(false);
    }
}
