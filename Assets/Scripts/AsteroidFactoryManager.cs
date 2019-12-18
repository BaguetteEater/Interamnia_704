using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidFactoryManager : MonoBehaviour
{
    public GameObject asteroidFactoryPrefab;
    public GameObject[] asteroidPrefabs;

    public int maximumAsteroids;
    public int numberZones;

    private Bounds genBounds;
    
    private GameObject[] factories;

    private Vector3 zoneScale;

    private int maximumPerZone;
    private const int NB_FACTORIES = 3;

    private int inFactoryNumber = -1;

    // Start is called before the first frame update
    void Start()
    {
        genBounds = GetComponent<MeshRenderer>().bounds;
        factories = new GameObject[3];

        zoneScale = new Vector3(
            genBounds.size.x, 
            genBounds.size.y, 
            genBounds.size.z/numberZones
        );

        maximumPerZone = maximumAsteroids/numberZones;

        Vector3 triggeredZonePosition = this.transform.position;
        factories[0] = CreateFactory(this.transform.position, 0, 0);
        factories[1] = CreateFactory(this.transform.position, genBounds.size.z/numberZones, 1);
        factories[2] = CreateFactory(this.transform.position, -genBounds.size.z/numberZones, 2);
    }

    public void SetInFactoryNumber(int inFactoryNumber)
    {
        this.inFactoryNumber = inFactoryNumber;
    }

    public void ShipLeaveFactory(int outFactoryNumber)
    {
        if(this.inFactoryNumber == -1)
        {
            Debug.LogError("Factory Manager : ship leaving a factory whitout getting out of one");
        }
        else 
        {
            float zOffset = genBounds.size.z/numberZones;

            // If the factory the ship is getting out of is positionned backward (z ship's position is going to the -infinite)
            if (factories[this.inFactoryNumber].transform.position.z < factories[outFactoryNumber].transform.position.z)
            {
                zOffset = -zOffset;
            }

            int idxDestroyedFactory = this.SearchAsteroidFactory(this.inFactoryNumber, outFactoryNumber);

            if (idxDestroyedFactory != -1)
            {
                Destroy(this.factories[idxDestroyedFactory].GetComponent<AsteroidFactory>().GetAsteroidParent());
                Destroy(this.factories[idxDestroyedFactory]);
                this.factories[idxDestroyedFactory] = this.CreateFactory(this.factories[inFactoryNumber].transform.position, zOffset, idxDestroyedFactory);
            }
        }
    }

    public GameObject CreateFactory(Vector3 triggeredZonePosition, float zOffset, int place)
    {
        GameObject factory = Instantiate(
            asteroidFactoryPrefab,
            new Vector3 (
                triggeredZonePosition.x,
                triggeredZonePosition.y,
                triggeredZonePosition.z + zOffset),
            Quaternion.identity
        ) as GameObject;

        GameObject asteroidParent = new GameObject("asteroids");
        asteroidParent.transform.position = this.transform.position;

        factory.GetComponent<AsteroidFactory>().SetAsteroidsPrefab(this.asteroidPrefabs);
        factory.GetComponent<AsteroidFactory>().SetAsteroidParent(asteroidParent);
        factory.GetComponent<AsteroidFactory>().SetScale(zoneScale);
        factory.GetComponent<AsteroidFactory>().SetMaximum(maximumPerZone);
        factory.GetComponent<AsteroidFactory>().SetPlace(place);

        AsteroidFactoryManager manager = this;
        factory.GetComponent<AsteroidFactory>().SetAsteroidFactoryManager(manager);

        factory.GetComponent<AsteroidFactory>().GenerateAsteroids();

        return factory;
    }

    private int SearchAsteroidFactory(int inFactoryNumber, int outFactoryNumber)
    {
        int res = -1;
        for (int i = 0; i < this.factories.Length; i++)
        {
            if(i != inFactoryNumber && i != outFactoryNumber)
            {
                res = i;
            }
        }

        return res;
    }
}
