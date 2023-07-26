using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleThrowItem : MonoBehaviour
{
    public bool isEffectActive = false;

    public void OnItemClick()
    {
        Debug.Log("asdfasdfasdfasdfd=================");
        StartCoroutine(DoubleThrowEffect());
    }

    public IEnumerator DoubleThrowEffect()
    {
        isEffectActive = true;
        PlayerInput.Instance.IsSelectingItem = true;

        int currentPlayer = PlayerInput.Instance.currentPlayer;
        for (int i = 0; i < 2; i++)
        {
            if (currentPlayer % 2 == 1)
            {
                PlayerInput.Instance.ThrowProjectile(PlayerInput.Instance.meemee, PlayerInput.Instance.throwSlider1, PlayerInput.Instance.lineRenderer1, ref PlayerInput.Instance.currentThrowForceMeemee, PlayerInput.Instance.KeyboardPrefab);
            }
            else if (currentPlayer % 2 == 0)
            {
                PlayerInput.Instance.ThrowProjectile(PlayerInput.Instance.rokrok, PlayerInput.Instance.throwSlider2, PlayerInput.Instance.lineRenderer2, ref PlayerInput.Instance.currentThrowForceRokrok, PlayerInput.Instance.MousePrefab);
            }
            yield return new WaitForSeconds(0.5f);
        }

        isEffectActive = false;
        PlayerInput.Instance.IsSelectingItem = false;
    }
}
