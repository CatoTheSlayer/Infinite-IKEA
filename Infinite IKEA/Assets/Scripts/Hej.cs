using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

[RequireComponent(typeof(InputSystemUIInputModule))]
public class EnsureActionAssetEnabled : MonoBehaviour
{ 
    public UnityEvent OnEnabled = new UnityEvent();
    private IEnumerator Start()
    {
        var module = GetComponent<InputSystemUIInputModule>();
        // Debug.Log($"Asset enabled? {module.actionsAsset.enabled}"); // shows true

        yield return null;
        // Debug.Log($"Asset enabled? {module.actionsAsset.enabled}") // shows false!
        module.actionsAsset.Enable();
        OnEnabled.Invoke();
    }
}