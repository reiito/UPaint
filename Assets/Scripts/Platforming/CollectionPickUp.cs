using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionPickUp : PickUps
{
    PlatformerManager platformerManager;

    public Sprite[] variations;

    public bool collected;

    private void Start()
    {
        platformerManager = PlatformerManager.platformerManagerInstance;
        GetComponent<SpriteRenderer>().sprite = variations[Random.Range(0, variations.Length)];
    }

    public override void PickedUp()
    {
        collected = true;
        platformerManager.AddScore();
    }

}
