using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpFriends : MonoBehaviour
{
    GameObject cage;
    public Text infoText;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && cage != null)
        {
            GameObject poussin = cage.transform.GetChild(0).gameObject;
            poussin.transform.parent = null;

            iTween.ShakeScale(cage, new Vector3(20, 20, 20), 1.5f);
            Destroy(cage, 1.6f);
            
            CanOpenCage(false);
            poussin.GetComponentInChildren<Canvas>().enabled = true;

            PlayerInfos.pi.FreeFriend();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "cage")
        {
            cage = other.gameObject;
            CanOpenCage(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "cage")
        {
            CanOpenCage(false);
        }
    }

    private void CanOpenCage(bool canOpenCage)
    {
        if (canOpenCage)
        {
            infoText.text = "Appuyez sur E pour ouvrir la cage";
        }
        else
        {
            cage = null;
            infoText.text = "";
        }
    }
}
