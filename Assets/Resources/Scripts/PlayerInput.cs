using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerInput : MonoBehaviour
{

    public GameObject KeyboardPrefab;                  
    public float maxThrowForce = 5f; 
    private float currentThrowForceMeemee = 0f;
    private float currentThrowForceRokrok = 0f;
    
    public Slider throwSlider1;
    public Slider throwSlider2;

    private bool isCharging = true;

    public LineRenderer lineRenderer1;
    public LineRenderer lineRenderer2;

    private bool isMouseClicked = false;
          
    private int currentPlayer = 0;
  
    public GameObject meemee;
    public GameObject rokrok;
           
    private bool isMouseClickedMeemee = false;
    private bool isMouseClickedRokrok = false;

    public float playerHealth;
    public float maxHealth = 100f;

    public Vector3 throwPosition;

    public float windStrength;
    public bool windDirection;

    private void Start()
    {
        playerHealth = maxHealth;

        throwSlider1.value = 0f;

        lineRenderer1.SetPositions(new Vector3[] { transform.position, transform.position });

        lineRenderer1.material = new Material(Shader.Find("Sprites/Default"));
        
        throwSlider2.value = 0f;

        lineRenderer2.SetPositions(new Vector3[] { transform.position, transform.position });
        
        lineRenderer2.material = new Material(Shader.Find("Sprites/Default"));

        lineRenderer2.enabled = false;

        windStrength = Random.Range(10,500);
        windDirection = (Random.value > 0.5f);

        if (meemee != null)
            throwPosition = new Vector3(1.3f, 1, 0);
        else throwPosition = new Vector3(-1.3f, 1, 0);
    }

    public void TakeDamage(int damage)
    {
        playerHealth -= damage;
        if (playerHealth <= 0)
        {
            Debug.Log("Player is dead.");
            SceneManager.LoadScene("GameOver");
        }

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (currentPlayer % 2 == 1)
                isMouseClickedMeemee = true;
            else if (currentPlayer % 2 == 0)
            {
                isMouseClickedRokrok = true;
                if (currentPlayer == 0)
                {
                    isMouseClickedMeemee = true;
                }
            }

            currentPlayer = currentPlayer + 1;
        }

        if (currentPlayer % 2 == 1)
        {
            PlayerAction(meemee, throwSlider1, lineRenderer1, isMouseClickedMeemee, ref currentThrowForceMeemee);
        }
        else if (currentPlayer % 2 == 0)
        {
            PlayerAction(rokrok, throwSlider2, lineRenderer2, isMouseClickedRokrok, ref currentThrowForceRokrok);
        }

        if (!isMouseClicked)
        {
            if (currentPlayer % 2 == 0)
            {
                UpdateLineRenderer(meemee, lineRenderer1);
            }
            else if (currentPlayer % 2 == 1)
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
            windStrength = Random.Range(10,500);
            windDirection = (Random.value > 0.5f);
        }
    }
  
    void ChargeThrow(Slider slider, ref float currentThrowForce)
    {
        currentThrowForce = Mathf.Clamp(currentThrowForce + Time.deltaTime * maxThrowForce, 0f, maxThrowForce);
        slider.value = currentThrowForce / maxThrowForce;

    }
 
    void ThrowProjectile(GameObject player, Slider slider, LineRenderer lineRenderer, ref float currentThrowForce)
    {
        GameObject projectile = Instantiate(KeyboardPrefab, player.transform.position, Quaternion.identity);

        Vector3 direction = (lineRenderer.GetPosition(1) - player.transform.position).normalized;

        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
        float windForce = windDirection ? windStrength : -windStrength;
        Debug.Log(windForce + "windforce");
        Vector3 force = direction * currentThrowForce + new Vector3(windForce, 0, 0);
        
        Debug.Log(force + "force");
        projectileRb.AddForce(force, ForceMode2D.Impulse);

        Weapon keyboardScript = projectile.GetComponent<Weapon>();
        keyboardScript.owner = this;
        keyboardScript.isLive=true;

        currentThrowForce = 0f;
        slider.value = 0f;
        lineRenderer.enabled = false;
    }
 
    void UpdateLineRenderer(GameObject player, LineRenderer lineRenderer)
    {
        lineRenderer.enabled = true;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = (mousePosition - player.transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (player == meemee)
        {
            if (15f < angle && angle < 75f)
            {
                Vector3 endPoint = player.transform.position + Quaternion.Euler(0f, 0f, angle) * Vector3.right * 6f;

                lineRenderer.SetPositions(new Vector3[] { player.transform.position, endPoint });
            }
        }
        else
        {
            if (105f < angle && angle < 165f)
            {
                Vector3 endPoint = player.transform.position + Quaternion.Euler(0f, 0f, angle) * Vector3.right * 6f;

                lineRenderer.SetPositions(new Vector3[] { player.transform.position, endPoint });
            }
        }

    }

    public void ProcessCollision(Collision2D collision, string collisionPoint)
    {
        Debug.Log("PlayerInput에서 충돌 처리");
        Weapon weapon = collision.gameObject.GetComponent<Weapon>();
        
        Debug.Log(weapon);
        Debug.Log(weapon.owner+ "owner");
        Debug.Log(weapon.isLive);

        if (weapon != null && weapon.isLive == true && weapon.owner != this)
        {
            
            weapon.isLive = false;
            Vector3 collisionVelocity = collision.relativeVelocity;

            int damage = weapon.CalculateDamage(collisionPoint, collisionVelocity);

            TakeDamage(damage);
            Debug.Log(collisionVelocity + "===== collisionVelocity");
            Debug.Log(damage + "===== damage");
        }
    }


}