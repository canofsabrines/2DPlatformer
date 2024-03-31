using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlatform : MonoBehaviour
{


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Messenger.Broadcast(GameEvent.PLAYER_DEATH);
            
        }
        Destroy(collision.gameObject);
    }
}
