using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
public class StartMenuEvents : MonoBehaviour
{
   private UIDocument _dokument;
    //Referencer til de forskellige knapper på start siden
    private Button _StartButton;
    private Button _QuitButton;

    private void Awake()
    {
        _dokument = GetComponent<UIDocument>();

        //Laver start start knap, og for den til at køre metoden OnStartSpilClick når der trykkes på knappen Start
        _StartButton = _dokument.rootVisualElement.Q("StartSpilKnap") as Button;
        _StartButton.RegisterCallback<ClickEvent>(OnStartSpilClick);

        //Sker det samme untagen den kører metoden OnQuitSpilClick når der trykkes på knappen Quit
        _QuitButton = _dokument.rootVisualElement.Q("QuitButton") as Button;
        _QuitButton.RegisterCallback<ClickEvent>(OnQuitSpilClick);
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
        Debug.Log("You trrykkede på Quit spil Knappen");
        Application.Quit();
    }
}
