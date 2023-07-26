using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerInput : MonoBehaviour
{
    public GameObject KeyboardPrefab;
    public GameObject MousePrefab;
    public float maxThrowForce = 5f;
    public float currentThrowForceMeemee = 0f;
    public float currentThrowForceRokrok = 0f;

    public Slider throwSlider1;
    public Slider throwSlider2;
    public bool isCharging = true;
    public LineRenderer lineRenderer1;
    public LineRenderer lineRenderer2;

    public float minAngle = 15f;
    public float maxAngle = 75f;
    private bool isMouseClicked = false;

    public int currentPlayer = 0;

    public GameObject meemee;
    public GameObject rokrok;

    private bool isMouseClickedMeemee = false;
    private bool isMouseClickedRokrok = false;


    public float playerHealth;
    public float maxHealth = 100f;

    public Vector3 throwPosition;

    public float windStrength;
    public bool windDirection;

    public bool useDoubleThrowItem = false;

    public static PlayerInput Instance { get; private set; }
    public List<Button> itemButtons = new List<Button>();

    public Button selectedItemButton = null;

    public bool isSelectingItem = false;

    private bool isItemClicked = false;

    public DoubleThrowItem doubleThrowItem;

    public GameObject itemButtonPrefab;
    public float speed = 5f; // �̵� �ӵ�
    public float minX = -1f; // x������ �̵��� �� �ִ� �ּҰ�
    public float maxX = 1f; // x������ �̵��� �� �ִ� �ִ밪
    Animator anim;
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public bool IsSelectingItem
    {
        get { return isSelectingItem; }
        set { isSelectingItem = value; }
    }

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

        windStrength = Random.Range(10, 300);
        windDirection = (Random.value > 0.5f);

        Instance = this;
        doubleThrowItem = GetComponent<DoubleThrowItem>();
    }

    public void TakeDamage(int damage)
    {
        if (damage <= 0) return;

        anim.SetTrigger("Hit");
        playerHealth -= damage;

        if (playerHealth <= 0)
        {
            Debug.Log("Player is dead.");
            StartCoroutine(AfterSecondsAndLoadScene(2f, "GameOver"));
        }
        else
        {
            StartCoroutine(AfterSecondsAndStand(2f));
        }
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
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null && hit.collider.CompareTag("DoubleThrowItemy"))
        {
            isItemClicked = true;
            return;
        }

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
            Debug.Log(currentPlayer + "============");
        }

        if (currentPlayer % 2 == 1)
        {
            PlayerAction(meemee, throwSlider1, lineRenderer1, isMouseClickedMeemee, ref currentThrowForceMeemee, KeyboardPrefab);
        }
        else if (currentPlayer % 2 == 0)
        {
            PlayerAction(rokrok, throwSlider2, lineRenderer2, isMouseClickedRokrok, ref currentThrowForceRokrok, MousePrefab);
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
        isItemClicked = false;

    }

    private void PlayerAction(GameObject player, Slider slider, LineRenderer lineRenderer, bool isMouseClicked, ref float currentThrowForce, GameObject prefab)
    {
        if (Input.GetMouseButton(0) && isCharging == true && isMouseClicked)
        {
            ChargeThrow(slider, ref currentThrowForce);
            if (currentThrowForce >= maxThrowForce)
            {
                if (useDoubleThrowItem)
                {
                    ThrowProjectileTwice(player, slider, lineRenderer, ref currentThrowForce, prefab);
                    useDoubleThrowItem = false;
                }
                else
                {
                    ThrowProjectile(player, slider, lineRenderer, ref currentThrowForce, prefab);
                }
                isCharging = false;

            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (isCharging == true)
            {
                if (useDoubleThrowItem)
                {
                    ThrowProjectileTwice(player, slider, lineRenderer, ref currentThrowForce, prefab);
                    useDoubleThrowItem = false;
                }
                else
                {
                    ThrowProjectile(player, slider, lineRenderer, ref currentThrowForce, prefab);
                }
            }
            else
            {
                isCharging = true;
            }
            isMouseClicked = false;
            currentThrowForce = 0f;
            windStrength = Random.Range(10, 300);
            windDirection = (Random.value > 0.5f);
        }
    }

    void ChargeThrow(Slider slider, ref float currentThrowForce)
    {
        currentThrowForce = Mathf.Clamp(currentThrowForce + Time.deltaTime * maxThrowForce, 0f, maxThrowForce);

        slider.value = currentThrowForce / maxThrowForce;

    }
    public void ThrowProjectile(GameObject player, Slider slider, LineRenderer lineRenderer, ref float currentThrowForce, GameObject prefab)
    {
        if (player == meemee)
            throwPosition = new Vector3(2f, 1, 0);
        else throwPosition = new Vector3(-2f, 1, 0);

        GameObject projectile = Instantiate(prefab, player.transform.position + throwPosition, Quaternion.identity);

        Vector3 direction = (lineRenderer.GetPosition(1) - player.transform.position).normalized;

        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
        float windForce = windDirection ? windStrength : -windStrength;
        Vector3 force = direction * currentThrowForce + new Vector3(windForce, 0, 0);
        projectileRb.AddForce(force, ForceMode2D.Impulse);

        Weapon keyboardScript = projectile.GetComponent<Weapon>();
        keyboardScript.owner = this;
        keyboardScript.isLive = true;

        currentThrowForce = 0f;
        slider.value = 0f;
        lineRenderer.enabled = false;
        StartCoroutine(AfterSecondsAndStand(2f));
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
        Debug.Log("PlayerInput?�서 충돌 ���리");
        Weapon weapon = collision.gameObject.GetComponent<Weapon>();

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

    void ThrowProjectileTwice(GameObject player, Slider slider, LineRenderer lineRenderer, ref float currentThrowForce, GameObject prefab)
    {
        if (player == meemee)
            throwPosition = new Vector3(2f, 1, 0);
        else throwPosition = new Vector3(-2f, 1, 0);
        for (int i = 0; i < 2; i++)
        {
            GameObject projectile = Instantiate(prefab, transform.position + throwPosition, Quaternion.identity);

            Vector3 direction = (lineRenderer.GetPosition(1) - transform.position).normalized;

            Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
            float windForce = windDirection ? windStrength : -windStrength;
            Vector3 force = direction * currentThrowForce + new Vector3(windForce, 0, 0);
            projectileRb.AddForce(force, ForceMode2D.Impulse);

            Weapon keyboardScript = projectile.GetComponent<Weapon>();
            keyboardScript.owner = this;
            keyboardScript.isLive = true;
        }

        currentThrowForce = 0f;
        slider.value = 0f;
        lineRenderer.enabled = false;
    }

    public void SetIsSelectingItem(bool value)
    {
        isSelectingItem = value;
    }

    public void AddItemButton(Button itemButton)
    {
        itemButtons.Add(itemButton);
    }

    public void DisableSelectedItemButton()
    {
        if (selectedItemButton != null)
        {
            selectedItemButton.interactable = false;
        }
    }


    private void OnItemButtonClick(Button itemButton)
    {
        if (!isSelectingItem)
        {
            return;
        }

        SetIsSelectingItem(false);
        DisableSelectedItemButton();
        selectedItemButton = itemButton;
    }

    IEnumerator AfterSecondsAndStand(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        anim.SetTrigger("Stand");
    }

    IEnumerator AfterSecondsAndLoadScene(float seconds, string sceneName)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(sceneName);
    }


}