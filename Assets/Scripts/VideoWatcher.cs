using System;
using System.Collections;
using UnityEngine;

public class VideoWatcher : MonoBehaviour
{
    [SerializeField] private GameObject video;
    [SerializeField] private GameObject actionText;
    [SerializeField] private TableController table;

    [SerializeField] private Transform character;
    [SerializeField] private Transform watchPoint;

    [SerializeField] private AudioSource knockSound;
    [SerializeField] private float knockSoundDelay;

    [SerializeField] private float walkDelay;
    [SerializeField] private GameObject man;

    private bool _isWatching;
    private ObjectsFinder _objectsFinder;

    public bool IsKnockPlayed { get; private set; }

    private void Start()
    {
        _objectsFinder = character.GetComponent<ObjectsFinder>();
    }

    private void Update()
    {
        if (!_isWatching && _objectsFinder.ObjectTransform != null &&
            _objectsFinder.ObjectTransform.CompareTag("Sofa") && table.IsFoodOnTable && actionText.activeSelf &&
            Input.GetKeyDown(KeyCode.E))
        {
            video.SetActive(true);
            _isWatching = true;
            character.position = watchPoint.position;
            StartCoroutine(WalkDelay());
            StartCoroutine(KnockSoundPlayer());
        }
    }

    private IEnumerator WalkDelay()
    {
        yield return new WaitForSeconds(walkDelay);
        man.SetActive(true);
    }

    private IEnumerator KnockSoundPlayer()
    {
        yield return new WaitForSeconds(knockSoundDelay);
        knockSound.Play();
        IsKnockPlayed = true;
        StartCoroutine(ShutDownTV());
    }


    private IEnumerator ShutDownTV()
    {
        yield return new WaitForSeconds(5);
        video.SetActive(false);
    }
}
