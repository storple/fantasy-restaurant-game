using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Component References")]
    [SerializeField] Rigidbody2D rb;

    [Header("Player Settings")]
    [SerializeField] float moveSpeed = 3f;

    [Header("Sprites")]
    public Sprite forwardSprite;
    public Sprite backwardSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;

    private SpriteRenderer spriteRenderer;
    private Vector2 movement;

    private PlayerControls controls;

    void Awake()
    {
        controls = new PlayerControls();

        controls.Player.Move.performed += ctx => movement = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => movement = Vector2.zero;
    }

    void OnEnable() => controls.Enable();
    void OnDisable() => controls.Disable();

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (movement.x > 0)
            spriteRenderer.sprite = rightSprite;
        else if (movement.x < 0)
            spriteRenderer.sprite = leftSprite;
        else if (movement.y > 0)
            spriteRenderer.sprite = backwardSprite;
        else if (movement.y < 0)
            spriteRenderer.sprite = forwardSprite;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}