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
    public Text angleText;

    public float minAngle = 15f;
    public float maxAngle = 75f;

    // 플레이어 체력
    public float playerHealth;
    public float maxHealth = 100f;
    private void Start()
    {
        // 게이지 초기화
        throwSlider.value = 0f;
        playerHealth=maxHealth;

        
    }

    // 체력 감소 처리
    public void TakeDamage(int damage)
    {
        playerHealth -= damage;
        if (playerHealth <= 0)
        {
            Debug.Log("Player is dead.");
            // 플레이어 사망 처리
        }
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
            }
        }
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
        GameObject projectile = Instantiate(KeyboardPrefab, transform.position+ new Vector3(1.3f, 1, 0), Quaternion.identity);

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = (mousePosition - transform.position).normalized;

        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
        projectileRb.AddForce(direction * currentThrowForce, ForceMode2D.Impulse);

        Weapon keyboardScript = projectile.GetComponent<Weapon>();
        keyboardScript.owner = this;
        keyboardScript.isLive=true;
        
        currentThrowForce = 0f;
        throwSlider.value = 0f;
    }

    
    public void ProcessCollision(Collision2D collision, string collisionPoint )
    {
        Debug.Log("PlayerInput에서 충돌 처리");
        Weapon weapon = collision.gameObject.GetComponent<Weapon>();

        // 충돌한 객체에 Weapon 컴포넌트가 있는 경우
        if (weapon != null && weapon.isLive == true && weapon.owner != this)
        {
            weapon.isLive=false;
            // 충돌 시의 속도를 가져옴
            Vector3 collisionVelocity = collision.relativeVelocity;

            // Weapon 컴포넌트의 CalculateDamage 함수를 호출하여 데미지를 계산
            int damage = weapon.CalculateDamage(collisionPoint, collisionVelocity);

            TakeDamage(damage);
            Debug.Log(collisionVelocity + "===== collisionVelocity");
            Debug.Log(damage + "===== damage");
        }
    }

    
}


