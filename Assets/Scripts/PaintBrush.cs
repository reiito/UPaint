using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PaintBrush : MonoBehaviour
{
    LineRenderer line;
    EdgeCollider2D edgeCollider;

    List<Vector2> points;

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
        edgeCollider = GetComponent<EdgeCollider2D>();
    }

    public void UpdateLine(Vector2 mousePos)
    {
        if (points == null)
        {
            points = new List<Vector2>();
            SetPoint(mousePos);
            return;
        }

        if (Vector2.Distance(points.Last(), mousePos) > 0.1f)
        {
            SetPoint(mousePos);
        }
    }

    void SetPoint(Vector2 point)
    {
        points.Add(point);
        line.positionCount = points.Count;
        line.SetPosition(points.Count - 1, point);

        if (points.Count > 1)
        {
            edgeCollider.points = points.ToArray();
        }
    }
}
