using TMPro;
using UnityEngine;


public class ActionTextHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI actionText;

    public TextMeshProUGUI ActionText => actionText;

    private Camera _camera;

    public bool IsTextShown { get; private set; }

    private void Start()
    {
        _camera = Camera.main;
    }

    public void ShowActionText(string text)
    {
        HandleActionText(text);
        IsTextShown = true;
    }

    public void HideActionText()
    {
        actionText.gameObject.SetActive(false);
        IsTextShown = false;
    }

    private void HandleActionText(string text)
    {
        actionText.text = text;
        actionText.gameObject.SetActive(true);
    }
}
