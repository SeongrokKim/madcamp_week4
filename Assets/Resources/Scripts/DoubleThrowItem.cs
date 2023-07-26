using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleThrowItem : MonoBehaviour
{
    // 아이템의 효과 적용 여부를 나타내는 변수
    public bool isEffectActive = false;

    // 아이템을 클릭했을 때 실행될 함수
    public void OnItemClick()
    {
        Debug.Log("asdfasdfasdfasdfd=================");
        // 아이템 효과 발동
        StartCoroutine(DoubleThrowEffect());
    }

    // 아이템 효과를 발동하는 코루틴
    public IEnumerator DoubleThrowEffect()
    {
        // 아이템 효과를 발동하고 지속시간을 설정
        isEffectActive = true;
        PlayerInput.Instance.IsSelectingItem = true; // 다른 아이템 선택 방지

        int currentPlayer = PlayerInput.Instance.currentPlayer;

        // 여기에 아이템의 효과를 구현하는 로직을 추가하세요.
        // 예시로 투사체를 두 번 던지는 효과를 구현하겠습니다.
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
            // 투사체 간 간격을 조절하려면 원하는 시간으로 설정하세요.
            yield return new WaitForSeconds(0.5f);
        }

        // 아이템 효과 종료
        isEffectActive = false;
        PlayerInput.Instance.IsSelectingItem = false; // 아이템 선택 가능으로 변경
    }
}
