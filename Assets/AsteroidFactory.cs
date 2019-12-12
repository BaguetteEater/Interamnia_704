using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AsteroidFactory : MonoBehaviour
{
    public GameObject[] asteroidPrefabs;
    public GameObject asteroidsParent;
    public int maximum;

    private MeshCollider collider; 
    private Bounds bounds;
    private int count;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<MeshCollider>();
        bounds = collider.bounds;
        count = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 center = this.transform.position;
        
        if (count < maximum)
        {
            GameObject asteroid = Instantiate(
                asteroidPrefabs[UnityEngine.Random.Range(0, asteroidPrefabs.Length)],
                center + new Vector3(
                    UnityEngine.Random.Range(bounds.min.x, bounds.max.x),
                    UnityEngine.Random.Range(bounds.min.y, bounds.max.y),
                    UnityEngine.Random.Range(bounds.min.z, bounds.max.z)
                ),
                Quaternion.identity,
                asteroidsParent?.transform
            ) as GameObject;

            AsteroidFactory asteroidFactory = this;
            asteroid.GetComponent<AsteroidController>().SetFactory(ref asteroidFactory);

            count++;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Asteroid"))
        {
            DestroyAsteroid(other.gameObject);
        }
    }

    public bool IsOutOfBounds(GameObject gameObject)
    {
        return !this.bounds.Contains(gameObject.transform.position + GetComponentInParent<Rigidbody>().transform.position);
    }

    public void DestroyAsteroid(GameObject asteroid)
    {
        Destroy(asteroid);
        count--;
    }
}
