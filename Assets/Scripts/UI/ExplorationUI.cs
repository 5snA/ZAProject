using UnityEngine;
using UnityEngine.UI;

public class ExplorationUI : MonoBehaviour
{
    // 각각의 탐사 버튼들
    public Button policeButton;
    public Button hospitalButton;
    public Button fireStationButton;
    public Button marketButton;
    public Button parkButton;
    public Button warehouseButton;
    public Button schoolButton;

    // 버튼들이 들어있는 부모 Panel (숨기기/보이기 용도)
    public GameObject explorationPanel;

    void Start()
    {
        // 각 버튼에 클릭 이벤트 등록
        policeButton.onClick.AddListener(() => OnButtonClicked("Police Station"));
        hospitalButton.onClick.AddListener(() => OnButtonClicked("Hospital"));
        fireStationButton.onClick.AddListener(() => OnButtonClicked("Fire Station"));
        marketButton.onClick.AddListener(() => OnButtonClicked("Supermarket"));
        parkButton.onClick.AddListener(() => OnButtonClicked("Park"));
        warehouseButton.onClick.AddListener(() => OnButtonClicked("Warehouse"));
        schoolButton.onClick.AddListener(() => OnButtonClicked("School"));
    }

    // 버튼 클릭 시 탐사 진행 + UI 숨김
    void OnButtonClicked(string locationName)
    {
        // 탐사 시스템 호출
        ExplorationSystem.Instance.HandleExplorationFromUI(locationName);

        // 탐사 UI 패널 숨기기
        if (explorationPanel != null)
        {
            explorationPanel.SetActive(false);
        }
    }
}