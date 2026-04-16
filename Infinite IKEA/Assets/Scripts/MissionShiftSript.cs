using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MissionShiftSript : MonoBehaviour
{
    [SerializeField] private UIDocument _MissionMenuDokument;  
    private Button _StartMission1;
    private Button _StartMission2;
    private Button _StartBoss;
    public mision_colectoin mision_Colectoin;

    private void Awake()
    {
        _StartMission1 = _MissionMenuDokument.rootVisualElement.Q("StartMission1") as Button;
        _StartMission1.RegisterCallback<ClickEvent>(OnStartMission1);
    }

    private void OnStartMission1(ClickEvent evt)
    {
        mision_Colectoin.Colection();
        SceneManager.LoadScene("First Level");
        
    }
}
