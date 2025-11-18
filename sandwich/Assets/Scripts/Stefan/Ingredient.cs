using UnityEngine;

public class Ingredient : MonoBehaviour, ICollectable
{
    [SerializeField] private bool isGrounded = false;
    public bool SetGrounded
    {
        get { return isGrounded; }
        set { isGrounded = value; }
    }
    private Rigidbody rb;
    private BoxCollider boxCollider;
    private const float GroundY = 0.02f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
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

            boxCollider.isTrigger = true;
            transform.position = new Vector3(transform.position.x,GroundY,transform.position.z);
        }
    }

    public void Collect(GameObject player)
    {
        GetComponent<BoxCollider>().enabled = false;
        player.GetComponentInParent<PlayerIngredients>().AddIngredient(gameObject);
    }
}