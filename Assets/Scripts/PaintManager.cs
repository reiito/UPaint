using UnityEngine;
using UnityEngine.UI;

public class PaintManager : MonoBehaviour
{
  public Transform defaultColor;

  public GameObject circleBrush;

  Transform activeColorButton;
  Material brushMat;

  private void Start()
  {
    activeColorButton = defaultColor;
    SetCurrentColor(defaultColor);
  }

  private void Update()
  {
    if (Input.GetMouseButton(0))
    {
      SpriteRenderer sprite = Instantiate(circleBrush, Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f)), Quaternion.identity, transform).GetComponent<SpriteRenderer>();
      sprite.material = brushMat;
    } 
  }

  public void SetCurrentColor(Transform colorButton)
  {
    activeColorButton.GetChild(0).gameObject.SetActive(false);
    brushMat = colorButton.gameObject.GetComponent<Image>().material;
    activeColorButton = colorButton.transform;
    colorButton.GetChild(0).gameObject.SetActive(true);
  }
}
