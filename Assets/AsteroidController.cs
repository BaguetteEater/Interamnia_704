using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    private void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Laser"))
        {
            Debug.Log("laser");
            Destroy(other.gameObject);
            Destroy(this.gameObject); // todo: decrease maximum count
        }
    }
}
