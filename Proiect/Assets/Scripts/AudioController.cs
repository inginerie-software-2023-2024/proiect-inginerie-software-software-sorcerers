using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public Toggle audioToggle;

    void Start()
    {
        audioToggle.GetComponent<Toggle>();

        if(AudioListener.volume == 0)
        {
            audioToggle.isOn = true;
        }
        else
        {
            audioToggle.isOn = false;
        }
    }

    public void ToggleAudioOnValueChanged(bool audioIn)
    {
        if(audioIn == true)
        {
            AudioListener.volume = 0;
            
        }
        else
        {
            AudioListener.volume = 1;
        }

    }

}
