using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D coinCollider)
    {
        if (coinCollider.CompareTag("Player"))
        {
          
            Destroy(gameObject);
            Messenger.Broadcast(GameEvent.COIN_OBTAINED);
        }
    }
}