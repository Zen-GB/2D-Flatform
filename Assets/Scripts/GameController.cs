using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    Vector2 checkPointPos;
    Rigidbody2D rb;

    CameraController cameraController;
    public ParticleController particleController;

    Quaternion playerRotation;
    MovementCotroller movementCotroller;

    AudioManager audioManager;


    private void Awake()
    {
        movementCotroller = GetComponent<MovementCotroller>();
        cameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        rb = GetComponent<Rigidbody2D>();
        particleController = GetComponent<ParticleController>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
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

    public void UpdateCheckPoint(Vector2 pos)
    {
        checkPointPos = pos;
        playerRotation = transform.rotation;
    }
    void Die()
    {
        //particleController.PlayParticle(ParticleController.Particles.die, transform.position);
        audioManager.PlaySFX(audioManager.death);
        StartCoroutine(Respawn(1f));
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
