using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuUI : MonoBehaviour
{
    private Vector3 activePlayContainerPosition = new Vector3(0, 0, 0);
    private Vector3 activeInfoContainerPosition = new Vector3(0, -200, 0);
    private Vector3 activeQuitContainerPosition = new Vector3(0, -400, 0);
    private float containerMoveSpeed = 20f;

    private Vector3 playVelocity = Vector3.zero;
    private Vector3 infoVelocity = Vector3.zero;
    private Vector3 quitVelocity = Vector3.zero;

    private bool moveButtons;

    public Animator shovelAnim;

    public RectTransform playButton;
    public RectTransform infoButton;
    public RectTransform quitButton;


    private void Start()
    {
        Debug.Log("Start");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        StartCoroutine(StartMainMenu());
    }

    private void Update()
    {
        if (moveButtons)
        {
            playButton.anchoredPosition3D = Vector3.SmoothDamp(playButton.anchoredPosition3D,
                    activePlayContainerPosition, ref playVelocity, containerMoveSpeed * Time.deltaTime);
            infoButton.anchoredPosition3D = Vector3.SmoothDamp(infoButton.anchoredPosition3D,
                    activeInfoContainerPosition, ref infoVelocity, containerMoveSpeed * Time.deltaTime);
            quitButton.anchoredPosition3D = Vector3.SmoothDamp(quitButton.anchoredPosition3D,
                    activeQuitContainerPosition, ref quitVelocity, containerMoveSpeed * Time.deltaTime);
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Game Scene");
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    private IEnumerator StartMainMenu()
    {
        yield return new WaitForSeconds(.75f);
        shovelAnim.Play("Play");
        yield return new WaitForSeconds(1f);
        moveButtons = true;
    }

    private void HideMainMenu(bool hide)
    {
        if (hide)
        {

        }
    }
}
