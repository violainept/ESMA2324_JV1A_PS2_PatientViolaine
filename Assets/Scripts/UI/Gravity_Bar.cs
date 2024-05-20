using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

// Script permettant de charger/decharger la bar de Gravite

public class Gravity_Bar : MonoBehaviour
{
    // ----------------------------------------------------------------------------------- Propri�t�s et Variables ----------------------------------------------------------------------------------- //


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

    // ----------------------------------------------------------------------------------- Bar de gravit� ----------------------------------------------------------------------------------- //

    private void GravityBar()
    {
        // Activ� la gravit�
        if (!gravityConsuming && canTriggerGravity && Input.GetKeyDown(KeyCode.Space))
        {
            canTriggerGravity = false;
            gravityConsuming = true;
        }
        if (gravityConsuming && !player.isGrounded())
        {
            // Consumer la gravit�
            if (gravity > 0f)
            {
                gravity = Mathf.Max(0f, gravity - gravPerSec * Time.deltaTime);
            }
            // Arr�ter de consumer la gravit�
            else
            {
                gravityConsuming = false;
            }
        }
        // Recharger la gravit�
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
                // Permet de faire dispara�tre la roue rouge lors de la recharge et de stocker sa quantit� pour l'utiliser si la gravit� est de nouveau consomm�e
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