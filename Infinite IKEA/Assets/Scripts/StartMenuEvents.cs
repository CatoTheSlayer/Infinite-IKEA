using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
public class StartMenuEvents : MonoBehaviour
{
   [SerializeField] private UIDocument _StartMenuDokument;  
    [SerializeField] private UIDocument _SettingsMenuDokument;
    //Referencer til de forskellige knapper på start siden
    private Button _StartButton;
    private Button _QuitButton;
    private Button _SettingsButton;

    private void Awake()
    {
       _StartMenuDokument.rootVisualElement.style.display = DisplayStyle.Flex;
        _SettingsMenuDokument.rootVisualElement.style.display = DisplayStyle.None;

        //Laver start start knap, og for den til at køre metoden OnStartSpilClick når der trykkes på knappen Start
        _StartButton = _StartMenuDokument.rootVisualElement.Q("StartSpilKnap") as Button;
        _StartButton.RegisterCallback<ClickEvent>(OnStartSpilClick);

        //Sker det samme untagen den kører metoden OnQuitSpilClick når der trykkes på knappen Quit
        _QuitButton = _StartMenuDokument.rootVisualElement.Q("QuitButton") as Button;
        _QuitButton.RegisterCallback<ClickEvent>(OnQuitSpilClick);

        _SettingsButton = _StartMenuDokument.rootVisualElement.Q("SettingsButton") as Button;
        _SettingsButton.RegisterCallback<ClickEvent>(OnSettingClick);

        UnityEngine.Cursor.lockState = UnityEngine.CursorLockMode.None; // Unlock the cursor for UI interaction
        UnityEngine.Cursor.visible = true; // Make the cursor visible
    }    

    private void OnDisable()
    {
        _StartButton.UnregisterCallback<ClickEvent>(OnStartSpilClick);
        _QuitButton.UnregisterCallback<ClickEvent>(OnQuitSpilClick);
    }


    //
    private void OnStartSpilClick(ClickEvent evt)
    {
        Debug.Log("You trrykkede på start spil Knappen");
        SceneManager.LoadScene("LevelMenu");
    }

    private void OnQuitSpilClick(ClickEvent evt)
    {
        Debug.Log("You trykkede på Quit spil Knappen");
        Application.Quit();
    }
    
    private void OnSettingClick(ClickEvent evt)
    {
         Debug.Log("You trykkede på Setting Knappen");
         _StartMenuDokument.rootVisualElement.style.display = DisplayStyle.None;
        _SettingsMenuDokument.rootVisualElement.style.display = DisplayStyle.Flex;
    }
}
