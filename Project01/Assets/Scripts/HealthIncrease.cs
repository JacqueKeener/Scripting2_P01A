using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthIncrease : CollectibleBase
{

    [SerializeField] int _healthAdded = 1;

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
        //player.IncreaseHealth(_healthAdded);
    }
}
