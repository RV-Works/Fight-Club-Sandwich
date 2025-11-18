using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerIngredients : MonoBehaviour
{
    [SerializeField] private GameObject m_top;
    [SerializeField] private List<GameObject> m_ingredients = new List<GameObject>();
    [SerializeField] private float m_velocity = 10f;

    private void UpdatePosition()
    {
        float toAdd = 0.235f;
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
        ingredient.transform.rotation.Equals(m_top.transform.rotation);
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
                GameObject ingredient = m_ingredients.Last();
                m_ingredients.Remove(ingredient);
                ingredient.transform.parent = null;
                
                Rigidbody rb = ingredient.GetComponent<Rigidbody>();
                rb.useGravity = true;

                rb.AddForce(Random.Range(-1f,1f) * m_velocity,0,Random.Range(-1f, 1f) * m_velocity);

                ingredient.GetComponent<Ingredient>().SetGrounded = false;
                
                ingredient.GetComponent<BoxCollider>().enabled = true;
            }
        }
        UpdatePosition();
    }

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.collider.tag);
        if (!collision.collider.CompareTag("Player"))
            return;

        Rigidbody rigidbody = GetComponent<Rigidbody>();
        Debug.Log(collision.rigidbody.linearVelocity.magnitude);

        if (collision.rigidbody.linearVelocity.x > 10f || collision.rigidbody.linearVelocity.x < -10f)
        {
            Vector3 direction = transform.position - collision.transform.position;

            float angle = Vector3.Angle(collision.transform.forward, direction);

            Debug.Log(angle);
            Debug.Log(name + " loses ingredients");
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        other.GetComponent<ICollectable>().Collect(gameObject);
    }
}
