using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class PlayerController : MonoBehaviour
{
   
    public AudioClip deathClip;
    public float jumpForce = 700f;
    private int  jumpCount = 0;
    private bool isGround = false;
    private bool isDead = false;
    public float moveSpeed = 5f;

    private Rigidbody2D playerRigidbody;
    private Animator animator;
    private AudioSource playerAudio;
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
    }
    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        if (isDead)
        {
            //사망 시 처리를 더 이상 진행하지 않고 종료
            return;
        }
        playerRigidbody.velocity = new Vector2(moveInput * moveSpeed, playerRigidbody.velocity.y);
        if (moveInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        animator.SetBool("Run", moveInput != 0);

        if (Input.GetButtonDown("Jump") && jumpCount < 2)
        {
            playerRigidbody.AddForce(Vector2.up * jumpForce);
            jumpCount++;
            playerAudio.Play();
        }
        else if (Input.GetButtonUp("Jump") && playerRigidbody.velocity.y > 0)
        {
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, playerRigidbody.velocity.y * 0.5f);
        }
        animator.SetBool("Grounded", isGround);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isGround = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y>0.7f)
        {
            isGround = true;
            jumpCount = 0;
        }
       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "DeadZone" && !isDead)
        {
            Die();
        }
        else if (other.tag == "Enemy"&&!isDead)
        {
            if (isGround)
            {
                // 적을 밟았을 때
                Destroy(other.gameObject);
            }
            else
            {
                // 적과 충돌했을 때
                Die();
            }
        }
    }

    private void Die()
    {
        animator.SetTrigger("Die");

        playerAudio.clip = deathClip;
        playerAudio.Play();
        playerRigidbody.velocity = Vector2.zero;
        isDead = true;
        animator.SetBool("Dead", true);
        GameManager.instance.OnPlayerDead();
    }
    
}
