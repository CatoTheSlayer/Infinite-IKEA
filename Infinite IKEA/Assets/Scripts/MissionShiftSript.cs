using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MissionShiftSript : MonoBehaviour
{
    [SerializeField] private UIDocument _MissionMenuDokument;  
    private Button _StartMission1;
    private Button _StartMission2;
    private Button _StartBoss;
    MainManager mainManager;
    public int MissionSelect = 0;

    private void Awake()
    {
        _StartMission1 = _MissionMenuDokument.rootVisualElement.Q("StartMission1") as Button;
        _StartMission1.RegisterCallback<ClickEvent>(OnStartMission1);
    }
    void Start()
    {
        MainManager.Instance.MissionSelect = MissionSelect; // Link the inventory system's coin amount to the main manager's coin amount for global accesss

    }

    private void OnStartMission1(ClickEvent evt)
    {
        MainManager.Instance.MissionSelect = 1;
        SceneManager.LoadScene("First Level");
    }
}
