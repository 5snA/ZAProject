using UnityEngine;
using System.Collections.Generic;
using ZAProject.Data; // ExplorationLootTable 경로를 정확하게 지정

// ExplorationSystem 클래스는 플레이어가 탐사를 진행할 때 모든 로직을 처리하는 클래스입니다.
public class ExplorationSystem : MonoBehaviour
{
    // 싱글톤으로 외부에서 접근 가능하도록 설정합니다.
    public static ExplorationSystem Instance;

    // 탐사 지점마다의 전리품 테이블을 담는 리스트입니다 (Inspector에서 할당).
    public List<ExplorationLootTable> lootTables;

    // 인스턴스 설정
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // UI에서 탐사 버튼을 눌렀을 때 호출되는 함수
    public void HandleExplorationFromUI(string locationName)
    {
        Debug.Log("[Exploration] Button clicked → Exploring " + locationName);
        HandleExploration(locationName);
    }

    // 탐사의 핵심 로직이 들어있는 함수
    private void HandleExploration(string location)
    {
        // 전투 확률 계산
        int combatChance = (location == "Hospital" || location == "Park" || location == "School") ? 70 : 50;
        int combatRoll = Random.Range(1, 101);
        if (combatRoll <= combatChance)
            Debug.Log("[Exploration] A battle occurred during exploration!");
        else
            Debug.Log("[Exploration] Exploration completed safely.");

        // NPC 발견 여부
        if (Random.Range(1, 101) <= 60)
            Debug.Log("[Exploration] You discovered a professional NPC!");
        else
            Debug.Log("[Exploration] No NPC found this time.");

        // 전리품 획득
        HandleLoot(location);

        // 피로도 반영
        FatigueSystem.Instance.OnExplorationCompleted();
    }

    // 실제 아이템 획득 로직
    private void HandleLoot(string location)
    {
        Debug.Log("[Exploration] Searching for supplies at " + location + "...");

        // 기본 물자 (25% 확률로 음식과 물 획득)
        if (Random.Range(0f, 1f) <= 0.25f)
            Debug.Log("🍱 Found a canned food!");
        if (Random.Range(0f, 1f) <= 0.25f)
            Debug.Log("🥤 Found bottled water!");

        // ScriptableObject에서 해당 지역의 전리품 정보를 찾아서 처리
        ExplorationLootTable table = lootTables.Find(t => t.LocationName == location);
        if (table == null)
        {
            Debug.LogWarning("[Exploration] No loot table found for location: " + location);
            return;
        }

        foreach (var entry in table.lootEntries)
        {
            if (Random.Range(0f, 1f) <= entry.dropChance)
            {
                Debug.Log("🎒 Found: " + entry.itemName);
            }
        }
    }
}
