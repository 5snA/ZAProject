// Unity의 핵심 기능을 사용하기 위해 필요한 네임스페이스입니다.
using UnityEngine;

// HungerSystem 클래스는 "허기" 수치를 관리하는 시스템입니다.
// MonoBehaviour를 상속하면 Unity에서 이 스크립트를 GameObject에 붙일 수 있습니다.
public class HungerSystem : MonoBehaviour
{
    public static HungerSystem Instance;

    private int hungerLevel = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OnDayPassed()
    {
        int currentDay = GameManager.Instance.CurrentDay;

        if (currentDay % 2 == 0)
        {
            hungerLevel++;
            Debug.Log("[Hunger System] Hunger has increased! Current hunger level: " + hungerLevel);
        }
        else
        {
            Debug.Log("[Hunger System] Hunger does not increase today (Day " + currentDay + ").");
        }
    }

    // ✅ 외부에서 hungerLevel 값을 가져올 수 있도록 Getter 함수 추가
    public int GetHungerLevel()
    {
        return hungerLevel;
    }
}