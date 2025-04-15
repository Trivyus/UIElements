using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private SceneNames _sceneNames;

    public void ChangeScene()
    {
        SceneManager.LoadScene(_sceneNames.ToString());
    }
}
