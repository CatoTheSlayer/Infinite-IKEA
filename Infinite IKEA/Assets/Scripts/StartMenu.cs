using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
   private UIDocument _document;

   private Button _button;

   private void Awake()
    {
         _document = GetComponent<UIDocument>();

         _button = _document.rootVisualElement.Q("StartGame") as Button;
         _button.RegisterCallback<ClickEvent>(OnPlayGameClick);
        
    }

    private void ODisable()
    {
        _button.UnregisterCallback<ClickEvent>(OnPlayGameClick);
    }

    private void OnPlayGameClick(ClickEvent evt)
    {
        Debug.Log("You Pressed the Start button");
        SceneManager.LoadScene("LevelMenu");
    }
}
