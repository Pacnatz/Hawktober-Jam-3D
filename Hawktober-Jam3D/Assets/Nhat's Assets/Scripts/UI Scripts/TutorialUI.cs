using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialUI : MonoBehaviour
{


    public void BackButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
