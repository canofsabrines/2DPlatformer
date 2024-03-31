using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerPlayer : MonoBehaviour
{
    public float speed = 4.5f;
    public float jumpForce = 12.0f;
    public GameObject bossPrefab;
    public GameObject goldPlatformPrefab;

    private BoxCollider2D box;
    private Rigidbody2D body;
    private Animator anim;
    bool gameWon = false;
    bool dead = false;
    int coinCount = 0;
    int enemiesKilled = 0;
    bool hasBossSpawned = false;

    void OnEnable()
    {
        Messenger.AddListener(GameEvent.ENEMY_HIT, OnEnemyHit);
        Messenger.AddListener(GameEvent.GAME_WIN, OnGameWin);
        Messenger.AddListener(GameEvent.PLAYER_DEATH, OnPlayerDeath);
        Messenger.AddListener(GameEvent.COIN_OBTAINED, OnCoinObtained);
    }
    void OnDisable()
    {
        Messenger.RemoveListener(GameEvent.ENEMY_HIT, OnEnemyHit);
        Messenger.RemoveListener(GameEvent.GAME_WIN, OnGameWin);
        Messenger.RemoveListener(GameEvent.PLAYER_DEATH, OnPlayerDeath);
        Messenger.RemoveListener(GameEvent.COIN_OBTAINED, OnCoinObtained);
    }

    void Start() {
        box = GetComponent<BoxCollider2D>();
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void OnGameWin()
    {
        gameWon = true;
    }

    private void OnPlayerDeath()
    {
        dead = true;
    }

    private void OnEnemyHit()
    {
        body.AddForce(Vector2.up * (jumpForce + 6), ForceMode2D.Impulse);
        if (enemiesKilled < 3)
        {
            ++enemiesKilled;
        }
    }

    private void OnCoinObtained()
    {
        ++coinCount;
        if (coinCount == 10)
        {
            Vector3 platformPos = new Vector3(40.072f, 3.12f, 0);
            Instantiate(goldPlatformPrefab, platformPos, transform.rotation);
        }
    }

    void Update() {

        if (gameWon || dead)
        {
            this.enabled = false;
            anim.SetFloat("speed", 0);
        }
        float deltaX = Input.GetAxis("Horizontal") * speed;
        Vector2 movement = new Vector2(deltaX, body.velocity.y);
        body.velocity = movement;

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
        body.gravityScale = (grounded && Mathf.Approximately(deltaX, 0)) ? 0 : 1;
        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        MovingPlatform platform = null;
        if (hit != null)
        {
            platform = hit.GetComponent<MovingPlatform>();
        }
        if (platform != null)
        {
            transform.parent = platform.transform;
        }
        else
        {
            transform.parent = null;
        }
        if (!gameWon)
        {
            anim.SetFloat("speed", Mathf.Abs(deltaX));
        }
        
        Vector3 pScale = Vector3.one;
        if (platform != null)
        {
            pScale = platform.transform.localScale;
        }
        if (!Mathf.Approximately(deltaX, 0))
        {
            transform.localScale = new Vector3(Mathf.Sign(deltaX) / pScale.x, 1 / pScale.y, 1);
        }

        if (enemiesKilled == 3 && !hasBossSpawned)
        {
            Vector3 bossPos = transform.position;
            bossPos.x = bossPos.x - 2;
            bossPos.y = 11.878f;
            Instantiate(bossPrefab, bossPos, transform.rotation);
            hasBossSpawned = true;
        }
    }
        
    
    
}