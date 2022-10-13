using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : Health
{
    [SerializeField] BossController boss;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void takeDamage(int damage)
    {
        boss.takeHit(1);
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if(player != null)
        {
            if (player.Invincible == false)
            {
                player.takeHit();
            }
        }
    }
}
