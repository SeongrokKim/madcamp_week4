using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Unity UIë¥¼ ì‚¬ìš©í•˜ê¸° ìœ„í•œ ë„¤ì„ìŠ¤í˜ì´ìŠ¤ ì¶”ê°€

public class PlayerInput : MonoBehaviour
{
<<<<<<< HEAD
    public GameObject KeyboardPrefab; // Åõ»çÃ¼ ÇÁ¸®ÆÕÀ» ¿¬°áÇÒ º¯¼ö
    public float maxThrowForce = 5f; // Åõ»çÃ¼°¡ °¡Áú ¼ö ÀÖ´Â ÃÖ´ë Èû
    private float currentThrowForceMeemee = 0f;
    private float currentThrowForceRokrok = 0f;

    // Slider UI ¿¬°áÀ» À§ÇÑ º¯¼ö
    public Slider throwSlider1;
    public Slider throwSlider2;
=======
    public GameObject KeyboardPrefab; // íˆ¬ì‚¬ì²´ í”„ë¦¬íŒ¹ì„ ì—°ê²°í•  ë³€ìˆ˜
    public float maxThrowForce = 10f; // íˆ¬ì‚¬ì²´ê°€ ê°€ì§ˆ ìˆ˜ ìˆëŠ” ìµœëŒ€ í˜
    private float currentThrowForce = 0f; // í˜„ì¬ íˆ¬ì‚¬ì²´ì˜ í˜

    // Slider UI ì—°ê²°ì„ ìœ„í•œ ë³€ìˆ˜
    public Slider throwSlider;
>>>>>>> master
    private bool isCharging = true;
    public Text angleText;

<<<<<<< HEAD
    // ±ä Á¡¼± Ç¥½Ã¸¦ À§ÇÑ LineRenderer ÄÄÆ÷³ÍÆ®
    public LineRenderer lineRenderer1;
    public LineRenderer lineRenderer2;

    public float minAngle = 15f;
    public float maxAngle = 75f;
    private bool isMouseClicked = false;

    // ÇÃ·¹ÀÌ¾î ¹ø°¥¾Æ°¡¸ç Çàµ¿ÇÏ±â À§ÇÑ º¯¼ö
    private int currentPlayer = 0;

    // µÎ °³ÀÇ Ä³¸¯ÅÍ¸¦ ÇÒ´çÇÒ º¯¼ö
    public GameObject meemee;
    public GameObject rokrok;

    // °¢ Ä³¸¯ÅÍÀÇ ¸¶¿ì½º Å¬¸¯ »óÅÂ¸¦ ÀúÀåÇÒ º¯¼ö
    private bool isMouseClickedMeemee = false;
    private bool isMouseClickedRokrok = false;

