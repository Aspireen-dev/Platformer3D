using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    public GameObject pickupEffect;
    public GameObject mobEffect;
    public GameObject waterEffect;
    public GameObject loot;
    bool hasJumpedOnMob = true;
    bool isInvincible = false;
    public GameObject cam1;
    public GameObject cam2;
    public AudioClip hitSound;
    AudioSource audioSource;
    SkinnedMeshRenderer rend;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rend = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "coin") // On a touché une pièce
        {
            GameObject go = Instantiate(pickupEffect, other.transform.position, Quaternion.identity);
            Destroy(go, 0.5f);
            PlayerInfos.pi.GetCoin();
            Destroy(other.gameObject);
        }

        if (other.gameObject.name == "Fin")
        {
            PlayerInfos.pi.GetScore();
        }

        if (other.gameObject.tag == "water")
        {
            GameObject go = Instantiate(waterEffect, transform.position, waterEffect.transform.rotation);
            Destroy(go, 0.5f);
            StartCoroutine(PlayerInfos.pi.Respawn());
        }

        if (other.gameObject.tag == "fall")
        {
            // Respawn ...
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        // Gestion des caméras
        if (other.gameObject.tag == "cam1")
        {
            cam1.SetActive(true);
        }

        if (other.gameObject.tag == "cam2")
        {
            cam2.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "cam1")
        {
            cam1.SetActive(false);
        }
        if (other.gameObject.tag == "cam2")
        {
            cam2.SetActive(false);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Si le monstre me touche
        if (hit.gameObject.tag == "hurt" && !isInvincible)
        {
            // Je suis blessé
            isInvincible = true;
            PlayerInfos.pi.SetHealth(-1);
            iTween.MoveAdd(gameObject, Vector3.back * 2, 0.3f);
            StartCoroutine(ResetInvincible());
        }
        // Si je saute sur le monstre;
        if (hit.gameObject.tag == "mob" && hasJumpedOnMob)
        {
            hit.gameObject.transform.parent.gameObject.GetComponent<Collider>().enabled = false;
            hasJumpedOnMob = false;
            audioSource.PlayOneShot(hitSound);
            iTween.PunchScale(hit.gameObject.transform.parent.gameObject, new Vector3(20, 20, 20), 0.5f);
            
            // Je détruis le monstre
            GameObject go = Instantiate(mobEffect, hit.transform.position, Quaternion.identity);
            Instantiate(loot, hit.gameObject.transform.position + Vector3.forward, Quaternion.identity * Quaternion.Euler(-90, 0, 0));
            Destroy(go, 0.6f);
            Destroy(hit.gameObject.transform.parent.parent.gameObject, 0.5f);

            PlayerInfos.pi.KillMob();
            StartCoroutine(ResetInstantiate());
        }
    }

    // On réinitialise hasJumpedOnMob après 1 sec
    IEnumerator ResetInstantiate()
    {
        yield return new WaitForSeconds(1f);
        hasJumpedOnMob = true;
    }

    IEnumerator ResetInvincible()
    {
        for (int i=0; i<10; i++)
        {
            yield return new WaitForSeconds(0.2f);
            rend.enabled = !rend.enabled;
        }
        yield return new WaitForSeconds(0.2f);
        isInvincible = false;
    }
}
