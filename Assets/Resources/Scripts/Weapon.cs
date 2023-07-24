using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float headDamage = 15f; 
    public float bodyDamage = 10f; 
    public PlayerInput owner;
    public float headHitThreshold = 0.1f;
    public bool isLive;

    // void Throw(GameObject projectile){
    //     Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
    //     projectileRb.AddForce(direction * currentThrowForce, ForceMode2D.Impulse);
    // }

    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     PlayerInput player = collision.GetComponent<PlayerInput>();
        
    //     if (player != null && player != owner)
    //     {
    //         float playerTop = player.GetComponent<Renderer>().bounds.max.y;
    //         if (Mathf.Abs(this.transform.position.y - playerTop) < headHitThreshold)
    //         {
    //             player.TakeDamage(headDamage);
    //         }
    //         else
    //         {
    //             player.TakeDamage(bodyDamage);
    //         }
    //     }
    //}

    public int CalculateDamage(string collisionPoint, Vector3 collisionVelocity)
    {
        int damage = 0;
        int collisionWeight = Mathf.FloorToInt(collisionVelocity.magnitude);
        switch (collisionPoint){
            case "Head":
                // 머리에 맞았을 경우의 데미지
                damage= Mathf.RoundToInt(collisionWeight*1.5f);
                break;
            case "Body":
                // 몸에 맞았을 경우의 데미지
                damage = collisionWeight;
                break;
            default:
                Debug.Log("Tag Error");
                break;
        }
        
        return damage;
    }
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(DestroyAfterSeconds(4f));
    }

    IEnumerator DestroyAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
