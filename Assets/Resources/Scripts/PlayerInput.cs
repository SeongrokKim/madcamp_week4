using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Unity UI�� ����ϱ� ���� ���ӽ����̽� �߰�

public class PlayerInput : MonoBehaviour
{
    public GameObject KeyboardPrefab; // ����ü �������� ������ ����
    public float maxThrowForce = 10f; // ����ü�� ���� �� �ִ� �ִ� ��
    private float currentThrowForce = 0f; // ���� ����ü�� ��

    // Slider UI ������ ���� ����
    public Slider throwSlider;
    private bool isCharging = true;

    private void Start()
    {
        // ������ �ʱ�ȭ
        throwSlider.value = 0f;
    }

    private void Update()
    {
        // ���콺 ���� ��ư�� ������ �ִ� ���� �������� ä���
        if (Input.GetMouseButton(0) && isCharging == true)
        {
            ChargeThrow();
            if (currentThrowForce >= maxThrowForce)
            {
                ThrowProjectile();
                isCharging = false;
            }
        }
        // ���콺 ���� ��ư�� ������ ����ü�� ����
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
        // ���� ũ�⸦ �������� ����
        currentThrowForce = Mathf.Clamp(currentThrowForce + Time.deltaTime * maxThrowForce, 0f, maxThrowForce);

        // Slider UI�� ���� ������ �� �ݿ�
        throwSlider.value = currentThrowForce / maxThrowForce;
    }

    void ThrowProjectile()
    {
        // ����ü�� �����ϰ� ĳ���Ͱ� ���� �������� ����
        GameObject projectile = Instantiate(KeyboardPrefab, transform.position, Quaternion.identity);

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = (mousePosition - transform.position).normalized;

        // ����ü�� ���� ���� ����
        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
        projectileRb.AddForce(direction * currentThrowForce, ForceMode2D.Impulse);

        // ������ �ʱ�ȭ
        currentThrowForce = 0f;
        throwSlider.value = 0f;
    }
}
