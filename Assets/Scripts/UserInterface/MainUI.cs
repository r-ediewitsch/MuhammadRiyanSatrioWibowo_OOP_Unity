using UnityEngine;
using UnityEngine.UIElements;

public class MainUI : MonoBehaviour {
    public UIDocument uiDocument;
    [SerializeField] Player player;
    [SerializeField] CombatManager combatManager;
    private Label healthText, pointsText, waveText, enemyCountText;
    public int point = 0;

    void Start() 
    {
        player = FindObjectOfType<Player>();

        var root = uiDocument.rootVisualElement;
        healthText = root.Q<Label>("Health");
        pointsText = root.Q<Label>("Point");
        waveText = root.Q<Label>("Wave");
        enemyCountText = root.Q<Label>("EnemiesLeft");
    }

    void Update()
    {
        UpdateHealth(player.GetComponent<HealthComponent>().health);
        UpdateWave(combatManager.waveNumber);
        UpdateEnemyCount(combatManager.totalEnemies);
        UpdatePoints(point);
    }

    public void UpdateHealth(float health) 
    {
        healthText.text = $"Health: {health}";
    }

    public void UpdatePoints(int points) 
    {
        pointsText.text = $"Points: {points}";
    }

    public void UpdateWave(int wave) 
    {
        waveText.text = $"Wave: {wave}";
    }

    public void UpdateEnemyCount(int count) 
    {
        enemyCountText.text = $"Enemies: {count}";
    }
}
