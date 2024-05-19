using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Gravity_Wheel : MonoBehaviour
{
    public Player_Movements player;


    public float gravity;
    public float maxGravity;
    public float stock;
    public float gravPerSec;

    public bool gravityExhausted;
    public bool canTriggerGravity;
    public bool gravityConsuming;

    [SerializeField] private Image blueWheel;
    [SerializeField] private Image redWheel;

    private void Start()
    {
        player = GameObject.FindObjectOfType(typeof(Player_Movements)) as Player_Movements;

        stock = maxGravity - 0.07f;
    }
    private void Update()
    {
        if (!gravityConsuming && canTriggerGravity && Input.GetKeyDown(KeyCode.Space)) 
        {

            // Activ� la gravit�
            canTriggerGravity = false;
            gravityConsuming = true;
        }
        if (gravityConsuming && !player.isGrounded())
        {
            if (gravity > 0f)
            {
                // Consumer la gravit�
                gravity = Mathf.Max(0f, gravity - gravPerSec * Time.deltaTime);
            }
            else
            {
                // Arr�ter de consumer la gravit�
                gravityConsuming = false;
            }
        }
        else
        {
            // Recharger la gravit�
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
                // Permet de faire dispara�tre la roue rouge lors de la recharge et de stocker sa quantit� pour l'utiliser si la gravit� est de nouveau consomm�e. 
                stock = redWheel.fillAmount;
                redWheel.fillAmount = 0f;

                if (gravity > 0f)
                {
                    blueWheel.fillAmount = (gravity / maxGravity);
                }
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