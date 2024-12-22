using System.Collections.Generic;
using System.IO;
using TMPro;

public class ScoreBoard
{
    //Khởi tạo file scoreboard khi play(trong SceneManager)
    private static ScoreBoard instance;

    private ScoreBoard() { }

    public static ScoreBoard Instance
    {
        get
        {
            if (instance == null)
                instance = new ScoreBoard();
            return instance;
        }
    }
    public void ResetScoreboard()
    {
        instance = new ScoreBoard();
    }
    //instance methods
    public int enemyKilled = 0;
    public TextMeshProUGUI timeScoreboard;
    public int lvPlayer = 0;
    public List<Inventory.Slot> weaponSlots = new();
    public List<Inventory.Slot> passiveSlots = new();
    public float totalDamage = 0;
    
}