using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController cc;
    // Variables pour le d�placement
    public float moveSpeed;
    public float jumpForce;
    public float gravity;
    // Vecteur direction souhait�e
    private Vector3 moveDir;
    private Animator anim;
    bool isWalking = false;

    private void Start()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Calcul de la direction
        moveDir = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, moveDir.y, Input.GetAxis("Vertical") * moveSpeed);

        // Check de la touche espace
        if (Input.GetButtonDown("Jump") && cc.isGrounded)
        {
            // On saute
            moveDir.y = jumpForce;
        }
        
        // On applique la gravit�
        moveDir.y -= gravity * Time.deltaTime;

        // Si on se d�place (si mouvement != 0)
        if (moveDir.x != 0 || moveDir.z != 0)
        {
            isWalking = true; // Le personnage marche
            // On tourne le personnage dans la bonne direction
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(moveDir.x, 0, moveDir.z)), 0.1f);
        }
        else
        {
            isWalking = false; // On s'arr�te de marcher
        }

        anim.SetBool("isWalking", isWalking); // On indique � l'animator si on marche

        // On applique le d�placement
        cc.Move(moveDir * Time.deltaTime);
    }
}
