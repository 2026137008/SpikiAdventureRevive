using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealthUI : MonoBehaviour
{
    public PlayerHealth playerHealth;

    public Slider hpBar;

    public TMP_Text hpText;

    void Update()
    {
        hpBar.maxValue = playerHealth.maxHP;
        hpBar.value = playerHealth.currentHP;

        hpText.text =
            playerHealth.currentHP +
            " / " +
            playerHealth.maxHP;
    }
}