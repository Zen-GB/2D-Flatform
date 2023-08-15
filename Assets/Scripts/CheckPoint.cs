using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    GameController gameController;
    public Transform respawnPoint;

    SpriteRenderer spriteRenderer;
    public Sprite active, passive;
    Collider2D coll;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        gameController = GameObject.FindGameObjectWithTag("Player").GetComponent<GameController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        coll = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            audioManager.PlaySFX(audioManager.checkPoint);
            gameController.UpdateCheckPoint(respawnPoint.position);
            spriteRenderer.sprite = active;
            coll.enabled = false;
        }
    }
}
