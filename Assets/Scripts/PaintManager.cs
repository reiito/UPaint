using UnityEngine;
using UnityEngine.UI;

public enum Tools
{
    BRUSH, ERASER, CIRCLE, SQUARE
}

public class PaintManager : MonoBehaviour
{
    public Transform defaultColor;

    public GameObject paintBrush;
    public GameObject circleBrush;

    PaintBrush activeLine;

    Transform activeColorButton;
    Material brushMat;

    Tools selectedTool;

    int layerCount = 0;

    private void Start()
    {
        activeColorButton = defaultColor;
        SetCurrentColor(defaultColor);
        selectedTool = Tools.BRUSH;
    }

    private void Update()
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
