using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float chaseSpeed = 2f;
    public float chaseRange = 10f;
    public float jumpForce = 2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool shouldJump;

    public int damage = 1;

    public int maxHealth = 3;
    private int currentHealth;
    private SpriteRenderer spriteRenderer;
    private Color ogColor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
        ogColor = spriteRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        //Is grounded?
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 1f, groundLayer);

        //Player direction
        float direction = Mathf.Sign(player.position.x - transform.position.x);

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        bool isPlayerInRange = distanceToPlayer <= chaseRange;

        //Player above detection
        bool isPlayerAbove = Physics2D.Raycast(transform.position, Vector2.up, 5f, 1 << player.gameObject.layer);

        if (isGrounded && isPlayerInRange)
        {
            //Chase Player
            rb.linearVelocity = new Vector2(direction * chaseSpeed, rb.linearVelocity.y);

            // Jumping
            //If ground
            RaycastHit2D groundInFront = Physics2D.Raycast(transform.position, new Vector2(direction, 0), 2f, groundLayer);
            //If gap
            RaycastHit2D gapAhead = Physics2D.Raycast(transform.position + new Vector3(direction, 0, 0), Vector2.down, 2f, groundLayer);
            //If platform above
            RaycastHit2D platformAbove = Physics2D.Raycast(transform.position, Vector2.up, 5f, groundLayer);

            if (!groundInFront.collider && !gapAhead.collider)
            {
                shouldJump = true;
            }
            else if (isPlayerAbove && platformAbove.collider)
            {
                shouldJump = true;
            }
        }
        else
        {
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
            shouldJump = false;
        }
    }

    private void FixedUpdate()
    {
        if (isGrounded && shouldJump)
        {
            shouldJump = false;
            Vector2 direction = (player.position - transform.position).normalized;

            Vector2 jumpDirection = direction * jumpForce;

            rb.AddForce(new Vector2(jumpDirection.x, jumpForce), ForceMode2D.Impulse);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        StartCoroutine(FlashWhite());
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private IEnumerator FlashWhite()
    {
        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = ogColor; //Makes it so he returns to his original colour
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
