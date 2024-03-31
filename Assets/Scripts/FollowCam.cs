using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.2f;
    private Vector3 velocity = Vector3.zero;
    void LateUpdate()
    {

        if (target != null)
        {
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
            if (!(target.position.y < -1.35))
            {
                transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
            }
            else
            {
                this.enabled = false;
            }
        }
        

       
    }
}
