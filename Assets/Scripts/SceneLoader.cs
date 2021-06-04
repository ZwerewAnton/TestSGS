using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    public void OpenNewScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
