using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartCollision : MonoBehaviour
{
    public PlayerInput player;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        string collisionPoint=gameObject.tag;
        Debug.Log(collisionPoint+"=== 자식");
        player.ProcessCollision(collision,collisionPoint);
    }
}
