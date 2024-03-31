using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private Vector3 startPos;
    public Vector3 finishPos;
    public float speed = 0.7f;
    public bool alive = true;
    int direction = 1;
    
    int hitsTaken = 0;



    private BoxCollider2D box;
    private Rigidbody2D body;
    private Animator anim;

    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
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



            Vector2 corner1 = new Vector2(max.x, min.y);
            Vector2 corner2 = new Vector2(min.x, min.y);

            if (playerBox.bounds.min.y > (min.y + (0.75 * (max.y - min.y))))
            {
                Messenger.Broadcast(GameEvent.ENEMY_HIT); 
                anim.SetTrigger("isHit");
                ++hitsTaken;
                if (hitsTaken == 3) {
                    Messenger.Broadcast(GameEvent.GAME_WIN);
                    Destroy(gameObject);
                    
                }
                
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
        Vector2 corner1 = new Vector2(max.x, min.y - .1f);
        Vector2 corner2 = new Vector2(min.x, min.y - .2f);
        


        Collider2D hit = Physics2D.OverlapArea(corner1, corner2);
        bool grounded = false;
        if (hit != null)
        {
            grounded = true;
        }
        





        if (Random.Range(0f, 200.0f) <= 1f)
        {
            direction *= -1;
        }
        if (Random.Range(0f, 1000.0f) <= 1f && grounded)
        {
            //jump code here

        }
        if (startPos.x - 4 > transform.position.x || startPos.x + 4 < transform.position.x)
        {
            direction *= -1;
        }
        transform.Translate(speed * Time.deltaTime * direction, 0, 0);

    }
    
}
