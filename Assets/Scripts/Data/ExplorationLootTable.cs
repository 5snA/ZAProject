using UnityEngine;
using System.Collections.Generic;

namespace ZAProject.Data
{
    // ✅ 전리품 항목 하나를 정의하는 클래스입니다.
    [System.Serializable] // 👉 이걸 반드시 붙여야 Inspector에 보입니다!
    public class LootEntry
    {
        public string itemName;     // 아이템 이름
        [Range(0f, 1f)]
        public float dropChance;    // 드롭 확률 (0~1)
    }

    // ✅ 탐사 장소별 전리품 테이블을 정의하는 ScriptableObject
    [CreateAssetMenu(fileName = "NewExplorationLootTable", menuName = "ZAProject/Exploration Loot Table")]
    public class ExplorationLootTable : ScriptableObject
    {
        public string LocationName;                 // 장소 이름 (예: Hospital)
        public List<LootEntry> lootEntries;         // 전리품 목록 리스트
    }
}