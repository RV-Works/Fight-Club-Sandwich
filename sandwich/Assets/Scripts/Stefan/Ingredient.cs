using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour, ICollectable
{
    [SerializeField] private float m_throwVelocity = 3f;
    [SerializeField] private bool isGrounded = false;
    [SerializeField] private Ingredients _ingredientType;
    [SerializeField] private Material _outlineMaterial;
    private Rigidbody rb;
    private BoxCollider boxCollider;
    private const float GroundY = 0.02f;
    private const RigidbodyConstraints onGroundConstraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
    private const RigidbodyConstraints inAirConstraints = RigidbodyConstraints.FreezeRotation;
    private LayerMask playerLayer;
    private LayerMask nothingLayer;
    private MeshRenderer _meshRenderer;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        playerLayer = LayerMask.GetMask("Player");
        nothingLayer = LayerMask.GetMask("Nothing");
    }

    private void Start()
    {
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
        _outlineMaterial = new Material(_meshRenderer.materials[1]);
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

    public void ThrowOffStack()
    {
        // enable rigidbody movement
        rb.constraints = inAirConstraints;
        rb.useGravity = true;

        // player cannot interact
        boxCollider.excludeLayers += playerLayer;

        // add outline to ingredient
        AddOutline();

        // set random velocity
        rb.linearVelocity = new Vector3(Random.Range(-1f, 1f) * m_throwVelocity, 1, Random.Range(-1f, 1f) * m_throwVelocity);

        isGrounded = false;

        boxCollider.isTrigger = false;
        boxCollider.enabled = true;
    }

    internal void Grounded()
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

    public virtual void Collect(GameObject player)
    {
        boxCollider.enabled = false;
        player.GetComponent<PlayerIngredients>().AddIngredient(gameObject);
        RemoveOutline();
    }

    internal virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            Grounded();
            transform.position = new Vector3(transform.position.x, GroundY, transform.position.z);
        }
    }

    public void AddOutline()
    {
        _meshRenderer.materials = new Material[2] {_meshRenderer.material, _outlineMaterial};
    }

    public void RemoveOutline()
    {
        _meshRenderer.materials = new Material[]{ _meshRenderer.material };
    }

    public Ingredients GetIngredientType()
    {
        return _ingredientType;
    }
}