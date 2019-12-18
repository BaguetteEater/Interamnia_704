using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AsteroidController : MonoBehaviour
{
    private AudioSource explosionSound;

    private void Start()
    {
        explosionSound = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Laser"))
        {
            AudioSource.PlayClipAtPoint(explosionSound.clip, this.transform.position);
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
        else if (other.gameObject.CompareTag("spaceship"))
        {
            AudioSource.PlayClipAtPoint(explosionSound.clip, this.transform.position);
            Destroy(other.gameObject);
            Destroy(this.gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
