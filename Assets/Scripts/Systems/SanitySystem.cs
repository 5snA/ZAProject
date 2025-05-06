using UnityEngine;
using System;

public class SanitySystem : MonoBehaviour
{
    // 싱글톤 패턴: 다른 스크립트에서 SanitySystem.Instance로 접근 가능하게 합니다.
    public static SanitySystem Instance;

    // 정신력 상태를 나타내는 열거형(enum)입니다.
    public enum SanityState
    {
        Normal,     // 정상
        Anxious,    // 불안
        Frenzied    // 광란
    }

    // 현재 정신 상태를 저장하는 변수입니다.
    private SanityState currentState = SanityState.Normal;

    // 마지막으로 정신력이 감소한 날 (광란 상태 진입 조건 확인용)
    private int lastDecreaseDay = -1;

    // 정신력 회복 없이 지나간 일수 (5일 이상이면 광란으로 진입)
    private int daysWithoutRecovery = 0;

    // 하루가 지날 때 GameManager가 호출하는 함수입니다.
    public void OnDayPassed()
    {
        int currentDay = GameManager.Instance.CurrentDay;

        // 5일간 정신력이 회복되지 않았다면 광란 상태로 진입
        if (currentState != SanityState.Frenzied && daysWithoutRecovery >= 5)
        {
            EnterFrenziedState();
        }

        // 광란 상태일 경우, 매일 도망 여부를 판정합니다 (10% 확률)
        if (currentState == SanityState.Frenzied)
        {
            int chance = UnityEngine.Random.Range(1, 101); // 1~100 사이 랜덤 수

            if (chance <= 10)
            {
                Debug.Log("[Sanity System] The character ran away due to frenzy! Cannot recover.");
                // 실제 캐릭터 제거 로직은 추후 추가 예정
            }
            else
            {
                Debug.Log("[Sanity System] Frenzied state continues. The character managed to stay.");
            }
        }

        // 하루가 지났으므로 회복되지 않은 일수를 1 증가시킵니다.
        daysWithoutRecovery++;
    }

    // 정신력이 하락해야 하는 상황일 때 호출 (예: 동료 사망, 습격 등)
    public void DecreaseSanity()
    {
        if (currentState == SanityState.Normal)
        {
            currentState = SanityState.Anxious;
            Debug.Log("[Sanity System] Sanity decreased: now Anxious.");
        }
        else if (currentState == SanityState.Anxious)
        {
            EnterFrenziedState();
        }

        lastDecreaseDay = GameManager.Instance.CurrentDay;

        // 상태가 악화되면 회복 카운트는 리셋됩니다.
        daysWithoutRecovery = 0;
    }

    // 정신 안정제 등을 사용해서 정신력을 1단계 회복하는 함수입니다.
    public void RecoverSanity()
    {
        if (currentState == SanityState.Frenzied)
        {
            currentState = SanityState.Anxious;
            Debug.Log("[Sanity System] Recovered from Frenzied to Anxious.");
        }
        else if (currentState == SanityState.Anxious)
        {
            currentState = SanityState.Normal;
            Debug.Log("[Sanity System] Recovered to Normal sanity.");
        }

        // 회복 시에는 회복 누락 일수도 초기화됩니다.
        daysWithoutRecovery = 0;
    }

    // 광란 상태로 진입하는 로직을 처리합니다.
    private void EnterFrenziedState()
    {
        currentState = SanityState.Frenzied;
        Debug.Log("[Sanity System] Entered Frenzied state! Stat penalties and escape chance are now active.");
    }

    // 현재 정신 상태를 다른 스크립트에서 확인할 수 있게 합니다.
    public SanityState GetCurrentState()
    {
        return currentState;
    }

    // 싱글톤 인스턴스를 설정하는 Unity 기본 함수입니다.
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject); // 중복 방지
    }
}
