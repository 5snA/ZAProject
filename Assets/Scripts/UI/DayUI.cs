// Unity의 핵심 네임스페이스입니다.
using UnityEngine;
// 버튼 UI를 사용하기 위한 네임스페이스입니다.
using UnityEngine.UI;
// TextMeshPro 텍스트 사용을 위한 네임스페이스입니다.
using TMPro;

public class DayUI : MonoBehaviour
{
    // 싱글톤 패턴 설정: 외부에서 DayUI.Instance로 접근 가능
    public static DayUI Instance;

    // 다음 날로 진행하는 버튼입니다 (에디터에서 연결 필요)
    public Button nextDayButton;

    // 각각의 상태를 표시할 TMP 텍스트들입니다 (에디터에서 연결 필요)
    public TMP_Text dayText;
    public TMP_Text hungerText;
    public TMP_Text thirstText;
    public TMP_Text sanityText;
    public TMP_Text fatigueText;

    // 이 Awake 함수는 오브젝트가 생성될 때 가장 먼저 호출됩니다.
    private void Awake()
    {
        // 싱글톤 인스턴스가 비어 있으면 현재 인스턴스를 지정
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            // 이미 인스턴스가 있다면 중복 생성을 막기 위해 제거
            Destroy(gameObject);
        }
    }

    // 게임 시작 시 1회 호출되는 함수입니다.
    private void Start()
    {
        // "다음 날" 버튼을 클릭했을 때 AdvanceDay() 실행
        nextDayButton.onClick.AddListener(() =>
        {
            // 하루 진행
            GameManager.Instance.AdvanceDay();

            // 버튼을 눌렀을 때 UI도 즉시 갱신되도록 텍스트 업데이트
            UpdateStatusTexts();
        });

        // 게임 시작 시 텍스트를 한 번 갱신
        UpdateStatusTexts();
    }

    // 각 상태 텍스트를 현재 값에 맞게 갱신하는 함수입니다.
    public void UpdateStatusTexts()
    {
        // 현재 날짜 표시
        dayText.text = "Day: " + GameManager.Instance.CurrentDay;

        // 허기 수치 표시
        hungerText.text = "Hunger: " + HungerSystem.Instance.GetHungerLevel();

        // 갈증 수치 표시
        thirstText.text = "Thirst: " + ThirstSystem.Instance.GetThirstLevel();

        // 정신력 상태 표시 (Normal, Anxious, Frenzied 중 하나)
        sanityText.text = "Sanity: " + SanitySystem.Instance.GetCurrentState().ToString();

        // 피로 상태 표시 (Yes 또는 No)
        fatigueText.text = "Fatigued: " + (FatigueSystem.Instance.IsFatigued() ? "Yes" : "No");
    }
}