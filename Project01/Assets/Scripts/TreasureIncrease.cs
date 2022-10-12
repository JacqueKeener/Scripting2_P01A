using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TreasureIncrease : CollectibleBase
{

    [SerializeField] TextMeshProUGUI text;


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
        if(text != null)
        {
            text.text = (int.Parse(text.text) + 1).ToString();
        }
    }

}
