using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public bool platformerMode;

    float switchTime = 180f;

    private void Start()
    {
        StartCoroutine(BaitAndSwitch());
    }

    IEnumerator BaitAndSwitch()
    {
        yield return new WaitForSeconds(switchTime);

        Debug.Log("2d mode");
        platformerMode = true;
    }
}
