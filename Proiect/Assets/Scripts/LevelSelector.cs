using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public Button myButton;

    public void LoadLevel()
    {
        Text buttonText = myButton.GetComponentInChildren<Text>();
        string textValue = buttonText.text;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + ((int)textValue[6]) - 48);
    }
}
