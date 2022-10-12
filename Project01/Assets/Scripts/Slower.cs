using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slower : Enemy
{

    [SerializeField] float _speedAmount = .1f;

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
        TankController controller = player.GetComponent<TankController>();
        if(controller != null)
        {
            controller.MaxSpeed = _speedAmount;
        }
    }


}
