using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorButton : MonoBehaviour
{
  PaintManager paintManager;

  private void Start()
  {
    paintManager = FindObjectOfType<PaintManager>();
  }

  public void OnColorClick()
  {
    paintManager.SetCurrentColor(transform);
  }
}
