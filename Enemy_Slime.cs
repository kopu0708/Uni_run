using UnityEngine;

public class Enemy_Slime : MonoBehaviour
{
    [Header("Target")]
    public Transform playerTransform;

    [Header("Movement")]
    public float moveSpeed = 10f;
    public float jumpForce = 500f;
    public float jumpInterval = 2f;//2초 마다 점프
    public AudioSource jumpSound;

    [Header("Animation")]
    public Animator animator;

    [Header("Ground Check")]
    public LayerMask groundLayer;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
