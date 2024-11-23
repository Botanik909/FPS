using UnityEngine;

public class ObjectsPicker : MonoBehaviour
{
    private Transform _pickedObject;

    [SerializeField] private ActionTextHandler actionText;
    [SerializeField] private Transform mainObjectsParent;
    [SerializeField] private Transform camera;

    private ObjectsFinder _objectsFinder;
    private bool _isObjectPicked;

    private void Start()
    {
        _objectsFinder = GetComponent<ObjectsFinder>();
    }

    private void Update()
    {
        if (!_isObjectPicked && actionText.gameObject.activeSelf && _objectsFinder.ObjectTransform != null &&
            _objectsFinder.ObjectTransform.CompareTag("Object") && Input.GetKeyDown(KeyCode.E))
        {
            _pickedObject = _objectsFinder.ObjectTransform;
            _pickedObject.SetParent(transform);
            _pickedObject.GetComponent<Rigidbody>().isKinematic = true;
            _pickedObject.transform.position = new Vector3(_pickedObject.transform.position.x, camera.position.y - 0.3f,
                _pickedObject.transform.position.z);
            _isObjectPicked = true;
            actionText.ShowActionText("Нажмите 'e' чтобы выбросить предмет");
        }
        
        else if (_isObjectPicked && Input.GetKeyDown(KeyCode.E))
        {
            _pickedObject.GetComponent<Rigidbody>().isKinematic = false;
            _isObjectPicked = false;
            _pickedObject.SetParent(mainObjectsParent);
            _pickedObject = null;
        }
    }
}