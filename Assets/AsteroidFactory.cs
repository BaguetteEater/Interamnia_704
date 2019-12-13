using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AsteroidFactory : MonoBehaviour
{
    public GameObject[] asteroidPrefabs;
    public GameObject asteroidsParent;

    public Camera camera;

    public int maximum;

    private MeshCollider collider; 

    private Bounds genBounds;
    private Bounds noGenBounds;

    private Vector3 worldCenter;

    private int count;
    private int rayCastLength = 100;

    private void Awake() {
        collider = GetComponent<MeshCollider>();

        genBounds = collider.bounds;
        count = 0;
        
        noGenBounds = GetComponentInChildren<MeshRenderer>().bounds;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.worldCenter = this.transform.position;
        for (int i = 0; i < maximum; i++)
        {
            Instantiate(
                asteroidPrefabs[UnityEngine.Random.Range(0, asteroidPrefabs.Length)],
                this.worldCenter + new Vector3(
                    UnityEngine.Random.Range(genBounds.min.x, genBounds.max.x),
                    UnityEngine.Random.Range(genBounds.min.y, genBounds.max.y),
                    UnityEngine.Random.Range(genBounds.min.z, genBounds.max.z)
                ),
                Quaternion.identity,
                asteroidsParent?.transform
            );
        }

        count = maximum;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.worldCenter = this.transform.position;
        if (count < maximum)
        {
            Vector3 asteroidPosition = this.worldCenter + new Vector3(
                    UnityEngine.Random.Range(genBounds.min.x, genBounds.max.x),
                    UnityEngine.Random.Range(genBounds.min.y, genBounds.max.y),
                    UnityEngine.Random.Range(genBounds.min.z, genBounds.max.z)
            );

            if (IsOutOfNoGenZone(asteroidPosition))
            {
                GameObject asteroid = CreateAsteroid(asteroidPosition);
                AsteroidFactory asteroidFactory = this;
                asteroid.GetComponent<AsteroidController>().SetFactory(ref asteroidFactory);
            }

            count++;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Asteroid"))
        {
            DestroyAsteroid(other.gameObject);
        }
    }

    private GameObject CreateAsteroid(Vector3 asteroidPosition)
    {
        return Instantiate(
                    asteroidPrefabs[UnityEngine.Random.Range(0, asteroidPrefabs.Length)],
                    asteroidPosition,
                    Quaternion.identity,
                    asteroidsParent?.transform
                ) as GameObject;
    }

    private bool IsOutOfNoGenZone(Vector3 asteroidPosition)
    {
        if (noGenBounds.Contains(asteroidPosition))
        {
            return false;
        }
        
        return true;
    }

    public void DestroyAsteroid(GameObject asteroid)
    {
        Destroy(asteroid);
        count--;
    }
}

