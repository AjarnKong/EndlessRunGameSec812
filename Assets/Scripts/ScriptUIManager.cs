using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScriptUIManager : MonoBehaviour
{
    public static ScriptUIManager Instance { get; private set; }

    public TMP_Text coinText, hpText;
    public Image[] heartIcons;
    public GameObject gameOverPanel;

    public GameObject invincibleIcon, magenetIcon;
    public TMP_Text invincibleTimerText, magnetTimerText;
    public ScriptPlayerController player;
    public TMP_Text gameOverCoinText, gameOverScoreText;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(this.gameObject); return; }
    
        Instance = this;
    }

    void Update()
    {
        if (player == null) return;
        if (invincibleIcon != null) invincibleIcon.SetActive(player.IsInvincible);
        if (magenetIcon != null) magenetIcon.SetActive(player.IsMagnetActive);

        if (invincibleTimerText != null && player.IsInvincible)
            invincibleTimerText.text = "Invincible: " + Mathf.CeilToInt(player.invincibleTimer) + "s";
        else if (invincibleTimerText != null && !player.IsInvincible)
            invincibleTimerText.text = "";

        if (magnetTimerText != null && player.IsMagnetActive)
            magnetTimerText.text = "Magnet: " + Mathf.CeilToInt(player.magnetTimer) + "s";
        else if (magnetTimerText != null && !player.IsMagnetActive)
            magnetTimerText.text = "";
    }

    public void ShowGameOver(int coins)
    {
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
        if (gameOverCoinText != null) gameOverCoinText.text = "Coins: " + coins;
        if (gameOverScoreText != null) gameOverScoreText.text = "Score: " + coins * 100;
    }

    public void UpdateHP(int hp)
    {
        if (hpText != null) hpText.text = "HP: " + hp;
        if (heartIcons != null)
            for (int i = 0; i < heartIcons.Length; i++)
                if (heartIcons[i] != null)
                    heartIcons[i].enabled = i < hp;
    }

    public void UpdateCoins(int total, int progress)
    {
        if (coinText != null) 
            coinText.text = "Coins: " + total + " (+" + progress + "/100)";
    }
}
