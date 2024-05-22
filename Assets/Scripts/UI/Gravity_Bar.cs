using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gravity_Bar : MonoBehaviour
{
    public Player_Controller player;

    public float gravity;
    public float maxGravity;
    public float gravPerSec;
    public float gravityRefill;
    public bool gravityExhausted;
    public bool canTriggerGravity;
    public bool gravityConsuming;
    [SerializeField] private Image blueWheel;
    [SerializeField] private Image redWheel;

    private void Start()
    {
        player = GameObject.FindObjectOfType(typeof(Player_Controller)) as Player_Controller;
    }

    private void Update()
    {
        if (!gravityConsuming && canTriggerGravity && Input.GetKeyDown(KeyCode.Space))
        {
            ActivateGravity();
        }
        if (gravityConsuming && !player.isGrounded())
        {
            if (gravity > 0f)
            {
                ConsumeGravity();
            }
            else
            {
                StopConsumeGravity();
            }
        }
        else if (player.isGrounded())
        {
            RefillGravity();
        }

        UpdateDisplay();


    }

    private void ActivateGravity()
    {
        canTriggerGravity = false;
        gravityConsuming = true;
    }

    private void ConsumeGravity()
    {
        gravity = Mathf.Max(0f, gravity - gravPerSec * Time.deltaTime);
    }

    private void StopConsumeGravity()
    {
        gravityConsuming = false;
    }

    private void RefillGravity()
    {
        gravity = Mathf.Min(maxGravity, gravity + gravityRefill * Time.deltaTime);
        if (gravity >= maxGravity)
        {
            canTriggerGravity = true;
        }
    }

    private void UpdateDisplay()
    {
        if (gravityConsuming)
        {
            blueWheel.fillAmount = (gravity / maxGravity);
            redWheel.fillAmount = (gravity / maxGravity + 0.03f);

        }
        else
        {
            canTriggerGravity = false;
            redWheel.fillAmount = (gravity / maxGravity);

            if (gravity >= maxGravity)
            {
                blueWheel.fillAmount = 1f;
                canTriggerGravity = true;
            }
        }
    }
}