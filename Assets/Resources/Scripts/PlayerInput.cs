using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Unity UI를 사용하기 위한 네임스페이스 추가

public class PlayerInput : MonoBehaviour
{
    public GameObject KeyboardPrefab; // 투사체 프리팹을 연결할 변수
    public float maxThrowForce = 5f; // 투사체가 가질 수 있는 최대 힘
    private float currentThrowForceMeemee = 0f;
    private float currentThrowForceRokrok = 0f;

    // Slider UI 연결을 위한 변수
    public Slider throwSlider1;
    public Slider throwSlider2;
    private bool isCharging = true;

    // 긴 점선 표시를 위한 LineRenderer 컴포넌트
    public LineRenderer lineRenderer1;
    public LineRenderer lineRenderer2;

    public float minAngle = 15f;
    public float maxAngle = 75f;
    private bool isMouseClicked = false;

    // 플레이어 번갈아가며 행동하기 위한 변수
    private int currentPlayer = 0;

    // 두 개의 캐릭터를 할당할 변수
    public GameObject meemee;
    public GameObject rokrok;

    // 각 캐릭터의 마우스 클릭 상태를 저장할 변수
    private bool isMouseClickedMeemee = false;
    private bool isMouseClickedRokrok = false;

    private void Start()
    {
        // 게이지 초기화
        throwSlider1.value = 0f;

        // 긴 점선의 시작과 끝 지점 설정 (초기에는 캐릭터 위치에서 캐릭터 위치로 설정)
        lineRenderer1.SetPositions(new Vector3[] { transform.position, transform.position });

        // 라인 렌더러를 점선으로 표시하기 위해 머티리얼 설정
        lineRenderer1.material = new Material(Shader.Find("Sprites/Default"));
        // 게이지 초기화
        throwSlider2.value = 0f;

        // 긴 점선의 시작과 끝 지점 설정 (초기에는 캐릭터 위치에서 캐릭터 위치로 설정)
        lineRenderer2.SetPositions(new Vector3[] { transform.position, transform.position });

        // 라인 렌더러를 점선으로 표시하기 위해 머티리얼 설정
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

        // 플레이어 번갈아가며 행동
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

    // 플레이어 행동 함수
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

    // 기존 코드와 동일하게 구현
    void ChargeThrow(Slider slider, ref float currentThrowForce)
    {
        // 힘의 크기를 게이지로 조절
        currentThrowForce = Mathf.Clamp(currentThrowForce + Time.deltaTime * maxThrowForce, 0f, maxThrowForce);

        // Slider UI에 현재 게이지 값 반영
        slider.value = currentThrowForce / maxThrowForce;

    }

    // 기존 코드와 동일하게 구현
    void ThrowProjectile(GameObject player, Slider slider, LineRenderer lineRenderer, ref float currentThrowForce)
    {
        // 투사체를 생성하고 캐릭터가 보는 방향으로 던짐
        GameObject projectile = Instantiate(KeyboardPrefab, player.transform.position, Quaternion.identity);

        Vector3 direction;

        if (isMouseClicked) // 마우스를 클릭한 상태라면
        {
            // 마우스 클릭한 시점의 각도를 유지하여 발사
            direction = (lineRenderer.GetPosition(1) - player.transform.position).normalized;
        }
        else
        {
            // 마우스를 클릭하지 않은 경우 기존과 동일하게 발사 각도 계산
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            direction = (mousePosition - player.transform.position).normalized;
        }

        // 투사체에 힘을 가해 던짐
        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
        projectileRb.AddForce(direction * currentThrowForce, ForceMode2D.Impulse);

        // 게이지 초기화
        currentThrowForce = 0f;
        slider.value = 0f;
    }

    // 기존 코드와 동일하게 구현
    void UpdateLineRenderer(GameObject player, LineRenderer lineRenderer)
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = (mousePosition - player.transform.position).normalized;

        // 마우스가 범위 안에 있는 경우 점선의 끝 지점 설정 (마우스 방향으로 최대 거리까지 설정)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle = Mathf.Clamp(angle, minAngle, maxAngle);
        Vector3 endPoint = player.transform.position + Quaternion.Euler(0f, 0f, angle) * Vector3.right * 6f;

        // LineRenderer의 점선 표시 업데이트
        lineRenderer.SetPositions(new Vector3[] { player.transform.position, endPoint });
    }
}