using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidFactory : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public int maximum;

    private Bounds bounds;
    private int count;

    // Start is called before the first frame update
    void Start()
    {
        bounds = GetComponent<Renderer>().bounds;
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 center = this.transform.position;

        if (count < maximum)
        {
            GameObject asteroid = Instantiate(
                asteroidPrefab,
                center + new Vector3(
                    Random.Range(bounds.min.x, bounds.max.x),
                    Random.Range(bounds.min.y, bounds.max.y),
                    Random.Range(bounds.min.z, bounds.max.z)
                ),
                Quaternion.identity, null
            ) as GameObject;

            AsteroidFactory asteroidFactory = this;
            asteroid.GetComponent<AsteroidController>().SetFactory(ref asteroidFactory);

            count++;
        }
    }

    public bool IsOutOfBounds(GameObject gameObject)
    {
        Debug.Log(this.bounds.extents);
        return !this.bounds.Contains(gameObject.transform.position);
    }

    public void DestroyAsteroid(GameObject asteroid)
    {
        Destroy(asteroid);
        count--;
    }
}
