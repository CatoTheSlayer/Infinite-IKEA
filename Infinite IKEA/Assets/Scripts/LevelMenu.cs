using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
 private UIDocument _document;

   private Button _button;

   private void Awake()
    {
         _document = GetComponent<UIDocument>();

         _button = _document.rootVisualElement.Q("Level1") as Button;
         _button.RegisterCallback<ClickEvent>(OnPlayLevel);
        
    }

    private void ODisable()
    {
        _button.UnregisterCallback<ClickEvent>(OnPlayLevel);
    }

    private void OnPlayLevel(ClickEvent evt)
    {
        Debug.Log("You Pressed the level button");
        SceneManager.LoadScene("bord og stole rum");
    }

}
