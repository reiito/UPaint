using System.Collections.Generic;
using UnityEngine;

public class PaintBrush : MonoBehaviour
{
    public GameObject paintBrushPrefab;

    LineLogic activeLine;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            activeLine = Instantiate(paintBrushPrefab, transform).GetComponent<LineLogic>();
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
}