using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerIngredients : MonoBehaviour
{
    [SerializeField] private GameObject m_top;
    [SerializeField] private List<GameObject> m_ingredients = new List<GameObject>();
    private Rigidbody m_rb;

    private void Start()
    {
        // m_inputManager.dashEvent += LoseIngredient;
        m_rb = GetComponent<Rigidbody>();
    }

    private void UpdatePosition()
    {
        float toAdd = 0.6f;
        float lastPosition = 0f;
        
        // position ingredients
        for (int i = 0; i < m_ingredients.Count; i++)
        {
            lastPosition += toAdd;
            m_ingredients[i].transform.localPosition = new Vector3(0,lastPosition,0);
            toAdd = m_ingredients[i].GetComponentInChildren<MeshRenderer>().bounds.size.y;
        }

        // position the top head
        lastPosition += toAdd;
        m_top.transform.localPosition = new Vector3(0, lastPosition, 0);
    }

    public void AddIngredient(GameObject ingredient)
    {
        // rotate same as player
        ingredient.transform.rotation.Equals(m_top.transform.rotation);
        
        // add to stack
        ingredient.transform.SetParent(gameObject.transform, false);
        m_ingredients.Add(ingredient);
        UpdatePosition();
    }

    public void LoseIngredient(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            if (m_ingredients.Count > 0)
            {
                // take newest ingredient and throw it out
                GameObject ingredient = m_ingredients.Last();
                m_ingredients.Remove(ingredient);
                ingredient.transform.SetParent(null);
                ingredient.GetComponent<Ingredient>().Throw();
            }
            else 
            { 
                return; 
            }
        }

        // update the top again
        UpdatePosition();
    }

    public void OnCollisionEnter(Collision collision)
    {
        // collision with player
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("magnitude: " + collision.rigidbody.linearVelocity.magnitude);


            if (collision.rigidbody.linearVelocity.magnitude > 10f)
            {
                Vector3 direction = transform.position - collision.transform.position;

                float angle = Vector3.Angle(collision.transform.forward, direction);

                // lose ingredients
                LoseIngredient(2);

                Debug.Log(angle);
                Debug.Log(name + " loses ingredients");
            }

            return;
        }        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ICollectable>(out ICollectable collectable))
        {
            collectable.Collect(gameObject);
        }
    }
}
