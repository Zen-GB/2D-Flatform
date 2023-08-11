using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    Vector2 checkPointPos;
    Rigidbody2D rb;

    CameraController cameraController;
    public ParticleController particleController;

    UnityEngine.Quaternion playerRotation;
    MovementCotroller movementCotroller;


    private void Awake()
    {
        movementCotroller = GetComponent<MovementCotroller>();
        cameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start ()
    {
        checkPointPos = transform.position;
        playerRotation = transform.rotation;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            Die();
        }
    }

    public void UpdateCheckPointPos(Vector2 pos)
    {
        checkPointPos = pos;
        playerRotation = transform.rotation;
    }
    void Die()
    {
        particleController.PlayParticle(ParticleSystem.Particle.die, transform.position);
        StartCoroutine(Respawn(0.05f));
    }

    IEnumerator Respawn(float duration)
    {
        rb.velocity = new Vector2(0, 0);
        rb.simulated = false;
        transform.localScale = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(duration);
        transform.position = checkPointPos;
        transform.rotation = playerRotation;
        transform.localScale = new Vector3(1, 1, 1);
        rb.simulated = true;
        movementCotroller.UpdateReativeTransform();
    }
}
