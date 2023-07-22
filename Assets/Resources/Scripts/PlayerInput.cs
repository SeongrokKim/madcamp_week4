using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public GameObject KeyboardPrefab; // 투사체 프리팹을 연결할 변수

    public float maxThrowForce = 10f; // 투사체가 가질 수 있는 최대 힘
    private float currentThrowForce = 0f; // 현재 투사체의 힘

    private void Update()
    {
        // 마우스 왼쪽 버튼을 누르고 있는 동안 게이지를 채우기
        if (Input.GetMouseButton(0))
        {
            ChargeThrow();
        }
        // 마우스 왼쪽 버튼을 놓으면 투사체를 던짐
        else if (Input.GetMouseButtonUp(0))
        {
            ThrowProjectile();
        }
    }

    void ChargeThrow()
    {
        // 힘의 크기를 게이지로 조절
        currentThrowForce = Mathf.Clamp(currentThrowForce + Time.deltaTime * maxThrowForce, 0f, maxThrowForce);
    }

    void ThrowProjectile()
    {
        // 투사체를 생성하고 캐릭터가 보는 방향으로 던짐
        GameObject projectile = Instantiate(KeyboardPrefab, transform.position, Quaternion.identity);

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = (mousePosition - transform.position).normalized;

        // 투사체에 힘을 가해 던짐
        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
        projectileRb.AddForce(direction * currentThrowForce, ForceMode2D.Impulse);

        // 게이지 초기화
        currentThrowForce = 0f;
    }
}
