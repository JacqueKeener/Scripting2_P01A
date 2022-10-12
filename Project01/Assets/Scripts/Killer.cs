using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killer : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void PlayerImpact(Player player)
    {
        //base.PlayerImpact(player);
        player.Kill();
    }


}
