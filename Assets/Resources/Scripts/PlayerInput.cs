using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Unity UI를 사용하기 위한 네임스페이스 추가

public class PlayerInput : MonoBehaviour
{
    public GameObject KeyboardPrefab; // 투사체 프리팹을 연결할 변수
    public float maxThrowForce = 10f; // 투사체가 가질 수 있는 최대 힘
    private float currentThrowForce = 0f; // 현재 투사체의 힘

    // Slider UI 연결을 위한 변수
    public Slider throwSlider;
    private bool isCharging = true;

    private void Start()
    {
        // 게이지 초기화
        throwSlider.value = 0f;
    }

    private void Update()
    {
        // 마우스 왼쪽 버튼을 누르고 있는 동안 게이지를 채우기
        if (Input.GetMouseButton(0) && isCharging == true)
        {
            ChargeThrow();
            if (currentThrowForce >= maxThrowForce)
            {
                ThrowProjectile();
                isCharging = false;
            }
        }
        // 마우스 왼쪽 버튼을 놓으면 투사체를 던짐
        else if (Input.GetMouseButtonUp(0))
        {
            if (isCharging == true)
            {
                ThrowProjectile();
            }
            else
            {
                isCharging = true;
            }        }
    }

    void ChargeThrow()
    {
        // 힘의 크기를 게이지로 조절
        currentThrowForce = Mathf.Clamp(currentThrowForce + Time.deltaTime * maxThrowForce, 0f, maxThrowForce);

        // Slider UI에 현재 게이지 값 반영
        throwSlider.value = currentThrowForce / maxThrowForce;
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
        throwSlider.value = 0f;
    }
}
