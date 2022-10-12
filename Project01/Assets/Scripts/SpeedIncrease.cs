using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedIncrease : CollectibleBase
{

    [SerializeField] float _speedAmount = .5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void Collect(Player player)
    {
        TankController controller = player.GetComponent<TankController>();
        if(controller != null)
        {
            controller.MaxSpeed += _speedAmount;
            Debug.Log(controller.MaxSpeed);
        }
    }

    protected override void Movement(Rigidbody rb)
    {
        Quaternion turnOffset = Quaternion.Euler(MovementSpeed, MovementSpeed, MovementSpeed);
        rb.MoveRotation(rb.rotation * turnOffset);
    }

}
