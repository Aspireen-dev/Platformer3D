using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public Vector3 lastPoint;
    public CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        lastPoint = transform.position;
        characterController = GetComponent<CharacterController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "checkpoint")
        {
            lastPoint = other.gameObject.transform.position;
            other.gameObject.GetComponent<CoinAnim>().enabled = true;
        }
    }

    public void Respawn()
    {
        characterController.enabled = false;
        transform.position = lastPoint;
        characterController.enabled = true;
        PlayerInfos.pi.SetHealth(3);
    }
}
