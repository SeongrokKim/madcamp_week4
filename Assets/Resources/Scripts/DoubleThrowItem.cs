using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleThrowItem : MonoBehaviour
{
    // �������� ȿ�� ���� ���θ� ��Ÿ���� ����
    public bool isEffectActive = false;

    // �������� Ŭ������ �� ����� �Լ�
    public void OnItemClick()
    {
        Debug.Log("asdfasdfasdfasdfd=================");
        // ������ ȿ�� �ߵ�
        StartCoroutine(DoubleThrowEffect());
    }

    // ������ ȿ���� �ߵ��ϴ� �ڷ�ƾ
    public IEnumerator DoubleThrowEffect()
    {
        // ������ ȿ���� �ߵ��ϰ� ���ӽð��� ����
        isEffectActive = true;
        PlayerInput.Instance.IsSelectingItem = true; // �ٸ� ������ ���� ����

        int currentPlayer = PlayerInput.Instance.currentPlayer;

        // ���⿡ �������� ȿ���� �����ϴ� ������ �߰��ϼ���.
        // ���÷� ����ü�� �� �� ������ ȿ���� �����ϰڽ��ϴ�.
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
            // ����ü �� ������ �����Ϸ��� ���ϴ� �ð����� �����ϼ���.
            yield return new WaitForSeconds(0.5f);
        }

        // ������ ȿ�� ����
        isEffectActive = false;
        PlayerInput.Instance.IsSelectingItem = false; // ������ ���� �������� ����
    }
}
