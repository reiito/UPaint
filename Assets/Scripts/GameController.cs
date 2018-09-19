using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class GameController : MonoBehaviour
{
    public static GameController gameInstance;

    const float endUIPos = -161;

    public CinemachineVirtualCamera vCamera;
    public GameObject paintPalete;

    public float switchTime = 180f;
    public float zoomSpeed = 10;
    public float endCamSize = 2f;

    float startCamSize;
    float startUIPos;

    bool platformerMode;
    public bool PlatformerMode { get { return platformerMode; } }

    bool zooming;
    public bool Zooming { get { return zooming; } }

    public Text controlText;

    bool startTimerRunning;

    PlayerMovement player;

    PlatformerManager platformerManager;

    private void Awake()
    {
        gameInstance = this;
    }

    private void Start()
    {
        player = PlayerMovement.playerInstance;
        platformerManager = PlatformerManager.platformerManagerInstance;

        player.gameObject.SetActive(false);

        startCamSize = vCamera.m_Lens.OrthographicSize;
        startUIPos = paintPalete.transform.position.x;

        controlText.gameObject.SetActive(false);

        StartCoroutine(BaitAndSwitch());
        startTimerRunning = true;
        ToggleLevel(false);
    }

    private void Update()
    {
        if (!startTimerRunning)
        {
            if (!zooming)
            {
                if (Input.GetKeyDown("p"))
                {
                    if (!platformerMode)
                    {
                        player.transform.position = platformerManager.RandomSpawnPosition();
                    }
                    else
                    {
                        player.gameObject.SetActive(false);
                    }
                    platformerMode = !platformerMode;
                }
            }

            if (platformerMode)
            {
                ZoomIn();
            }
            else
            {
                ZoomOut();
            }
        }

        if (Input.GetKeyDown("escape"))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
      Application.Quit();
#endif
        }
    }

    IEnumerator BaitAndSwitch()
    {
        yield return new WaitForSeconds(switchTime);

        platformerMode = true;
        startTimerRunning = false;
        controlText.gameObject.SetActive(true);
    }


    float zoomOffset = 0.2f;
    void ZoomIn()
    {
        if (vCamera.m_Lens.OrthographicSize != endCamSize)
        {
            vCamera.m_Lens.OrthographicSize = Mathf.Lerp(vCamera.m_Lens.OrthographicSize, endCamSize, Mathf.SmoothStep(0.0f, 1.0f, Time.deltaTime * zoomSpeed));
            paintPalete.transform.position = new Vector2(Mathf.Lerp(paintPalete.transform.position.x, endUIPos, Mathf.SmoothStep(0.0f, 1.0f, Time.deltaTime * zoomSpeed)), paintPalete.transform.position.y);
        }

        if (vCamera.m_Lens.OrthographicSize <= endCamSize + zoomOffset)
        {
            paintPalete.SetActive(false);
            zooming = false;
            ToggleLevel(true);
        }
        else
        {
            zooming = true;
        }
    }

    void ZoomOut()
    {
        if (vCamera.m_Lens.OrthographicSize != startCamSize)
        {
            vCamera.m_Lens.OrthographicSize = Mathf.Lerp(vCamera.m_Lens.OrthographicSize, startCamSize, Mathf.SmoothStep(0.0f, 1.0f, Time.deltaTime * zoomSpeed));
            paintPalete.transform.position = new Vector2(Mathf.Lerp(paintPalete.transform.position.x, startUIPos, Mathf.SmoothStep(0.0f, 1.0f, Time.deltaTime * zoomSpeed)), paintPalete.transform.position.y);
        }

        if (!paintPalete.activeInHierarchy)
            paintPalete.SetActive(true);

        if (vCamera.m_Lens.OrthographicSize >= startCamSize - zoomOffset)
        {
            paintPalete.SetActive(true);
            zooming = false;
            ToggleLevel(false);
        }
        else
        {
            zooming = true;
        }
    }

    void ToggleLevel(bool activate)
    {
        player.gameObject.SetActive(activate);
        platformerManager.TogglePlatformerElements(activate);
    }
}
