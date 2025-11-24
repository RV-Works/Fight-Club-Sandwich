using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerIngredients : MonoBehaviour
{
    [SerializeField] private GameObject _topBack;
    [SerializeField] private GameObject _topFront;
    [SerializeField] private GameObject _eyeLeft;
    [SerializeField] private GameObject _eyeRight;
    [SerializeField] private GameObject _ingredientParent;
    [SerializeField] private List<GameObject> ingredients = new List<GameObject>();
    [SerializeField] private List<Rigidbody> _immunityRigidbodies = new List<Rigidbody>();
    private const float _teunScale = 7.815301f; // the model has a scale for some fking reason
    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void UpdatePosition()
    {
        float toAdd = 0f;
        float lastPosition = 0f;
        
        // position ingredients
        for (int i = 0; i < ingredients.Count; i++)
        {
            lastPosition += toAdd;
            ingredients[i].transform.localPosition = new Vector3(0,0,lastPosition);
            toAdd = ingredients[i].GetComponentInChildren<MeshRenderer>().bounds.size.y * ingredients[i].transform.localScale.y;
        }

        // position the top head
        lastPosition += toAdd;
        _topBack.transform.localPosition = new Vector3(0, 0, lastPosition);
        _topFront.transform.localPosition = new Vector3(0, 0, lastPosition);

        // position the eyes
        lastPosition += 0.03173957f;
        _eyeLeft.transform.localPosition = new Vector3(_eyeLeft.transform.localPosition.x, _eyeLeft.transform.localPosition.y, lastPosition);
        _eyeRight.transform.localPosition = new Vector3(_eyeRight.transform.localPosition.x, _eyeRight.transform.localPosition.y, lastPosition);
    }

    public void AddIngredient(GameObject ingredient)
    {
        // rotate same as player
        // ingredient.transform.rotation.Equals(top.transform.rotation);
        
        // add to stack
        ingredient.transform.SetParent(_ingredientParent.transform, true);
        //ingredient.transform.localScale = Vector3.one / _teunScale;
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
