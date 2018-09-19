using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockPickUp : PickUps
{
    public PaintTools type;

    public bool unlocked;

    public override void PickedUp()
    {
        paintManager.UnlockTool(type);
        unlocked = true;
    }
}
