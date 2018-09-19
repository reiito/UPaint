using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlatformerManager : MonoBehaviour
{
    public static PlatformerManager platformerManagerInstance;

    public bool randomLevel;
    public int randMin, randMax;

    public PolygonCollider2D camBounds;

    public GameObject collectablePrefab;

    public Text collectedText;

    public CollectionPickUp[] collectionPickUps;
    public Transform collectablesParent;
    public UnlockPickUp[] unlockPickUps;

    public ExitPickUp exit;

    public int collected;

    int randCollectableCount;

    private void Awake()
    {
        platformerManagerInstance = this;

        collected = 0;
    }

    private void Start()
    {
        if (randomLevel)
        {
            exit.transform.position = RandomSpawnPosition();

            randCollectableCount = Random.Range(randMin, randMax);
            collectionPickUps = new CollectionPickUp[randCollectableCount];
            for (int i = 0; i < randCollectableCount; i++)
            {
                collectionPickUps[i] = Instantiate(collectablePrefab, RandomSpawnPosition(), Quaternion.identity, collectablesParent).GetComponent<CollectionPickUp>();
            }

            foreach (UnlockPickUp up in unlockPickUps)
            {
                up.transform.position = RandomSpawnPosition();
            }
        }

        collectedText.text = collected + "/" + collectionPickUps.Length;
    }

    public void AddScore()
    {
        collected++;
        collectedText.text = collected + "/" + collectionPickUps.Length;
    }

    public void TogglePlatformerElements(bool activate)
    {
        collectedText.gameObject.SetActive(activate);

        exit.gameObject.SetActive(activate);

        foreach (CollectionPickUp cp in collectionPickUps)
        {
            if (!cp.collected)
            {
                cp.gameObject.SetActive(activate);
            }
        }

        foreach (UnlockPickUp up in unlockPickUps)
        {
            if (!up.unlocked)
            {
                up.gameObject.SetActive(activate);
            }
        }
    }
    public Vector2 RandomSpawnPosition()
    {
        float randomX = Random.Range(camBounds.points[2].x, camBounds.points[0].x);
        float randomY = Random.Range(camBounds.points[2].y, camBounds.points[0].y);
        return new Vector2(randomX, randomY);
    }

}
