using System.Collections;
using UnityEngine;

public class BedHider : MonoBehaviour
{
    [SerializeField] private ActionTextHandler actionText;
    [SerializeField] private Transform hidedPoint;
    [SerializeField] private Transform releasePoint;
    [SerializeField] private FirstPersonMovement fpm;
    [SerializeField] private Transform character;
    [SerializeField] private FirstPersonLook fpl;
    [SerializeField] private VideoWatcher videoWatcher;
    [SerializeField] private float breakingSoundDelay;
    [SerializeField] private AudioSource door;
    [SerializeField] private float screamerDelay;
    [SerializeField] private AudioSource screamerSound;
    [SerializeField] private GameObject screamerImage;
    [SerializeField] private GameObject spawningMan;
    [SerializeField] private float manSpawningDelay = 10;

    private float _timer;

    private ObjectsFinder _objectsFinder;
    private bool _isHided;

    private void Start()
    {
        _objectsFinder = fpm.GetComponent<ObjectsFinder>();
    }

    private void Update()
    {
        if (videoWatcher.IsKnockPlayed)
        {
            _timer += Time.deltaTime;
        }
        
        if(_isHided)
            actionText.ShowActionText("Нажмите 'e' чтобы выйти из под кровати");
        
        if (!_isHided && _objectsFinder.ObjectTransform != null && _objectsFinder.ObjectTransform.CompareTag("Bed") &&
            actionText.gameObject.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            _timer = 0;
            _isHided = true;
            character.position = hidedPoint.transform.position;
            character.rotation = hidedPoint.rotation;
            character.GetComponent<Rigidbody>().isKinematic = true;
            fpl.transform.localRotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f));
            fpl.enabled = false;
            fpm.enabled = false;
            if (videoWatcher.IsKnockPlayed)
            {
                StartCoroutine(BreakingSoundPlayer());
                StartCoroutine(SpawnMan());
            }
        }

        else if (_isHided && actionText.gameObject.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            character.GetComponent<Rigidbody>().isKinematic = false;
            fpl.enabled = true;
            fpm.enabled = true;
            character.position = releasePoint.position;
            _isHided = false;
            if (_timer < screamerDelay && videoWatcher.IsKnockPlayed)
            {
                screamerImage.SetActive(true);
                screamerSound.Play();
            }
        }
    }

    private IEnumerator LastScreamer()
    {
        yield return new WaitForSeconds(5);
            screamerImage.SetActive(true);
            screamerSound.Play();
    }

    private IEnumerator SpawnMan()
    {
        yield return new WaitForSeconds(manSpawningDelay);
        spawningMan.SetActive(true);
        StartCoroutine(LastScreamer());
    }

    private IEnumerator BreakingSoundPlayer()
    {
        yield return new WaitForSeconds(breakingSoundDelay);
        door.Play();
    }
}