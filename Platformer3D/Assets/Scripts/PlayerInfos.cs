using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfos : MonoBehaviour
{
    public static PlayerInfos pi;

    public int playerHealth = 3;
    public int nbCoins = 0;
    public int nbCoinsToCollect;
    public int nbFreeFriends = 0;
    public int nbFriendsToFree;
    public int nbMobsKilled = 0;
    public int nbMobsToKill;

    public Image[] hearts;
    public Text coinText;
    public Text infoText;
    public CheckpointManager checkpointManager;

    private void Awake()
    {
        pi = this;
    }

    private void Start()
    {
        nbCoinsToCollect = GameObject.FindGameObjectsWithTag("coin").Length;
        nbFriendsToFree = GameObject.FindGameObjectsWithTag("friend").Length;
        nbMobsToKill = GameObject.FindGameObjectsWithTag("mob").Length;
        nbCoinsToCollect += nbMobsToKill;
    }

    public void SetHealth(int val)
    {
        playerHealth += val;
        if (playerHealth > 3)
            playerHealth = 3;
        if (playerHealth <= 0)
        {
            playerHealth = 0;
            StartCoroutine(Respawn());
        }

        SetHealthBar();
    }

    public void GetCoin()
    {
        nbCoins++;
        coinText.text = nbCoins.ToString();
    }

    public void FreeFriend()
    {
        nbFreeFriends++;
    }

    public void KillMob()
    {
        nbMobsKilled++;
    }

    public void SetHealthBar()
    {
        // On vide la barre de vie
        foreach (Image img in hearts)
        {
            img.enabled = false;
        }

        // On met le bon nombre de coeurs à l'écran
        for (int i=0; i<playerHealth; i++)
        {
            hearts[i].enabled = true;
        }
    }

    public void GetScore()
    {
        int scoreFinal = (nbCoins * 5) + (playerHealth * 10);
        infoText.text = "Score : " + scoreFinal;
    }

    public IEnumerator Respawn()
    {
        yield return new WaitForSeconds(0.5f);
        checkpointManager.Respawn();
    }
}
