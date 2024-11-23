using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class MicrowaveController : MonoBehaviour
{
    [SerializeField] private DoorsRotator microwaveDoor;
    [SerializeField] private ActionTextHandler actionText;
    [SerializeField] private GameObject microwaveLight;
    [SerializeField] private float workingTime;

    public bool IsWorking { get; private set; }
    private bool _isFull;
    private bool _isReady;

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Object"))
            _isFull = true;
    }

    private void OnCollisionExit(Collision other)
    {
        _isFull = false;
    }

    private void Update()
    {
        if (!IsWorking && !_isReady && actionText.gameObject.activeSelf && _isFull && !microwaveDoor.IsOpened)
        {
            IsWorking = true;
            microwaveLight.SetActive(true);
            StartCoroutine(MicrowaveDisabler());
        }
    }

    private IEnumerator MicrowaveReuseEnabler()
    {
        yield return new WaitForSeconds(10);
        _isReady = false;
    }

    private IEnumerator MicrowaveDisabler()
    {
        yield return new WaitForSeconds(workingTime);
        IsWorking = false;
        microwaveLight.SetActive(false);
        _isReady = true;
        StartCoroutine(MicrowaveReuseEnabler());
    }
}
