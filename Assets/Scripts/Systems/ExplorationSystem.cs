using UnityEngine;

// ExplorationSystem 클래스는 플레이어가 선택한 장소로 탐사를 진행하는 시스템입니다.
public class ExplorationSystem : MonoBehaviour
{
    // 싱글톤 패턴: 다른 스크립트에서도 ExplorationSystem.Instance로 접근할 수 있게 설정합니다.
    public static ExplorationSystem Instance;

    // 탐사 가능한 장소 목록입니다. (필요에 따라 UI에서 활용 가능)
    private string[] locations = {
        "Police Station", "Hospital", "Fire Station", "Supermarket", "Warehouse", "Park", "School"
    };

    // Unity가 이 오브젝트를 생성할 때 자동으로 호출되는 함수입니다.
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject); // 중복 방지
    }

    // 🔔 플레이어가 탐사할 장소를 선택했을 때 호출하는 함수입니다.
    // UI 버튼 등에서 이 함수를 호출하며 문자열로 장소를 전달합니다.
    public void StartExploration(string location)
    {
        Debug.Log("[Exploration] Exploring " + location + "...");
        HandleExploration(location); // 실제 탐사 처리 시작
    }

    // 실제 탐사 결과를 처리하는 내부 함수입니다.
    private void HandleExploration(string location)
    {
        // 전투 확률 설정 (공원, 병원, 학교는 70%, 나머지는 50%)
        int combatChance = (location == "Hospital" || location == "Park" || location == "School") ? 70 : 50;
        int combatRoll = Random.Range(1, 101); // 1~100 중 무작위 수

        // 전투 발생 여부 판단
        if (combatRoll <= combatChance)
        {
            Debug.Log("[Exploration] A battle occurred during exploration! (Chance: " + combatChance + "%)");
            // 전투 시스템 호출 예정
        }
        else
        {
            Debug.Log("[Exploration] Exploration completed safely without battle.");
        }

        // NPC 발견 확률 60%
        int npcRoll = Random.Range(1, 101);
        if (npcRoll <= 60)
        {
            Debug.Log("[Exploration] You discovered a professional NPC!");
            // NPC 영입 이벤트 처리 예정
        }
        else
        {
            Debug.Log("[Exploration] No NPC found during the exploration.");
        }

        // 물자 획득
        HandleLoot(location);

        // 피로 상태 적용
        FatigueSystem.Instance.OnExplorationCompleted();
    }

    // 탐사 장소에 따라 물자를 획득하는 함수입니다.
    private void HandleLoot(string location)
    {
        Debug.Log("[Exploration] Searching for supplies at " + location + "...");

        // 기본 물자 획득 (각각 25% 확률)
        if (Random.Range(0f, 1f) <= 0.25f)
            Debug.Log("🍱 Found a canned food!");

        if (Random.Range(0f, 1f) <= 0.25f)
            Debug.Log("🥤 Found bottled water!");

        // 병원만의 고유 아이템 (예시)
        if (location == "Hospital")
        {
            if (Random.Range(0f, 1f) <= 0.10f) Debug.Log("🩺 Found surgical tools!");
            if (Random.Range(0f, 1f) <= 0.10f) Debug.Log("🦾 Found a prosthetic arm!");
            if (Random.Range(0f, 1f) <= 0.10f) Debug.Log("🦿 Found a prosthetic leg!");
            if (Random.Range(0f, 1f) <= 0.10f) Debug.Log("🧰 Found a first-aid kit!");
            if (Random.Range(0f, 1f) <= 0.10f) Debug.Log("💊 Found a sanity pill!");
        }

        // 다른 장소들의 고유 물자도 이후에 이곳에서 확장 가능합니다.
    }
    
    // UI에서 탐사 버튼을 눌렀을 때 호출되는 함수입니다.
// 전달받은 장소 이름(locationName)을 그대로 탐사 처리에 넘깁니다.
    public void HandleExplorationFromUI(string locationName)
    {
        Debug.Log("[Exploration] Button clicked → Exploring " + locationName);
        HandleExploration(locationName); // 내부 로직은 기존과 동일하게 처리
    }

}
