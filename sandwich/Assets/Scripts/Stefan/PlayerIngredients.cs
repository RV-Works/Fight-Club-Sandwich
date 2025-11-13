using System.Collections.Generic;
using UnityEngine;

public class PlayerIngredients : MonoBehaviour
{
    [SerializeField] private GameObject m_top;
    [SerializeField] private List<GameObject> m_ingredients = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdatePosition()
    {
        float toAdd = 1.6f;
        float lastPosition = 0f;
        for (int i = 0; i < m_ingredients.Count; i++)
        {
            lastPosition += toAdd;
            m_ingredients[i].transform.position = new Vector3(0,lastPosition,0);
            toAdd = m_ingredients[i].GetComponentInChildren<MeshRenderer>().bounds.size.y;
        }
    }

    public void AddIngredient(GameObject ingredient)
    {
        ingredient.transform.rotation.Equals(m_top.transform.rotation);
        ingredient.transform.SetParent(gameObject.transform, false);
        m_ingredients.Add(ingredient);
        UpdatePosition();
    }

    public void OnTriggerEnter(Collider other)
    {
        other.GetComponent<ICollectable>().Collect(gameObject);
    }
}
