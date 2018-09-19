using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUps : MonoBehaviour
{
    protected PaintManager paintManager;

    private void Start()
    {
        paintManager = PaintManager.paintManagerInstance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PickedUp();
            transform.gameObject.SetActive(false);
        }
    }

    public virtual void PickedUp() { }
}
