using UnityEngine;

// GameManager 클래스는 게임 전체 흐름을 관리하는 중앙 관리자 역할을 합니다.
// MonoBehaviour를 상속하면 이 스크립트를 Unity 오브젝트에 붙일 수 있습니다.
public class GameManager : MonoBehaviour
{
    // 싱글톤 패턴: 다른 스크립트에서 GameManager.Instance로 접근할 수 있게 합니다.
    public static GameManager Instance;

    // 게임 상태를 정의한 열거형입니다.
    // Shelter: 쉘터 상태, Exploration: 탐사 중, Event: 랜덤 이벤트 발생 중
    public enum GameState { Shelter, Exploration, Event }

    // 현재 게임 상태를 저장하는 변수입니다.
    // 외부에서는 읽기만 가능하고, 변경은 GameManager 내부에서만 가능합니다.
    public GameState CurrentState { get; private set; }

    // 현재 날짜를 저장하는 변수입니다. 게임은 1일차부터 시작합니다.
    public int CurrentDay { get; private set; } = 1;

    // ✅ 탐사 UI를 에디터에서 연결하기 위한 변수입니다.
    // 하루가 끝나고 다시 탐사 버튼들을 보이게 할 때 사용됩니다.
    public GameObject explorationPanel;

    // 오브젝트가 생성될 때 가장 먼저 호출되는 함수입니다.
    // 여기서 싱글톤 인스턴스를 설정하고, 중복 생성을 방지합니다.
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시에도 유지
        }
        else
        {
            Destroy(gameObject); // 이미 존재하는 인스턴스가 있다면 제거
        }
    }

    // 게임이 시작될 때 호출되는 함수입니다.
    private void Start()
    {
        // 콘솔에 시작 로그 출력
        Debug.Log("Game started (Day " + CurrentDay + ")");

        // 기본 게임 상태를 쉘터로 설정
        CurrentState = GameState.Shelter;

        // 상태 텍스트 초기화
        DayUI.Instance.UpdateStatusTexts();

        // // 하루를 강제로 한 번 진행 (테스트용)
        // AdvanceDay();
    }

    // 하루가 경과할 때 호출되는 핵심 함수입니다.
    public void AdvanceDay()
    {
        // 날짜 증가
        CurrentDay++;

        // 콘솔에 하루 시작 로그 출력
        Debug.Log("==== Day " + CurrentDay + " Start ====");

        // 생존 시스템 호출 (각 시스템에서 하루 경과 처리)
        HungerSystem.Instance.OnDayPassed();       // 허기 증가
        ThirstSystem.Instance.OnDayPassed();       // 갈증 증가
        FatigueSystem.Instance.OnDayPassed();      // 피로도 처리
        SanitySystem.Instance.OnDayPassed();       // 정신력 변화

        // 랜덤 이벤트 발생 여부 체크
        EventSystem.Instance.CheckForRandomEvent();

        // 게임 상태를 다시 쉘터 상태로 설정
        CurrentState = GameState.Shelter;

        // ✅ 탐사 UI를 다시 보여줍니다.
        if (explorationPanel != null)
        {
            explorationPanel.SetActive(true); // 탐사 UI 패널을 활성화
            Debug.Log("[GameManager] 탐사 UI가 다시 나타났습니다.");
        }
        else
        {
            Debug.LogWarning("[GameManager] explorationPanel이 연결되지 않았습니다!");
        }

        // ✅ 텍스트 UI 갱신
        DayUI.Instance.UpdateStatusTexts();
    }
}
