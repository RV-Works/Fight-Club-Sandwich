using System.Collections.Generic;
using UnityEngine;

public class PlayerIngredients : MonoBehaviour
{
    private List<GameObject> m_ingredients;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddIngredient(GameObject ingredient)
    {
        m_ingredients.Add(ingredient);
    }
}
