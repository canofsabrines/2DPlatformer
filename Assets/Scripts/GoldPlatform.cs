using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldPlatform : MonoBehaviour
{

    bool playerTouch = false;
    public Vector3 finishPos;
    public float speed = 0.2f;
    private Vector3 startPos;
    private float trackPercent = 0;
    private int direction = 1;

    void OnCollisionEnter2D(Collision2D platCollider)
    {
        playerTouch = true;
    }

    void Start()
    {
        startPos = transform.position;
        finishPos = new Vector3(startPos.x, startPos.y + 9.4f, startPos.z);
    }

    void Update()
    {
        if (playerTouch)
        {
            trackPercent += direction * speed * Time.deltaTime;
            float y = (finishPos.y - startPos.y) * trackPercent + startPos.y;
            transform.position = new Vector3(startPos.x, y, startPos.z);
        }
        

        if ((direction == 1 && trackPercent > .9f) || (direction == -1 && trackPercent < .1f))
        {
            this.enabled = false;
        }
    }
}