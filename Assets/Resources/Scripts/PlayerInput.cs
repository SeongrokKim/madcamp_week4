using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Unity UI를 사용하기 위한 네임스페이스 추가

public class PlayerInput : MonoBehaviour
{

    public GameObject KeyboardPrefab; //     ü                     
    public float maxThrowForce = 5f; //     ü            ִ   ִ    
    private float currentThrowForceMeemee = 0f;
    private float currentThrowForceRokrok = 0f;

    // Slider UI                 
    public Slider throwSlider1;
    public Slider throwSlider2;

    private bool isCharging = true;
    public Text angleText;
    
    public LineRenderer lineRenderer1;
    public LineRenderer lineRenderer2;

    public float minAngle = 15f;
    public float maxAngle = 75f;
    private bool isMouseClicked = false;

    //  ÷  ̾       ư     ൿ ϱ           
    private int currentPlayer = 0;

    //         ĳ   ͸   Ҵ        
    public GameObject meemee;
    public GameObject rokrok;

    //    ĳ          콺 Ŭ      ¸             
    private bool isMouseClickedMeemee = false;
    private bool isMouseClickedRokrok = false;
    
    // 플레이어 체력
    public float playerHealth;
    public float maxHealth = 100f;

    private void Start()
    {
        // 게이지 초기화
        throwSlider.value = 0f;
        playerHealth=maxHealth;
        
        //         ʱ ȭ
        throwSlider1.value = 0f;

        //              ۰               ( ʱ⿡   ĳ       ġ     ĳ       ġ       )
        lineRenderer1.SetPositions(new Vector3[] { transform.position, transform.position });

        //                        ǥ   ϱ         Ƽ         
        lineRenderer1.material = new Material(Shader.Find("Sprites/Default"));
        //         ʱ ȭ
        throwSlider2.value = 0f;

        //              ۰               ( ʱ⿡   ĳ       ġ     ĳ       ġ       )
        lineRenderer2.SetPositions(new Vector3[] { transform.position, transform.position });

        //                        ǥ   ϱ         Ƽ         
        lineRenderer2.material = new Material(Shader.Find("Sprites/Default"));
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
        if (Input.GetMouseButtonDown(0))
        {
            if (currentPlayer == 1)
                isMouseClickedMeemee = true;
            else if (currentPlayer == 0)
                isMouseClickedRokrok = true;

            currentPlayer = (currentPlayer + 1) % 2;
        }

        //  ÷  ̾       ư     ൿ
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

    //       ڵ        ϰ      
    void ChargeThrow(Slider slider, ref float currentThrowForce)
    {
        // 힘의 크기를 게이지로 조절
        currentThrowForce = Mathf.Clamp(currentThrowForce + Time.deltaTime * maxThrowForce, 0f, maxThrowForce);
        // Slider UI                   ݿ 
        slider.value = currentThrowForce / maxThrowForce;

    }

    //       ڵ        ϰ      
    void ThrowProjectile(GameObject player, Slider slider, LineRenderer lineRenderer, ref float currentThrowForce)
    {
        GameObject projectile = Instantiate(KeyboardPrefab, transform.position+ new Vector3(1.3f, 1, 0), Quaternion.identity);

        Vector3 direction;

        if (isMouseClicked) //    콺   Ŭ        ¶  
        {
            //    콺 Ŭ                        Ͽ   ߻ 
            direction = (lineRenderer.GetPosition(1) - player.transform.position).normalized;
        }
        else
        {
            //    콺   Ŭ                            ϰ   ߻          
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            direction = (mousePosition - player.transform.position).normalized;
        }

        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
        projectileRb.AddForce(direction * currentThrowForce, ForceMode2D.Impulse);

        Weapon keyboardScript = projectile.GetComponent<Weapon>();
        keyboardScript.owner = this;
        keyboardScript.isLive=true;
        
        currentThrowForce = 0f;
        slider.value = 0f;
    }

    //       ڵ        ϰ      
    void UpdateLineRenderer(GameObject player, LineRenderer lineRenderer)
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = (mousePosition - player.transform.position).normalized;

        //    콺         ȿ   ִ                          (   콺           ִ   Ÿ          )
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle = Mathf.Clamp(angle, minAngle, maxAngle);
        Vector3 endPoint = player.transform.position + Quaternion.Euler(0f, 0f, angle) * Vector3.right * 6f;

        // LineRenderer        ǥ         Ʈ
        lineRenderer.SetPositions(new Vector3[] { player.transform.position, endPoint });
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
