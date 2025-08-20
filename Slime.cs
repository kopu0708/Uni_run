using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Slime : MonoBehaviour
{
    [Header("Target")]
    public Transform playerTransform;

    [Header("Movement")]
    public float moveSpeed = 2f;
    public float jumpForce = 500f;
    public float jumpInterval = 2f;

    [Header("Ground Check")]
    public LayerMask groundLayer;
    public float groundCheckDistance = 0.2f;
    public Transform groundCheckPoint;

    private Rigidbody2D rb;
    private bool isGrounded;
    private Vector3 initialScale;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialScale = transform.localScale;

        if (playerTransform == null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                playerTransform = player.transform;
            }
            else
            {
                Debug.LogError("Player not found! Please assign the player Transform in the inspector.");
            }
        }
        StartCoroutine(JumpRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfGrounded();
    }

    private void CheckIfGrounded()
    {
        isGrounded = Physics2D.Raycast(groundCheckPoint.position, Vector2.down, groundCheckDistance, groundLayer);
        Debug.DrawRay(groundCheckPoint.position, Vector2.down * groundCheckDistance, isGrounded ? Color.green : Color.red);
    }

    private IEnumerator JumpRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(jumpInterval);
            if (isGrounded && playerTransform != null)
            {
                Jump();
            }
        }
    }
    private void Jump()
    {
        Vector2 jumpDirection = (playerTransform.position - transform.position).normalized;
        if (jumpDirection.x > 0)
        {
            transform.localScale = new Vector3(-initialScale.x, initialScale.y, initialScale.z);
        }
        else
        {
            transform.localScale = new Vector3(initialScale.x, initialScale.y, initialScale.z);
        }
        rb.AddForce(new Vector2(jumpDirection.x * moveSpeed, jumpForce));
    }
    private void OnDrawGizmos()
    {
        if (groundCheckPoint != null)
        {
            Gizmos.color = isGrounded ? Color.green : Color.red;
            Gizmos.DrawLine(groundCheckPoint.position, groundCheckPoint.position + Vector3.down * groundCheckDistance);
        }
    }
}
