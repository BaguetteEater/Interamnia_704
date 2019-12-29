using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonsController : MonoBehaviour
{
    public Button gameButton;

    void Start()
    {
        gameButton.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.L))
        {
            SceneManager.LoadScene("Game");
        }
    }
}
