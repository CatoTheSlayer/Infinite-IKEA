using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
public class StartMenuEvents : MonoBehaviour
{
   private UIDocument _dokument;

    private Button _button;

    private void Awake()
    {
        _dokument = GetComponent<UIDocument>();

        _button = _dokument.rootVisualElement.Q("StartSpilKnap") as Button;
        _button.RegisterCallback<ClickEvent>(OnStartSpilClick);
    }

    private void OnDisable()
    {
        _button.UnregisterCallback<ClickEvent>(OnStartSpilClick);
    }

    private void OnStartSpilClick(ClickEvent evt)
    {
        Debug.Log("You trrykkede på start spil Knappen");
        SceneManager.LoadScene("LevelMenu");
    }
}
