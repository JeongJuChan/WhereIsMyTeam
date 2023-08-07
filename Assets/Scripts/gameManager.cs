using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public static gameManager I;

    public Text timeTxt;
    float time = 0.0f;

    public GameObject card;

    public int line = 4;

    public GameObject firstCard;
    public GameObject secondCard;

    public GameObject endTxt;

    public AudioClip match;
    public AudioSource audioSource;

    private void Awake()
    {
        I = this;
    }

    void Start()
    {
        Time.timeScale = 1.0f;

        int[] rtans = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };
        for (int i = 0; i < Math.Pow(line, 2); i++)
        {
            rtans = rtans.OrderBy(item => UnityEngine.Random.Range(-1f, 1f)).ToArray();

            GameObject newCard = Instantiate(card);
            newCard.transform.parent = GameObject.Find("Cards").transform;
            
            // (1) 0 (2) 0.5Ä­ (3) 1Ä­ ... => (n - 1) * 0.5 Ä­
            float fitCenterValue = (line - 1) * (1.4f * 0.5f);

            float x = (i / line) * 1.4f - fitCenterValue;
            float y = (i % line) * 1.4f - (fitCenterValue + 0.9f); // 0.9f == ³ôÀÌ ¸ÂÃçÁØ °ª
            newCard.transform.position = new Vector3(x, y, 0);

            string rtanName = "rtan" + rtans[i].ToString();
            newCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(rtanName);

        }
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        timeTxt.text = time.ToString("N2");
        if (time >= 3f)
        {
            time = 30.0f;
            endTxt.SetActive(true);
            Time.timeScale = 0.0f;
        }
    }

    public void checkMatched()
    {
        string firstCardImage = firstCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;
        string secondCardImage = secondCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;

        if (firstCardImage == secondCardImage)
        {
            audioSource.PlayOneShot(match);

            firstCard.GetComponent<card>().destroyCard();
            secondCard.GetComponent<card>().destroyCard();

            int cardsLeft = GameObject.Find("Cards").transform.childCount;
            if (cardsLeft == 2)
            {
                endTxt.SetActive(true);
                Time.timeScale = 0.0f;
            }
        }
        else
        {
            firstCard.GetComponent<card>().closeCard();
            secondCard.GetComponent<card>().closeCard();
        }

        firstCard = null;
        secondCard = null;
    }

    public void retryGame()
    {
        SceneManager.LoadScene("MainScene");
    }

}
