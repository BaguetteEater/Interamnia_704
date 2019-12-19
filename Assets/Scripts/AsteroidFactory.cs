using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AsteroidFactory : MonoBehaviour
{
    private const int maximumScale = 5;

    private GameObject[] asteroidPrefabs;
    private GameObject asteroidParent;
    public Vector3 centreZone;
    private MeshCollider collider; 
    private Bounds genBounds;
    private Vector3 zoneCenter;
    private AsteroidFactoryManager manager;
    private int count;
    private int maximum;
    private int place;

    private void Start()
    {
        CreateAsteroid(new Vector3(
                   0,
                    -1,
                    20)
                );
    }

    public void GenerateAsteroids()
    {
        collider = this.gameObject.AddComponent<MeshCollider>();
        collider.convex = true;
        collider.isTrigger = true;

        genBounds = collider.bounds;
        count = 0;

        this.zoneCenter = this.transform.position;
        for (int i = 0; i < maximum; i++)
        {
            CreateAsteroid(new Vector3 (
                    UnityEngine.Random.Range(genBounds.min.x, genBounds.max.x),
                    UnityEngine.Random.Range(genBounds.min.y, genBounds.max.y),
                    UnityEngine.Random.Range(genBounds.min.z, genBounds.max.z)
                ));
        }

        count = maximum;
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.gameObject.CompareTag("spaceship"))
        {
            manager.ShipLeaveFactory(this.place);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("spaceship"))
        {
            manager.SetInFactoryNumber(this.place);
        }
    }

    private GameObject CreateAsteroid(Vector3 asteroidPosition)
    {
        GameObject asteroid = Instantiate(
                                    asteroidPrefabs[UnityEngine.Random.Range(0, asteroidPrefabs.Length)],
                                    asteroidPosition,
                                    Quaternion.identity,
                                    asteroidParent?.transform
                              ) as GameObject;

        int scale = UnityEngine.Random.Range(1, maximumScale);
        asteroid.transform.localScale = new Vector3(scale, scale, scale);

        return asteroid;
    }

    public void DestroyAsteroid(GameObject asteroid)
    {
        Destroy(asteroid);
        count--;
    }

    public void SetAsteroidsPrefab(GameObject[] asteroidPrefabs)
    {
        this.asteroidPrefabs = asteroidPrefabs;
    }

    public void SetAsteroidParent(GameObject asteroidParent)
    {
        this.asteroidParent = asteroidParent;
    }

    public GameObject GetAsteroidParent()
    {
        return this.asteroidParent;
    }

    public void SetScale(Vector3 scale)
    {
        this.gameObject.transform.localScale = scale;
    }

    public void SetMaximum(int maximum)
    {
        this.maximum = maximum;
    }

    public void SetAsteroidFactoryManager(AsteroidFactoryManager manager)
    {
        this.manager = manager;
    }

    public void SetPlace(int place)
    {
        this.place = place;
    }
}

