using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerIngredients : MonoBehaviour
{
    [SerializeField] private GameObject top;
    [SerializeField] private List<GameObject> ingredients = new List<GameObject>();
    [SerializeField] private List<Rigidbody> _immunityRigidbodies = new List<Rigidbody>();
    private Rigidbody _rb;

    private void Start()
    {
        // m_inputManager.dashEvent += LoseIngredient;
        _rb = GetComponent<Rigidbody>();
    }

    private void UpdatePosition()
    {
        float toAdd = 0.6f;
        float lastPosition = 0f;
        
        // position ingredients
        for (int i = 0; i < ingredients.Count; i++)
        {
            lastPosition += toAdd;
            ingredients[i].transform.localPosition = new Vector3(0,lastPosition,0);
            toAdd = ingredients[i].GetComponentInChildren<MeshRenderer>().bounds.size.y;
        }

        // position the top head
        lastPosition += toAdd;
        top.transform.localPosition = new Vector3(0, lastPosition, 0);
    }

    public void AddIngredient(GameObject ingredient)
    {
        // rotate same as player
        ingredient.transform.rotation.Equals(top.transform.rotation);
        
        // add to stack
        ingredient.transform.SetParent(gameObject.transform, false);
        ingredients.Add(ingredient);
        UpdatePosition();
    }

    public void LoseIngredient(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            if (ingredients.Count > 0)
            {
                // take newest ingredient and throw it out
                GameObject ingredient = ingredients.Last();
                ingredients.Remove(ingredient);
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

    private IEnumerator GiveImmune(Rigidbody rigidbodyToAdd)
    {
        _immunityRigidbodies.Add(rigidbodyToAdd);
        yield return new WaitForSeconds(0.5f);
        _immunityRigidbodies.Remove(rigidbodyToAdd);
    }

    public void OnCollisionEnter(Collision collision)
    {
        // collision with player
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("my magnitude: " + _rb.linearVelocity + " | " + _rb.linearVelocity.magnitude);
            Debug.Log("enemy magnitude: " + collision.rigidbody.linearVelocity + " | " + collision.rigidbody.linearVelocity.magnitude);

            if (_immunityRigidbodies.Contains(collision.rigidbody))
                return;

            if (_rb.linearVelocity.magnitude > 10f && collision.rigidbody.linearVelocity.magnitude < 10f)
            {
                Debug.Log("yes");
                StartCoroutine(GiveImmune(collision.rigidbody));
            }

            if (collision.rigidbody.linearVelocity.magnitude > 10f)
            {
                Vector3 direction = transform.position - collision.transform.position;

                //float angle = Vector3.Angle(collision.transform.forward, direction);

                // lose ingredients
                LoseIngredient(2);

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
