using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitPickUp : PickUps
{
    public override void PickedUp()
    {
        SceneManager.LoadScene("Main");
    }
}
