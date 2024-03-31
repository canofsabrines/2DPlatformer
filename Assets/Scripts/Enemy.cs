using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Vector3 startPos;
    public Vector3 finishPos;
    public float speed = 0.5f;
    private float trackPercent = 0;
    private int direction = 1;
    public bool alive = true;





    private BoxCollider2D box;
    private Rigidbody2D body; // TEMP
    private Animator anim;

    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        body = GetComponent<Rigidbody2D>(); // TEMP
        anim = GetComponent<Animator>();
        startPos = transform.position;
        finishPos = new Vector3(startPos.x, startPos.y - 20, startPos.z);
    }

    void OnTriggerEnter2D(Collider2D enemyCollider)
    {
        if (enemyCollider.CompareTag("Player"))
        {
            BoxCollider2D playerBox = enemyCollider.GetComponent<BoxCollider2D>();
            Vector3 max = box.bounds.max;
            Vector3 min = box.bounds.min;



            if (playerBox.bounds.min.y > (min.y + (0.75 * (max.y - min.y))))
            {
                Messenger.Broadcast(GameEvent.ENEMY_HIT);
                alive = false;
            }
            else
            {
                Destroy(enemyCollider.gameObject);
            }
        }
        

    }

    void Update()
    {
        Vector3 max = box.bounds.max;
        Vector3 min = box.bounds.min;



        Vector2 corner1 = new Vector2(max.x, min.y); // TEMP
        Vector2 corner2 = new Vector2(min.x, min.y); // TEMP
                                                     //  Vector2 corner1 = new Vector2(max.x, min.y - .1f);
                                                     //  Vector2 corner2 = new Vector2(min.x, min.y - .2f);
        Collider2D hit = Physics2D.OverlapArea(corner1, corner2); // TEMP
        bool grounded = false; // TEMP
        if (hit != null) // WHOLE FUNCTION HERE IS TEMP
        {
            grounded = true;
        }
        if (!alive)
        {
            anim.SetBool("death", true);
            trackPercent += direction * speed * Time.deltaTime;
            float y = (finishPos.y - startPos.y) * trackPercent + startPos.y;
            transform.position = new Vector3(startPos.x, y, startPos.z);

        }
    }
}

    