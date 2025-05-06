// Unity의 핵심 기능을 사용하기 위한 네임스페이스입니다.
using UnityEngine;

// FatigueSystem 클래스는 "탐사 후 피로 상태"를 관리하는 시스템입니다.
public class FatigueSystem : MonoBehaviour
{
    // 싱글톤 패턴: 다른 스크립트에서도 FatigueSystem.Instance로 접근 가능하게 합니다.
    public static FatigueSystem Instance;

    // 현재 피로 상태인지 여부를 저장하는 변수입니다.
    private bool isFatigued = false;

    // 마지막으로 탐사를 진행한 날짜를 저장합니다.
    private int lastExplorationDay = -1;

    // 같은 날에 여러 번 로그가 찍히지 않도록 하는 플래그입니다.
    private bool fatigueLoggedToday = false;

    // 싱글톤 인스턴스를 설정하는 Unity의 초기화 함수입니다.
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject); // 중복 인스턴스 제거
    }

    // 하루가 지날 때 GameManager에서 호출되는 함수입니다.
    public void OnDayPassed()
    {
        int currentDay = GameManager.Instance.CurrentDay;

        if (isFatigued)
        {
            // 탐사 후 2일 이상 쉬었는지 확인
            if (currentDay - lastExplorationDay >= 2)
            {
                isFatigued = false;
                Debug.Log("[Fatigue System] Fatigue has been recovered after sufficient rest.");
            }
            else if (!fatigueLoggedToday)
            {
                // 피로 상태일 경우 패널티 메시지를 하루에 한 번만 출력
                Debug.Log("[Fatigue System] You are fatigued. -40% penalty to actions, exploration, and combat.");
                fatigueLoggedToday = true;
            }
        }
        else
        {
            // 피로 상태가 아닐 경우 로그 출력 플래그를 리셋
            fatigueLoggedToday = false;
        }
    }

    // 탐사를 마치면 GameManager에서 호출되는 함수입니다.
    // 현재 날짜를 마지막 탐사일로 기록하고 피로 상태로 설정합니다.
    public void OnExplorationCompleted()
    {
        lastExplorationDay = GameManager.Instance.CurrentDay;
        isFatigued = true;
        Debug.Log("[Fatigue System] You have entered a fatigued state after exploration.");
    }

    // 현재 피로 상태인지 확인할 수 있는 함수입니다 (외부에서 사용 가능).
    public bool IsFatigued()
    {
        return isFatigued;
    }
}