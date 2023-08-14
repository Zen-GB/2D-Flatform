using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed;
    Vector3 targetPos;

    MovementCotroller movementCotroller;
    Rigidbody2D rb;
    Vector3 moveDirection;

    Rigidbody2D playerRb;

    //Moving Platform Ways
    public GameObject ways;
    public Transform[] wayPoints;
    int pointIndex;
    int pointCount;
    int direction = 1;

    public float waitDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        movementCotroller = GameObject.FindGameObjectWithTag("Player").GetComponent<MovementCotroller>();
        playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        //Moving Platform Ways
        wayPoints = new Transform[ways.transform.childCount];
        for (int i = 0; i < ways.gameObject.transform.childCount; i++)
        {
            wayPoints[i] = ways.transform.GetChild(i).gameObject.transform;
        }
    }

    private void Start()
    {
        pointIndex = 1;
        pointCount = wayPoints.Length;
        targetPos = wayPoints[1].transform.position;
        DirectionCaculate();
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, targetPos) < 0.05f)
        {
            NextPoint();
        }
    }

    void NextPoint()
    {
        transform.position = targetPos;
        moveDirection = Vector3.zero;
        if (pointIndex == pointCount -1) //Arrived last point
        {
            direction = -1;
        }
        if (pointIndex == 0) //Arrived first point
        {
            direction = 1;
        }
        pointIndex += direction;
        targetPos = wayPoints[pointIndex].transform.position;

        StartCoroutine(WaitNextPoint());
    }

    IEnumerator WaitNextPoint ()
    {
        yield return new WaitForSeconds(waitDirection);
        DirectionCaculate();
    }

    private void FixedUpdate()
    {
        rb.velocity = moveDirection * speed;
    }

    void DirectionCaculate ()
    {
        moveDirection = (targetPos - transform.position).normalized;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            movementCotroller.isOnPlatform = true;
            movementCotroller.platformrb = rb;
            playerRb.gravityScale = playerRb.gravityScale * 50;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            movementCotroller.isOnPlatform = false;
            playerRb.gravityScale = playerRb.gravityScale / 50;
        }
    }
}
