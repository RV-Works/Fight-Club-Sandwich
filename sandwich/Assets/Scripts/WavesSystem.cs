using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class WavesSystem : MonoBehaviour
{
    [SerializeField] private List<Ingredient> ingredients;
    //[SerializeField] private List<SabotageItem> sabotageItems;
    [SerializeField] private int WavesCount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (WavesCount == 1)
        {
            wave1();
        }
        else if (WavesCount == 2)
        {
            wave2();
        }
        else if (WavesCount == 3)
        {
            wave3();
        }
    }

    void wave1() 
    { 
        
    }
    void wave2() 
    { 

    }
    void wave3() 
    {

    }

}
