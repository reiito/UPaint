using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public enum Tools
{
    BRUSH, ERASER, CIRCLE, SQUARE
}

public class PaintManager : MonoBehaviour
{
    public GameController gameController;

    public Transform defaultColor;
    public Transform defaultTool;

    public GameObject paintBrush;
    public GameObject circleBrush;

    public Material[] buttonMaterials;

    PaintBrush activeLine;

    Transform activeColorButton;
    Material brushMat;

    Transform activeToolButton;
    Tools selectedTool;

    int layerCount = 0;

    private void Start()
    {
        activeColorButton = defaultColor;
        SetCurrentColor(defaultColor);
        SetCurrentTool(defaultTool);
    }

    private void Update()
    {
        if (!gameController.platformerMode)
        {
            switch (selectedTool)
            {
                case Tools.BRUSH:
                    PaintBrush();
                    break;
                case Tools.ERASER:
                    break;
                case Tools.CIRCLE:
                    PaintCircle();
                    break;
                case Tools.SQUARE:
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
                selectedTool = Tools.BRUSH;
                break;
            case "Eraser":
                selectedTool = Tools.ERASER;
                break;
            case "Circle":
                selectedTool = Tools.CIRCLE;
                break;
            case "Square":
                selectedTool = Tools.SQUARE;
                break;

            default:
                Debug.Log(activeToolButton.name + " not implemented yet");
                break;
        }
    }

    public void SetCurrentTool(Tools newTool)
    {
        selectedTool = newTool;
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
            activeLine = Instantiate(paintBrush, transform).GetComponent<PaintBrush>();
            activeLine.GetComponent<LineRenderer>().material = brushMat;
            activeLine.GetComponent<LineRenderer>().material.renderQueue += layerCount;
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

    void PaintCircle()
    {
        if (Input.GetMouseButtonDown(0))
        {
            layerCount++;
            SpriteRenderer sprite = Instantiate(circleBrush, Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f)), Quaternion.identity, transform).GetComponent<SpriteRenderer>();
            sprite.material = brushMat;
            sprite.material.renderQueue += layerCount;
        }
    }
}
