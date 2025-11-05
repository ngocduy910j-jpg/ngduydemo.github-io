using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class TaiXiuGame : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_Text resultText;
    public TMP_Text coinText;
    public TMP_InputField betInput;
    public TMP_Text historyText;

    [Header("Dice Images")]
    public Image dice1, dice2, dice3;
    public Sprite[] diceFaces;

    [Header("Audio")]
    public AudioSource diceSound;

    private string playerChoice = "";
    private int coin = 0;
    private System.Random rand = new System.Random();

    // Thống kê
    private int totalPlays = 0;
    private int totalWins = 0;
    private int totalLoses = 0;
    private string history = "";

    void Start()
    {
        coin = PlayerPrefs.GetInt("coin", 100000);
        history = PlayerPrefs.GetString("history", "");
        coinText.text = $"💰 Tiền: {coin:N0}";
        historyText.text = history;
    }

    public void ChonTai() { playerChoice = "Tài"; resultText.text = "Bạn chọn: TÀI"; }
    public void ChonXiu() { playerChoice = "Xỉu"; resultText.text = "Bạn chọn: XỈU"; }

    public void DatCuoc()
    {
        if (playerChoice == "")
        {
            resultText.text = "⚠️ Hãy chọn Tài hoặc Xỉu trước!";
            return;
        }

        if (!int.TryParse(betInput.text, out int betAmount) || betAmount <= 0)
        {
            resultText.text = "⚠️ Số tiền cược không hợp lệ!";
            return;
        }

        if (betAmount > coin)
        {
            resultText.text = "⚠️ Bạn không đủ tiền!";
            return;
        }

        StartCoroutine(RollDiceAnimation(betAmount));
    }

    IEnumerator RollDiceAnimation(int betAmount)
    {
        if (diceSound != null) diceSound.Play();

        // Quay xúc xắc 1s
        for (int i = 0; i < 15; i++)
        {
            dice1.sprite = diceFaces[Random.Range(0, 6)];
            dice2.sprite = diceFaces[Random.Range(0, 6)];
            dice3.sprite = diceFaces[Random.Range(0, 6)];
            yield return new WaitForSeconds(0.05f);
        }

        // Kết quả thật
        int d1 = rand.Next(1, 7);
        int d2 = rand.Next(1, 7);
        int d3 = rand.Next(1, 7);
        int tong = d1 + d2 + d3;

        dice1.sprite = diceFaces[d1 - 1];
        dice2.sprite = diceFaces[d2 - 1];
        dice3.sprite = diceFaces[d3 - 1];

        string ketQua = (tong >= 11) ? "Tài" : "Xỉu";

        // Thêm yếu tố nhà cái (AI can thiệp)
        bool nhaCaiThang = Random.value < 0.1f; // 10% nhà cái lật kết quả
        if (nhaCaiThang)
        {
            ketQua = (ketQua == "Tài") ? "Xỉu" : "Tài";
            resultText.text = "🤖 Nhà cái can thiệp!";
            yield return new WaitForSeconds(0.8f);
        }

        // Xử lý thắng thua
        totalPlays++;
        bool playerWin = (playerChoice == ketQua);
        if (playerWin)
        {
            coin += betAmount;
            totalWins++;
            resultText.text = $"🎉 {tong} điểm ({ketQua}) — Bạn THẮNG +{betAmount:N0}!";
        }
        else
        {
            coin -= betAmount;
            totalLoses++;
            resultText.text = $"😢 {tong} điểm ({ketQua}) — Bạn THUA -{betAmount:N0}!";
        }

        // Lưu lịch sử
        string ketquaVan = $"{totalPlays}. Tổng {tong} ({ketQua}) - {(playerWin ? "THẮNG" : "THUA")}";
        history = ketquaVan + "\\n" + history;
        historyText.text = history;

        // Cập nhật UI + lưu dữ liệu
        coinText.text = $"💰 Tiền: {coin:N0}";
        PlayerPrefs.SetInt("coin", coin);
        PlayerPrefs.SetString("history", history);
        PlayerPrefs.Save();

        playerChoice = "";
    }

    public void ResetGame()
    {
        coin = 100000;
        history = "";
        totalPlays = totalWins = totalLoses = 0;
        PlayerPrefs.DeleteAll();
        resultText.text = "Trò chơi đã được reset!";
        coinText.text = $"💰 Tiền: {coin:N0}";
        historyText.text = "";
    }
}
