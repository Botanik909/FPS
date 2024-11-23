using UnityEngine;

public class ObjectsFinder : MonoBehaviour
{

    [SerializeField] private float rayDistance;

    [SerializeField] private ActionTextHandler actionText;
    [SerializeField] private TableController table;
        
    private Camera _camera;
    public Transform ObjectTransform { get; private set; }

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        var ray = _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            if (hit.collider.CompareTag("Object"))
            {
                ObjectTransform = hit.collider.transform;
                if(!actionText.ActionText.IsActive())
                    actionText.ShowActionText("Нажмите 'e' чтобы подобрать предмет");
            }
            else if (hit.collider.CompareTag("Door") || hit.collider.CompareTag("MicrowaveDoor"))
            {
                ObjectTransform = hit.collider.transform;
                DoorsRotator door = ObjectTransform.GetComponent<DoorsRotator>();
                if(!actionText.ActionText.IsActive() && !door.IsOpened)
                    actionText.ShowActionText("Нажмите 'e' чтобы открыть дверь");
                else if(!actionText.ActionText.IsActive() && door.IsOpened)
                    actionText.ShowActionText("Нажмите 'e' чтобы закрыть дверь");
            }
            else if (hit.collider.CompareTag("Microwave"))
            {
                ObjectTransform = hit.collider.transform;
                actionText.ShowActionText("Нажмите 'e' чтобы включить микроволновку");
            }
            
            else if (hit.collider.CompareTag("Sofa") && table.IsFoodOnTable)
            {
                ObjectTransform = hit.collider.transform;
                actionText.ShowActionText("Нажмите 'e' чтобы посмотреть телевизор");
            }
            
            else if (hit.collider.CompareTag("Bed"))
            {
                ObjectTransform = hit.collider.transform;
                actionText.ShowActionText("Нажмите 'e' чтобы спрятаться под кровать");
            }
            
            else
            {
                ObjectTransform = null;
                actionText.HideActionText();
            }
        }

        /*ObjectTransform = null;
        actionText.HideActionText();*/
    }
}