using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : Enemy
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

    public void decreaseHealth()
    {
        boss.decreaseHealth(3);
    }
}
