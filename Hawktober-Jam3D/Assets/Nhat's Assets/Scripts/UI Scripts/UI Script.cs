using UnityEngine;
using TMPro;

public class UIScript : MonoBehaviour
{

    private Vector3 activeContainerPosition = new Vector3(200, 150, 0);
    private Vector3 inactiveContainerPosition = new Vector3(-200, 150, 0);
    private float containerMoveSpeed = 100f;

    //M1911 Container
    public RectTransform gunContainer;
    [HideInInspector]
    private TMP_Text gunTMPro;
    public string gunText;
    private Vector3 gunVelocity = Vector3.zero;

    public int ammoArrayPosition = 0;

    void Start()
    {
        gunContainer = gunContainer.GetComponent<RectTransform>();
        gunTMPro = gunContainer.GetChild(0).GetComponent<TMP_Text>();
    }
    void Update()
    {
        switch (ammoArrayPosition)
        {
            case 0:
                gunContainer.anchoredPosition3D = Vector3.SmoothDamp(gunContainer.anchoredPosition3D,
                    inactiveContainerPosition, ref gunVelocity, containerMoveSpeed * Time.deltaTime);
                break;
            case 1:
                gunContainer.anchoredPosition3D = Vector3.SmoothDamp(gunContainer.anchoredPosition3D,
                    activeContainerPosition, ref gunVelocity, containerMoveSpeed * Time.deltaTime);
                break;
        }

        gunTMPro.text = gunText;


    }
}
