// Unity의 핵심 기능을 사용하기 위해 꼭 필요한 네임스페이스입니다.
using UnityEngine;

// ThirstSystem 클래스는 "갈증 수치"를 관리하는 시스템입니다.
// MonoBehaviour를 상속받았기 때문에 Unity 오브젝트에 붙여서 사용할 수 있습니다.
public class ThirstSystem : MonoBehaviour
{
    // 싱글톤 패턴을 사용하여 다른 스크립트에서 쉽게 접근할 수 있도록 합니다.
    public static ThirstSystem Instance;

    // 현재 갈증 단계를 저장하는 변수입니다.
    // 0은 정상 상태이며, 숫자가 클수록 더 갈증이 심한 상태입니다.
    private int thirstLevel = 0;

    // Unity가 게임 오브젝트를 생성할 때 자동으로 호출하는 함수입니다.
    private void Awake()
    {
        // 아직 인스턴스가 없다면 현재 객체를 인스턴스로 지정합니다.
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            // 이미 존재하는 인스턴스가 있다면 중복을 피하기 위해 제거합니다.
            Destroy(gameObject);
        }
    }

    // 하루가 지나면 GameManager에서 이 함수를 호출합니다.
    public void OnDayPassed()
    {
        // 하루가 지날 때마다 갈증이 1 증가합니다.
        thirstLevel++;

        // 현재 갈증 수치를 출력합니다 (Unity Console에서 확인 가능).
        Debug.Log("[Thirst System] Thirst has increased! Current thirst level: " + thirstLevel);

        // 추후 thirstLevel이 일정 수치를 넘으면 패널티를 적용할 수 있도록 확장 가능합니다.
    }
    
    // ThirstSystem.cs에 추가
    public int GetThirstLevel()
    {
        return thirstLevel;
    }

}