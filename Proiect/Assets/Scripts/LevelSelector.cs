using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] int SceneID;

    public void LoadLevel()
    {
        SceneManager.LoadScene(SceneID);
    }
}
