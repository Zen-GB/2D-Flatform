using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms;

public class MovementCotroller : MonoBehaviour
{
    Rigidbody2D rb;

    public ParticleController particleController;

    bool btnPressed;

    [SerializeField] int speed;
    float speedMultiplier;
    [Range (1,10)]
    [SerializeField] float acceleration;

    bool isWallTouch;
    public LayerMask wallLayer;
    public Transform wallCheckPoint;

    Vector2 reativeTransform;

    public bool isOnPlatform;
    public Rigidbody2D platformrb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        UpdateReativeTransform();
    }

    private void FixedUpdate()
    {
        UpdateSpeedMultilier();
        float targetSpeed = speed * speedMultiplier * reativeTransform.x;

        if (isOnPlatform)
        {
            rb.velocity = new Vector2(targetSpeed + platformrb.velocity.x, rb.velocity.y);
        }

        else
        {
            rb.velocity = new Vector2(targetSpeed, rb.velocity.y);
        }

        isWallTouch = Physics2D.OverlapBox(wallCheckPoint.position, new Vector2(0.04f, 0.55f), 0, wallLayer);
        if (isWallTouch)
        {
            Flip();
        }
    }

    public void Flip()
    {
        particleController.PlayParticle(ParticleController.Particles.touch, wallCheckPoint.position);
        transform.Rotate(0, 180, 0);
        UpdateReativeTransform();
    }

    public void UpdateReativeTransform()
    {
        reativeTransform = transform.InverseTransformVector(Vector2.one);
    }

    public void Move (InputAction.CallbackContext value)
    {
        if (value.started)
        {
            btnPressed = true;
        }
        else if (value.canceled)
        {
            btnPressed = false;
        }
    }

    void UpdateSpeedMultilier ()
    {
        if (btnPressed && speedMultiplier < 1)
        {
            speedMultiplier += Time.deltaTime*acceleration;
        }
        else if (!btnPressed && speedMultiplier > 0)
        {
            speedMultiplier -= Time.deltaTime*acceleration;
            if (speedMultiplier < 0) speedMultiplier = 0;
        }
    }
}
