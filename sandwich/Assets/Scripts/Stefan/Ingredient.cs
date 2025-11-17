using UnityEngine;

public class Ingredient : MonoBehaviour, ICollectable
{
    [SerializeField] private bool isGrounded = false;
    private Rigidbody rb;
    private const float GroundY = 0.02f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        IsGrounded();
    }

    void IsGrounded()
    {
        if (rb == null) return;

        // When ingredient reaches Y <= 0.02 turn off gravity once
        if (!isGrounded && transform.position.y <= GroundY)
        {
            isGrounded = true;
            rb.useGravity = false;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    public void Collect(GameObject player)
    {
        GetComponent<BoxCollider>().enabled = false;
        player.GetComponentInParent<PlayerIngredients>().AddIngredient(gameObject);
    }
}