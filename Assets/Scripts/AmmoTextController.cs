using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoTextController : MonoBehaviour
{
    private SpaceshipInput spaceshipInput;
    private TextMesh textMesh;

    // Start is called before the first frame update
    void Start()
    {
        spaceshipInput = GetComponentInParent<SpaceshipInput>();
        textMesh = GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        int ammo = spaceshipInput.laserAmmo;


        if (ammo >= 0)
        {
            textMesh.text = ammo + "";

            if (ammo == 0)
            {
                textMesh.color = Color.red;
            }
            else if (ammo > 10)
            {
                textMesh.color = Color.green;
            }
            else
            {
                textMesh.color = new Color(250, 115, 0);
            }
        }
    }
}
