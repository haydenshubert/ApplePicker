using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplePicker : MonoBehaviour
{
    public GameRound roundCounter;

    [Header("Inscribed")]
    public GameObject basketPrefab;
    public int numBaskets = 4;
    public float basketBottomY = -14f;
    public float basketSpacingY = 2f;
    public List<GameObject> basketList;

    void Start()
    {
        // Added for game round
        GameObject roundGO = GameObject.Find("GameRound");
        roundCounter = roundGO.GetComponent<GameRound>();

        basketList = new List<GameObject>();
        for (int i = 0; i < numBaskets; i++)
        {
            GameObject tBasketGO = Instantiate<GameObject>(basketPrefab);
            Vector3 pos = Vector3.zero;
            pos.y = basketBottomY + (basketSpacingY * i);
            tBasketGO.transform.position = pos;
            basketList.Add(tBasketGO);
        }
    }

    public void AppleMissed()
    {
        // Destory all of the falling Apples
        GameObject[] appleArray = GameObject.FindGameObjectsWithTag("Apple");
        foreach (GameObject tempGO in appleArray)
        {
            Destroy(tempGO);
        }

        // Destroy one of the Baskets
        // Get the index of the last Basket in basketList
        int basketIndex = basketList.Count - 1;
        // Get a reference to the Basket GameObject
        GameObject basketGO = basketList[basketIndex];
        // Remove the Basket from the lsit and destroy the GameObject
        basketList.RemoveAt(basketIndex);
        Destroy(basketGO);
        roundCounter.round += 1;

        // If there are no Baskets left, restart the game
        if (basketList.Count == 0)
        {
            SceneManager.LoadScene(2);
        }
    }

    public void LogHit()
    {
        SceneManager.LoadScene(2);
    }
}
