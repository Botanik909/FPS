using System;
using UnityEngine;


public class TableController : MonoBehaviour
{
    public bool IsFoodOnTable { get; private set; }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Object"))
            IsFoodOnTable = true;
    }
    
}
