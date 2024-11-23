using UnityEngine;

public class MovingTowards : MonoBehaviour
{
    private void Update()
    {
        transform.Translate(new Vector3(0.0f, 0.0f, 1f * Time.deltaTime));
    }
}
