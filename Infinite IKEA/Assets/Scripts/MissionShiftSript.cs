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

        _StartMission2 = _MissionMenuDokument.rootVisualElement.Q("StartMission2") as Button;
        _StartMission2.RegisterCallback<ClickEvent>(OnStartMission2);

        _StartBoss = _MissionMenuDokument.rootVisualElement.Q("StartBoss") as Button;
        _StartBoss.RegisterCallback<ClickEvent>(OnStartBoss);

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

    private void OnStartMission2(ClickEvent evt)
    {
        MainManager.Instance.MissionSelect = 2;
        SceneManager.LoadScene("First Level");
    }

    private void OnStartBoss(ClickEvent evt)
    {
        SceneManager.LoadScene("BossFight");
    }
}
