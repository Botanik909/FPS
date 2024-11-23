using UnityEngine;

public class DoorsRotator : MonoBehaviour
{
    [SerializeField] private float rotationDuration;

    [SerializeField] private ObjectsFinder objects;
    [SerializeField] private ActionTextHandler actionText;
    [SerializeField] private MicrowaveController microwave;

    private float _elapsedTime;

    private bool _isOpening;
    public bool IsOpened { get; private set; }
    private Quaternion _doorEndRotation;
    private Quaternion _doorStartRotation;

    private void Start()
    {

        _doorStartRotation = transform.rotation;

        _doorEndRotation = _doorStartRotation * Quaternion.Euler(0f, 120.0f, 0.0f);
    }

    private void Update()
    {
        if (actionText.ActionText.IsActive() && objects.ObjectTransform != null &&
            (objects.ObjectTransform.CompareTag("Door") || objects.ObjectTransform.CompareTag("MicrowaveDoor")) &&
            objects.ObjectTransform == transform &&
            Input.GetKeyDown(KeyCode.E))
        {
            if(objects.ObjectTransform.CompareTag("MicrowaveDoor") && !microwave.IsWorking)
                OpenDoors();
            else if(objects.ObjectTransform.CompareTag("Door"))
                OpenDoors();
        }
    }

    private void FixedUpdate()
    {
        if (_isOpening)
        {
            _elapsedTime += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(_doorStartRotation, _doorEndRotation,
                _elapsedTime / rotationDuration);
            if (_elapsedTime >= rotationDuration)
            {
                if (!IsOpened) IsOpened = true;
                else IsOpened = false;
                _doorEndRotation = _doorStartRotation;
                _doorStartRotation = transform.rotation;
                
                _isOpening = false;
            }
        }
    }

    public void OpenDoors()
    {
        _elapsedTime = 0f;
        _isOpening = true;
    }

}