    private void Start()
    {
        // °ÔÀÌÁö ÃÊ±âÈ­
        throwSlider1.value = 0f;

        // ±ä Á¡¼±ÀÇ ½ÃÀÛ°ú ³¡ ÁöÁ¡ ¼³Á¤ (ÃÊ±â¿¡´Â Ä³¸¯ÅÍ À§Ä¡¿¡¼­ Ä³¸¯ÅÍ À§Ä¡·Î ¼³Á¤)
        lineRenderer1.SetPositions(new Vector3[] { transform.position, transform.position });

        // ¶óÀÎ ·»´õ·¯¸¦ Á¡¼±À¸·Î Ç¥½ÃÇÏ±â À§ÇØ ¸ÓÆ¼¸®¾ó ¼³Á¤
        lineRenderer1.material = new Material(Shader.Find("Sprites/Default"));
        // °ÔÀÌÁö ÃÊ±âÈ­
        throwSlider2.value = 0f;

        // ±ä Á¡¼±ÀÇ ½ÃÀÛ°ú ³¡ ÁöÁ¡ ¼³Á¤ (ÃÊ±â¿¡´Â Ä³¸¯ÅÍ À§Ä¡¿¡¼­ Ä³¸¯ÅÍ À§Ä¡·Î ¼³Á¤)
        lineRenderer2.SetPositions(new Vector3[] { transform.position, transform.position });

        // ¶óÀÎ ·»´õ·¯¸¦ Á¡¼±À¸·Î Ç¥½ÃÇÏ±â À§ÇØ ¸ÓÆ¼¸®¾ó ¼³Á¤
        lineRenderer2.material = new Material(Shader.Find("Sprites/Default"));
    
    
=======
    public float minAngle = 15f;
    public float maxAngle = 75f;

    // í”Œë ˆì´ì–´ ì²´ë ¥
    public float playerHealth;
    public float maxHealth = 100f;
    private void Start()
    {
        // ê²Œì´ì§€ ì´ˆê¸°í™”
        throwSlider.value = 0f;
        playerHealth=maxHealth;

        
    }

    // ì²´ë ¥ ê°ì†Œ ì²˜ë¦¬
    public void TakeDamage(int damage)
    {
        playerHealth -= damage;
        if (playerHealth <= 0)
        {
            Debug.Log("Player is dead.");
            // í”Œë ˆì´ì–´ ì‚¬ë§ ì²˜ë¦¬
        }
>>>>>>> master
    }

    private void Update()
    {
<<<<<<< HEAD

        if (Input.GetMouseButtonDown(0))
        {
            if (currentPlayer == 1)
                isMouseClickedMeemee = true;
            else if (currentPlayer == 0)
                isMouseClickedRokrok = true;

            currentPlayer = (currentPlayer + 1) % 2;
        }

        // ÇÃ·¹ÀÌ¾î ¹ø°¥¾Æ°¡¸ç Çàµ¿
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

    // ÇÃ·¹ÀÌ¾î Çàµ¿ ÇÔ¼ö
    private void PlayerAction(GameObject player, Slider slider, LineRenderer lineRenderer, bool isMouseClicked, ref float currentThrowForce)
    {
        if (Input.GetMouseButton(0) && isCharging == true && isMouseClicked)
=======
        // ë§ˆìš°ìŠ¤ ì™¼ìª½ ë²„íŠ¼ì„ ëˆ„ë¥´ê³  ìˆëŠ” ë™ì•ˆ ê²Œì´ì§€ë¥¼ ì±„ìš°ê¸°
        if (Input.GetMouseButton(0) && isCharging == true)
>>>>>>> master
        {
            ChargeThrow(slider, ref currentThrowForce);
            if (currentThrowForce >= maxThrowForce)
            {
                ThrowProjectile(player, slider, lineRenderer, ref currentThrowForce);
                isCharging = false;

            }
        }
<<<<<<< HEAD
=======
        // ë§ˆìš°ìŠ¤ ì™¼ìª½ ë²„íŠ¼ì„ ë†“ìœ¼ë©´ íˆ¬ì‚¬ì²´ë¥¼ ë˜ì§
>>>>>>> master
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
<<<<<<< HEAD
            isMouseClicked = false;
            currentThrowForce = 0f;
=======
>>>>>>> master
        }
    }

    // ±âÁ¸ ÄÚµå¿Í µ¿ÀÏÇÏ°Ô ±¸Çö
    void ChargeThrow(Slider slider, ref float currentThrowForce)
    {
        // í˜ì˜ í¬ê¸°ë¥¼ ê²Œì´ì§€ë¡œ ì¡°ì ˆ
        currentThrowForce = Mathf.Clamp(currentThrowForce + Time.deltaTime * maxThrowForce, 0f, maxThrowForce);

<<<<<<< HEAD
        // Slider UI¿¡ ÇöÀç °ÔÀÌÁö °ª ¹İ¿µ
        slider.value = currentThrowForce / maxThrowForce;

=======
        // Slider UIì— í˜„ì¬ ê²Œì´ì§€ ê°’ ë°˜ì˜
        throwSlider.value = currentThrowForce / maxThrowForce;
>>>>>>> master
    }

    // ±âÁ¸ ÄÚµå¿Í µ¿ÀÏÇÏ°Ô ±¸Çö
    void ThrowProjectile(GameObject player, Slider slider, LineRenderer lineRenderer, ref float currentThrowForce)
    {
<<<<<<< HEAD
        // Åõ»çÃ¼¸¦ »ı¼ºÇÏ°í Ä³¸¯ÅÍ°¡ º¸´Â ¹æÇâÀ¸·Î ´øÁü
        GameObject projectile = Instantiate(KeyboardPrefab, player.transform.position, Quaternion.identity);
=======
        GameObject projectile = Instantiate(KeyboardPrefab, transform.position+ new Vector3(1.3f, 1, 0), Quaternion.identity);
>>>>>>> master

        Vector3 direction;

        if (isMouseClicked) // ¸¶¿ì½º¸¦ Å¬¸¯ÇÑ »óÅÂ¶ó¸é
        {
            // ¸¶¿ì½º Å¬¸¯ÇÑ ½ÃÁ¡ÀÇ °¢µµ¸¦ À¯ÁöÇÏ¿© ¹ß»ç
            direction = (lineRenderer.GetPosition(1) - player.transform.position).normalized;
        }
        else
        {
            // ¸¶¿ì½º¸¦ Å¬¸¯ÇÏÁö ¾ÊÀº °æ¿ì ±âÁ¸°ú µ¿ÀÏÇÏ°Ô ¹ß»ç °¢µµ °è»ê
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

    // ±âÁ¸ ÄÚµå¿Í µ¿ÀÏÇÏ°Ô ±¸Çö
    void UpdateLineRenderer(GameObject player, LineRenderer lineRenderer)
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = (mousePosition - player.transform.position).normalized;

        // ¸¶¿ì½º°¡ ¹üÀ§ ¾È¿¡ ÀÖ´Â °æ¿ì Á¡¼±ÀÇ ³¡ ÁöÁ¡ ¼³Á¤ (¸¶¿ì½º ¹æÇâÀ¸·Î ÃÖ´ë °Å¸®±îÁö ¼³Á¤)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle = Mathf.Clamp(angle, minAngle, maxAngle);
        Vector3 endPoint = player.transform.position + Quaternion.Euler(0f, 0f, angle) * Vector3.right * 6f;

        // LineRendererÀÇ Á¡¼± Ç¥½Ã ¾÷µ¥ÀÌÆ®
        lineRenderer.SetPositions(new Vector3[] { player.transform.position, endPoint });
    }
<<<<<<< HEAD
}
=======

    
    public void ProcessCollision(Collision2D collision, string collisionPoint )
    {
        Debug.Log("PlayerInputì—ì„œ ì¶©ëŒ ì²˜ë¦¬");
        Weapon weapon = collision.gameObject.GetComponent<Weapon>();

        // ì¶©ëŒí•œ ê°ì²´ì— Weapon ì»´í¬ë„ŒíŠ¸ê°€ ìˆëŠ” ê²½ìš°
        if (weapon != null && weapon.isLive == true && weapon.owner != this)
        {
            weapon.isLive=false;
            // ì¶©ëŒ ì‹œì˜ ì†ë„ë¥¼ ê°€ì ¸ì˜´
            Vector3 collisionVelocity = collision.relativeVelocity;

            // Weapon ì»´í¬ë„ŒíŠ¸ì˜ CalculateDamage í•¨ìˆ˜ë¥¼ í˜¸ì¶œí•˜ì—¬ ë°ë¯¸ì§€ë¥¼ ê³„ì‚°
            int damage = weapon.CalculateDamage(collisionPoint, collisionVelocity);

            TakeDamage(damage);
            Debug.Log(collisionVelocity + "===== collisionVelocity");
            Debug.Log(damage + "===== damage");
        }
    }

    
}


>>>>>>> master
