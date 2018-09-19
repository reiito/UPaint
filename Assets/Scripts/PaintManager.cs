using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Serializable]
public enum PaintTools
{
    BRUSH, ERASER, CIRCLE, SQUARE
}

[System.Serializable]
public struct LockedTool
{
    public Button button;
    public GameObject lockObj;
}

public class PaintManager : MonoBehaviour
{
    public static PaintManager paintManagerInstance;
    const float DEFAULT_BRUSH_SIZE = 0.075f;

    public Transform defaultColor;
    public Transform defaultTool;

    public GameObject[] brushPrefabs;

    public GameObject sizeDropDown;
    public LockedTool[] lockedTools;

    public Material[] buttonMaterials;

    PaintBrush activeLine;

    Transform activeColorButton;
    Material brushMat;

    Transform activeToolButton;
    PaintTools selectedTool;

    int layerCount = 0;

    float brushSize = DEFAULT_BRUSH_SIZE;

    GameController gameController;

    private void Awake()
    {
        paintManagerInstance = this;
    }

    private void Start()
    {
        gameController = GameController.gameInstance;
        activeColorButton = defaultColor;
        SetCurrentColor(defaultColor);
        SetCurrentTool(defaultTool);

        foreach (LockedTool t in lockedTools)
        {
            t.button.enabled = false;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
        }

        if (!gameController.PlatformerMode)
        {
            switch (selectedTool)
            {
                case PaintTools.BRUSH:
                    PaintBrush();
                    break;
                case PaintTools.ERASER:
                    Erase();
                    break;
                case PaintTools.CIRCLE:
                    PaintCircle();
                    break;
                case PaintTools.SQUARE:
                    PaintSquare();
                    break;
            }
        }
    }

    public void SetCurrentTool(Transform toolButton)
    {
        if (activeToolButton)
        {
            activeToolButton.GetComponent<Image>().material = buttonMaterials[0];
        }
        activeToolButton = toolButton;
        activeToolButton.GetComponent<Image>().material = buttonMaterials[1];

        switch (activeToolButton.name)
        {
            case "Brush":
                selectedTool = PaintTools.BRUSH;
                break;
            case "Eraser":
                selectedTool = PaintTools.ERASER;
                break;
            case "Circle":
                selectedTool = PaintTools.CIRCLE;
                break;
            case "Square":
                selectedTool = PaintTools.SQUARE;
                break;

            default:
                Debug.Log(activeToolButton.name + " not implemented yet");
                break;
        }
    }

    public void SetBrushSize(Dropdown menu)
    {
        switch (menu.value)
        {
            case 0:
                brushSize = DEFAULT_BRUSH_SIZE;
                break;
            case 1:
                brushSize = DEFAULT_BRUSH_SIZE * 2;
                break;
            case 2:
                brushSize = DEFAULT_BRUSH_SIZE * 4;
                break;
        }
    }

    public void SetCurrentColor(Transform colorButton)
    {
        activeColorButton.GetChild(0).gameObject.SetActive(false);
        brushMat = colorButton.gameObject.GetComponent<Image>().material;
        activeColorButton = colorButton.transform;
        colorButton.GetChild(0).gameObject.SetActive(true);
    }

    void PaintBrush()
    {
        if (Input.GetMouseButtonDown(0))
        {
            layerCount++;
            activeLine = Instantiate(brushPrefabs[0], transform).GetComponent<PaintBrush>();
            activeLine.GetComponent<LineRenderer>().widthMultiplier = brushSize;
            activeLine.GetComponent<EdgeCollider2D>().edgeRadius = brushSize / 2;
            activeLine.GetComponent<LineRenderer>().material = brushMat;
            activeLine.GetComponent<LineRenderer>().material.renderQueue += layerCount;
            activeLine.gameObject.layer = LayerMask.NameToLayer("Ground");
        }

        if (Input.GetMouseButtonUp(0))
        {
            activeLine = null;
        }

        if (activeLine != null)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            activeLine.UpdateLine(mousePos);
        }
    }

    void Erase()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.transform.tag == "Paint")
                {
                    Destroy(hit.transform.gameObject);
                }
            }
        }
    }

    void PaintCircle()
    {
        if (Input.GetMouseButtonDown(0))
        {
            layerCount++;
            GameObject circle = Instantiate(brushPrefabs[1], Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f)), Quaternion.identity, transform);
            circle.transform.localScale = new Vector3(brushSize, brushSize, brushSize) * 2;
            circle.layer = LayerMask.NameToLayer("Ground");
            SpriteRenderer sprite = circle.GetComponent<SpriteRenderer>();
            sprite.material = brushMat;
            sprite.material.renderQueue += layerCount;
        }
    }

    void PaintSquare()
    {
        if (Input.GetMouseButtonDown(0))
        {
            layerCount++;
            GameObject square = Instantiate(brushPrefabs[2], Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f)), Quaternion.identity, transform);
            square.transform.localScale = new Vector3(brushSize, brushSize, brushSize) * 2;
            square.layer = LayerMask.NameToLayer("Ground");
            SpriteRenderer sprite = square.GetComponent<SpriteRenderer>();
            sprite.material = brushMat;
            sprite.material.renderQueue += layerCount;
        }
    }

    public void UnlockTool(PaintTools tool)
    {
        switch (tool)
        {
            case PaintTools.SQUARE:
                lockedTools[0].button.enabled = true;
                lockedTools[0].lockObj.SetActive(false);
                break;

        }
    }
}
