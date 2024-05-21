using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gravity_Bar : MonoBehaviour
{

    public float gravity;
    public float maxGravity;
    public float gravPerSec;
    public bool gravityExhausted;
    public bool canTriggerGravity;
    public bool gravityConsuming;
    [SerializeField] private Image blueWheel;
    [SerializeField] private Image redWheel;

    private void Update()
    {
        if (!gravityConsuming && canTriggerGravity && Input.GetKeyDown(KeyCode.Space))
        {
            ActivateGravity();
        }
        if (gravityConsuming)
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
        else
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
        gravity = Mathf.Min(maxGravity, gravity + gravPerSec * Time.deltaTime);
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
            //95 -> 100
            // 75 -> 80
            // 0-> 0
            float prorata = (gravity / maxGravity) * 5f;
            int arrondi = Mathf.CeilToInt(prorata);
            redWheel.fillAmount = arrondi / 5f;
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