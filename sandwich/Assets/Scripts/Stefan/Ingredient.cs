using UnityEngine;

public class Ingredient : MonoBehaviour, ICollectable
{
    [SerializeField] private float m_throwVelocity = 3f;
    [SerializeField] private bool isGrounded = false;
    private Rigidbody rb;
    private BoxCollider boxCollider;
    private const float GroundY = 0.02f;
    private const RigidbodyConstraints onGroundConstraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
    private const RigidbodyConstraints inAirConstraints = RigidbodyConstraints.FreezeRotation;
    private LayerMask playerLayer;
    private LayerMask nothingLayer;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        playerLayer = LayerMask.GetMask("Player");
        nothingLayer = LayerMask.GetMask("Nothing");
    }

    void Update()
    {
        IsGrounded();
    }

    private void IsGrounded()
    {
        if (rb == null) return;

        // When ingredient reaches Y <= 0.02 turn off gravity once
        if (!isGrounded && transform.position.y <= GroundY)
        {
            Grounded();
            transform.position = new Vector3(transform.position.x,GroundY,transform.position.z);
        }
    }

    public void Throw()
    {
        // enable rigidbody movement
        rb.constraints = inAirConstraints;
        rb.useGravity = true;

        // player cannot interact
        boxCollider.excludeLayers += playerLayer;

        // set random velocity
        rb.linearVelocity = new Vector3(Random.Range(-1f, 1f) * m_throwVelocity, 1, Random.Range(-1f, 1f) * m_throwVelocity);

        isGrounded = false;

        boxCollider.isTrigger = false;
        boxCollider.enabled = true;
    }

    private void Grounded()
    {
        // player can interact
        boxCollider.excludeLayers = nothingLayer;

        isGrounded = true;
        rb.useGravity = false;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // disable RigidBody X and Y movement
        rb.constraints = onGroundConstraints;
        boxCollider.isTrigger = true;
    }

    public void Collect(GameObject player)
    {
        boxCollider.enabled = false;
        player.GetComponentInParent<PlayerIngredients>().AddIngredient(gameObject);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            Grounded();
            transform.position = new Vector3(transform.position.x, GroundY, transform.position.z);
        }
    }
}