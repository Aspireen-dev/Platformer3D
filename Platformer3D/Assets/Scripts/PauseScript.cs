using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{
    bool isPaused = false;

    public GameObject pausePanel;
    public Text friendsText;
    public Text coinsText;
    public Text mobsText;
    public GameObject miniMap;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                isPaused = false;
                pausePanel.SetActive(isPaused);
                Time.timeScale = 1f;
            }
            else
            {
                isPaused = true;
                pausePanel.SetActive(isPaused);
                SetGoalsText();
                Time.timeScale = 0f;
            }
        }

        if (Input.GetKeyUp(KeyCode.M))
        {
            miniMap.active = !miniMap.active;
        }
    }

    private void SetGoalsText()
    {
        PlayerInfos pi = PlayerInfos.pi;
        friendsText.text = "- Amis délivrés : " + pi.nbFreeFriends + " / " + pi.nbFriendsToFree;
        coinsText.text = "- Pièces récupérées : " + pi.nbCoins + " / " + pi.nbCoinsToCollect;
        mobsText.text = "- Ennemis battus : " + pi.nbMobsKilled + " / " + pi.nbMobsToKill;
    }
}
