using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

// Script permettant de charger/decharger la bar de Gravite

public class Gravity_Bar : MonoBehaviour
{
    // ----------------------------------------------------------------------------------- Propriétés et Variables ----------------------------------------------------------------------------------- //


    public Player_Controller player;

    private float stock;

    public float gravity;
    public float maxGravity;
    public float gravPerSec;

    private bool gravityExhausted;
    private bool canTriggerGravity;
    private bool gravityConsuming;

    [SerializeField] private Image blueWheel;
    [SerializeField] private Image redWheel;

    private void Start()
    {
        player = GameObject.FindObjectOfType(typeof(Player_Controller)) as Player_Controller;

        stock = maxGravity - 0.07f;
    }
    private void Update()
    {
        GravityBar();
    }

    // ----------------------------------------------------------------------------------- Bar de gravité ----------------------------------------------------------------------------------- //

    private void GravityBar()
    {
        // Activé la gravité
        if (!gravityConsuming && canTriggerGravity && Input.GetKeyDown(KeyCode.Space))
        {
            canTriggerGravity = false;
            gravityConsuming = true;
        }
        if (gravityConsuming && !player.isGrounded())
        {
            // Consumer la gravité
            if (gravity > 0f)
            {
                gravity = Mathf.Max(0f, gravity - gravPerSec * Time.deltaTime);
            }
            // Arrêter de consumer la gravité
            else
            {
                gravityConsuming = false;
            }
        }
        // Recharger la gravité
        else
        {
            if (player.isGrounded())
            {
                gravity = Mathf.Min(maxGravity, gravity + gravPerSec * Time.deltaTime);
                if (gravity >= maxGravity)
                {
                    canTriggerGravity = true;
                }
            }
        }

        UpdateDisplay();
    }
    private void UpdateDisplay()
    {
        if (gravityConsuming && !player.isGrounded())
        {
            redWheel.fillAmount = stock;
            blueWheel.fillAmount = (gravity / maxGravity);
            
            if (blueWheel.fillAmount == 0f)
            {
                redWheel.fillAmount = (gravity / maxGravity);
            }
            else
            {
                redWheel.fillAmount = (gravity / maxGravity + 0.05f);
            }
        }
        else
        {
            if (player.isGrounded())
            {
                // Permet de faire disparaître la roue rouge lors de la recharge et de stocker sa quantité pour l'utiliser si la gravité est de nouveau consommée
                stock = redWheel.fillAmount;
                redWheel.fillAmount = 0f;

                if (gravity > 0f)
                {
                    blueWheel.fillAmount = (gravity / maxGravity);
                }
                // PROBLEME : n'est jamais joue + manque le fait de bloquer la mecanique
                else
                {
                    redWheel.fillAmount = (gravity / maxGravity);
                    if (gravity >= maxGravity)
                    {
                        blueWheel.fillAmount = 1f;
                    }
                }
              
            }
        }
    }
}