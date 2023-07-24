using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Unity UI�� ����ϱ� ���� ���ӽ����̽� �߰�

public class PlayerInput : MonoBehaviour
{
    public GameObject KeyboardPrefab; // ����ü �������� ������ ����
    public float maxThrowForce = 5f; // ����ü�� ���� �� �ִ� �ִ� ��
    private float currentThrowForceMeemee = 0f;
    private float currentThrowForceRokrok = 0f;

    // Slider UI ������ ���� ����
    public Slider throwSlider1;
    public Slider throwSlider2;
    private bool isCharging = true;

    // �� ���� ǥ�ø� ���� LineRenderer ������Ʈ
    public LineRenderer lineRenderer1;
    public LineRenderer lineRenderer2;

    public float minAngle = 15f;
    public float maxAngle = 75f;
    private bool isMouseClicked = false;

    // �÷��̾� �����ư��� �ൿ�ϱ� ���� ����
    private int currentPlayer = 0;

    // �� ���� ĳ���͸� �Ҵ��� ����
    public GameObject meemee;
    public GameObject rokrok;

    // �� ĳ������ ���콺 Ŭ�� ���¸� ������ ����
    private bool isMouseClickedMeemee = false;
    private bool isMouseClickedRokrok = false;

    private void Start()
    {
        // ������ �ʱ�ȭ
        throwSlider1.value = 0f;

        // �� ������ ���۰� �� ���� ���� (�ʱ⿡�� ĳ���� ��ġ���� ĳ���� ��ġ�� ����)
        lineRenderer1.SetPositions(new Vector3[] { transform.position, transform.position });

        // ���� �������� �������� ǥ���ϱ� ���� ��Ƽ���� ����
        lineRenderer1.material = new Material(Shader.Find("Sprites/Default"));
        // ������ �ʱ�ȭ
        throwSlider2.value = 0f;

        // �� ������ ���۰� �� ���� ���� (�ʱ⿡�� ĳ���� ��ġ���� ĳ���� ��ġ�� ����)
        lineRenderer2.SetPositions(new Vector3[] { transform.position, transform.position });

        // ���� �������� �������� ǥ���ϱ� ���� ��Ƽ���� ����
        lineRenderer2.material = new Material(Shader.Find("Sprites/Default"));
    
    
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            if (currentPlayer == 1)
                isMouseClickedMeemee = true;
            else if (currentPlayer == 0)
                isMouseClickedRokrok = true;

            currentPlayer = (currentPlayer + 1) % 2;
        }

        // �÷��̾� �����ư��� �ൿ
        if (currentPlayer == 1)
        {
            PlayerAction(meemee, throwSlider1, lineRenderer1, isMouseClickedMeemee, ref currentThrowForceMeemee);
        }
        else if (currentPlayer == 0)
        {
            PlayerAction(rokrok, throwSlider2, lineRenderer2, isMouseClickedRokrok, ref currentThrowForceRokrok);
        }

        if (!isMouseClicked)
        {
            if (currentPlayer == 0)
            {
                UpdateLineRenderer(meemee, lineRenderer1);
            }
            else if (currentPlayer == 1)
            {
                UpdateLineRenderer(rokrok, lineRenderer2);
            }
        }

    }

    // �÷��̾� �ൿ �Լ�
    private void PlayerAction(GameObject player, Slider slider, LineRenderer lineRenderer, bool isMouseClicked, ref float currentThrowForce)
    {
        if (Input.GetMouseButton(0) && isCharging == true && isMouseClicked)
        {
            ChargeThrow(slider, ref currentThrowForce);
            if (currentThrowForce >= maxThrowForce)
            {
                ThrowProjectile(player, slider, lineRenderer, ref currentThrowForce);
                isCharging = false;

            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (isCharging == true)
            {
                ThrowProjectile(player, slider, lineRenderer, ref currentThrowForce);
            }
            else
            {
                isCharging = true;
            }
            isMouseClicked = false;
            currentThrowForce = 0f;
        }
    }

    // ���� �ڵ�� �����ϰ� ����
    void ChargeThrow(Slider slider, ref float currentThrowForce)
    {
        // ���� ũ�⸦ �������� ����
        currentThrowForce = Mathf.Clamp(currentThrowForce + Time.deltaTime * maxThrowForce, 0f, maxThrowForce);

        // Slider UI�� ���� ������ �� �ݿ�
        slider.value = currentThrowForce / maxThrowForce;

    }

    // ���� �ڵ�� �����ϰ� ����
    void ThrowProjectile(GameObject player, Slider slider, LineRenderer lineRenderer, ref float currentThrowForce)
    {
        // ����ü�� �����ϰ� ĳ���Ͱ� ���� �������� ����
        GameObject projectile = Instantiate(KeyboardPrefab, player.transform.position, Quaternion.identity);

        Vector3 direction;

        if (isMouseClicked) // ���콺�� Ŭ���� ���¶��
        {
            // ���콺 Ŭ���� ������ ������ �����Ͽ� �߻�
            direction = (lineRenderer.GetPosition(1) - player.transform.position).normalized;
        }
        else
        {
            // ���콺�� Ŭ������ ���� ��� ������ �����ϰ� �߻� ���� ���
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            direction = (mousePosition - player.transform.position).normalized;
        }

        // ����ü�� ���� ���� ����
        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
        projectileRb.AddForce(direction * currentThrowForce, ForceMode2D.Impulse);

        // ������ �ʱ�ȭ
        currentThrowForce = 0f;
        slider.value = 0f;
    }

    // ���� �ڵ�� �����ϰ� ����
    void UpdateLineRenderer(GameObject player, LineRenderer lineRenderer)
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = (mousePosition - player.transform.position).normalized;

        // ���콺�� ���� �ȿ� �ִ� ��� ������ �� ���� ���� (���콺 �������� �ִ� �Ÿ����� ����)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle = Mathf.Clamp(angle, minAngle, maxAngle);
        Vector3 endPoint = player.transform.position + Quaternion.Euler(0f, 0f, angle) * Vector3.right * 6f;

        // LineRenderer�� ���� ǥ�� ������Ʈ
        lineRenderer.SetPositions(new Vector3[] { player.transform.position, endPoint });
    }
}