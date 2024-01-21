using UnityEngine;
using UnityEngine.SceneManagement;
public class StartMenuNavigateSettings : MonoBehaviour
{

    public void GoToSettingsGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

}
