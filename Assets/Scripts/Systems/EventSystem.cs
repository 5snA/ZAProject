using UnityEngine;

// 이 클래스는 하루가 지날 때 랜덤한 이벤트를 발생시키는 시스템입니다.
public class EventSystem : MonoBehaviour
{
    // 싱글톤 패턴으로 다른 스크립트에서 EventSystem.Instance로 접근할 수 있도록 합니다.
    public static EventSystem Instance;

    // 하루마다 이벤트가 발생할 확률 (기본값은 50%)
    private float eventChance = 0.5f;

    // 오브젝트가 생성될 때 호출되는 함수입니다.
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject); // 중복 방지를 위해 기존 인스턴스가 있으면 제거
    }

    // GameManager가 하루가 지났을 때 호출하는 함수입니다.
    public void CheckForRandomEvent()
    {
        // 0.0 ~ 1.0 사이의 랜덤 값을 생성합니다.
        float roll = Random.Range(0f, 1f);

        // 랜덤값이 eventChance보다 작거나 같으면 이벤트가 발생합니다.
        if (roll <= eventChance)
        {
            TriggerRandomEvent();
        }
        else
        {
            Debug.Log("[Event System] Nothing happened today.");
        }
    }

    // 랜덤 이벤트를 실제로 실행하는 함수입니다.
    private void TriggerRandomEvent()
    {
        int day = GameManager.Instance.CurrentDay;

        int eventCount = 8; // 총 이벤트 개수
        int index = Random.Range(0, eventCount); // 0부터 7까지 중 하나를 무작위로 선택

        switch (index)
        {
            case 0:
                if (day >= 4)
                {
                    Debug.Log("[Event] A retired soldier is standing outside the shelter.");
                    // 영입 로직 추가 예정
                }
                else
                {
                    Debug.Log("[Event] You heard a strange sound, but no one was there.");
                }
                break;

            case 1:
                Debug.Log("[Event] A thief is lurking near the shelter.");
                // 선택지 구현 필요 (도둑을 들일지 여부)
                break;

            case 2:
                Debug.Log("[Event] A zombie burst in when you opened the door!");
                // 전투 발생 처리 필요
                break;

            case 3:
                Debug.Log("[Event] A mouse is trying to steal your food.");
                // 도구 여부에 따라 음식 손실 여부 결정
                break;

            case 4:
                Debug.Log("[Event] An earthquake shook the shelter!");
                // 음식/음료를 제외한 도구 중 하나 파손
                break;

            case 5:
                Debug.Log("[Event] A fire broke out in the shelter!");
                // 소방관이 있는지에 따라 손실 여부 결정
                break;

            case 6:
                Debug.Log("[Event] The recruited thief has fled!");
                // 도둑이 있다면 물자 손실 발생
                break;

            case 7:
                Debug.Log("[Event] Armed raiders have attacked the shelter!");
                // 전투 아이템 보유 여부에 따라 피해량 결정
                break;
        }
    }
}
