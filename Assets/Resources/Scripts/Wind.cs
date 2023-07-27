using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Wind : MonoBehaviour
{
    public static Transform[] windPoints;
    static float windStrength;
    static bool windDirection;
    static public float windForce;
    // Start is called before the first frame update
    void Awake()
    {
        windPoints = GetComponentsInChildren<Transform>();
    }

    public static float GetWind(){
        Debug.Log("들어오긴 함");
        windStrength = Random.Range(10, 300);
        Debug.Log("들어오긴 함2");
        windDirection = (Random.value > 0.5f);
        Debug.Log("들어오긴 함3");
        windForce = windDirection ? windStrength : -windStrength;
        Debug.Log("windForce"+ windForce);
        UpdateWindPoints();
        Debug.Log("들어오긴 함4");
        return windForce;   
    }

        static void UpdateWindPoints()
    {
        // Calculate the number of active windPoints based on windForce
        int numWindPoints = Mathf.FloorToInt(Mathf.Abs(windForce) / 50f); // adjust this line to suit your needs
        Debug.Log(numWindPoints+" numWindPoints");
        numWindPoints = Mathf.Clamp(numWindPoints, 1, 6);

        // Deactivate all windPoints
        for (int i = 1; i < windPoints.Length; i++)
        {
            if (i < numWindPoints)
            {
                var spriteRenderer = windPoints[i].gameObject.GetComponent<SpriteRenderer>();
                windPoints[i].gameObject.SetActive(true);
                if(spriteRenderer != null) 
                {
                    Debug.Log("hello ");
                    spriteRenderer.flipX = !(windDirection);
                }
            }
            else
            {
                Debug.Log("hello 2");
                windPoints[i].gameObject.SetActive(false);
            }
        }
    }
}
