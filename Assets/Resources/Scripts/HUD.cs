using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType { HP }
    public InfoType type;

    public PlayerInput player1;

    
    Slider leftSlider;

    // Start is called before the first frame update
    void Awake() {
        {
            leftSlider = GetComponent<Slider>();
        }
    }

    void LateUpdate()
    {
        switch (type) {
            case InfoType.HP:
                float curHealth = player1.playerHealth;
                float maxHealth = player1.maxHealth;
                leftSlider.value=curHealth / maxHealth;
                break;
        }
    }
}
