using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
  float switchTime = 180f;

  bool platformerMode;

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
