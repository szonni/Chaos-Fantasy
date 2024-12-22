using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterHandler : MonoBehaviour
{
    public CharacterData characterData;

    PlayerAnimation animator;
    PlayerMovement movement;

    // Current character stats
    [HideInInspector]
    public float currentHealth;
    [HideInInspector]
    public float currentRecovery;
    [HideInInspector]
    public float currentMight;
    [HideInInspector]
    public float currentProjectileSpeed;
    [HideInInspector]
    public float currentMagnet;
    [HideInInspector]
    public float currentCooldownReduction;

    PlayerCollector collector;
    
    // Experience variables
    public int exp = 0;
    public int level = 1;
    public int expCap;

    // SubClass LevelRange will determine how much will the exp required to level up increase in certain level ranges
    // Ex: Level 1 -> 4: each time you level up the expCap is increased by 2
    //     Level 5 -> 10: each time you level up the expCap is increased by 4
    // Use System.Serializable to expose the class's variables in the inspector
    [System.Serializable]
    public class LevelRange
    {
        public int startLevel;
        public int endLevel;
        public int expCapIncrease;
    }

    // Player's components UI
    [Header("UI")]
    public Image healthBar;
    public Image expBar;
    public TextMeshProUGUI levelText;
    AudioManager audioManager;
    // This determines how many seconds the character has before taking damage again
    [Header("I-Frames")]
    public float invincibilityDuration;
    private float invincibilityTimer;
    private bool isInvincible;

    // A list of LevelRange to set level ranges, exp increase in each level range
    [Header("Level Ranges")]
    public List<LevelRange> levelRanges;

    Inventory inventory;
    public int weaponId;
    public int itemId;

    ItemPool itemPool;

    void Awake()
    {
        // Load character
        characterData = CharacterSelector.LoadData();
        CharacterSelector.instance.DestroySingleton();

        inventory = GetComponent<Inventory>();
        collector = GetComponentInChildren<PlayerCollector>();
        itemPool = FindFirstObjectByType<ItemPool>();

        movement = GetComponent<PlayerMovement>();
        movement.currentMoveSpeed = characterData.MoveSpeed;
        // Assign current stats to the starting stats
        currentHealth = characterData.MaxHealth;
        currentRecovery = characterData.Recovery;
        currentMight = characterData.Might;
        currentProjectileSpeed = characterData.ProjectileSpeed;
        currentMagnet = characterData.Magnet;
        currentCooldownReduction = characterData.CooldownReduction;

        collector.SetRadius(currentMagnet);

        // Set animation for the character
        animator = GetComponent<PlayerAnimation>(); 
        animator.SetAnimator(characterData.animationController);

        // Set the starting weapon
        AcquireWeapon(characterData.StartingWeapon);
        //UI Audio
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        // Initialize expCap as the first expCapIncrease so the character can level up immediately
        expCap = levelRanges[0].expCapIncrease;

        // Init player's UI elements
        UpdateHealthBar();
        UpdateExpBar();
        UpdateLevelText();
    }

    void Update()
    {
        if (invincibilityTimer > 0)
        {
            invincibilityTimer -= Time.deltaTime;
        }
        // If I-Frames reaches 0, then the character is no longer invincible
        else if (isInvincible)
        {
            isInvincible = false;
        }

        // Recover health over time
        Recover();
    }

    // Increase exp on pick up exp gems/defeated a boss
    public void IncreaseExp(int amount)
    {
        exp += amount;
        LevelUpChecker();
        UpdateExpBar();  
    }

    void LevelUpChecker()
    {
        if (exp > expCap)
        {
            level += 1;
            exp -= expCap;

            UpdateLevelText();

            foreach (LevelRange range in levelRanges)
            {
                if (level >= range.startLevel && level <= range.endLevel)
                {
                    expCap += range.expCapIncrease;
                    break;
                }
            }
            
            // Change the state of the game to LevelUp
            GameManager.instance.StartLevelUpScreen();
            // Create item table
            itemPool.ChooseItems();
        }
    }

    public void ApplyWeaponBuffs() 
    {
        foreach (var weapon in inventory.weaponSlots) 
        {
            if (weapon.item == null) continue;
            Weapon weaponToBuff = weapon.item as Weapon;
            weaponToBuff.ApplyBuffs();
        }
    }

    public void TakeDamage(float dmg)
    {
        // If the character still has I-Frames then take no damage
        if (isInvincible)
        {
            return;
        }

        currentHealth -= dmg;

        // If character takes damage, set the I-Frames timer
        invincibilityTimer = invincibilityDuration;
        isInvincible = true;

        if (currentHealth <= 0)
        {
            Die();
        }
        
        UpdateHealthBar();
    }
    [ContextMenu("testdie")]
    void Die()
    {
        audioManager.PlaySFX(audioManager.deathMusic);
        ScoreBoard.Instance.timeScoreboard = GameManager.instance.stopWatchDisplay;
        ScoreBoard.Instance.lvPlayer = level;
        ScoreBoard.Instance.weaponSlots = new(inventory.weaponSlots);
        ScoreBoard.Instance.passiveSlots = new(inventory.passiveItemSlots);
        GameManager.instance.TriggerGameOver();
    
    }
    
    // Character health regeneration
    void Recover()
    {
        if (currentRecovery == 0)
        {
            return;
        }
        if (currentHealth < characterData.MaxHealth)
        {
            currentHealth += currentRecovery * Time.deltaTime;

            // To make sure currentHealth never exceed maxHealth
            if (currentHealth > characterData.MaxHealth)
            {
                currentHealth = characterData.MaxHealth;
            }
        }
    }

    public void AcquireWeapon(GameObject wp)
    {
        // Instantiate the weapon and then set the weapon to be the child of the character
        GameObject spawnedWeapon = Instantiate(wp, transform.position, Quaternion.identity);
        spawnedWeapon.transform.SetParent(this.transform);
        // Put the weapon into the inventory
        inventory.AddWeapon(weaponId, spawnedWeapon.GetComponent<Weapon>());
        weaponId += 1;
    }

    public void AcquirePassiveItem(GameObject item)
    {
        // Instantiate the item and then set the item to be the child of the character
        GameObject spawnedItem = Instantiate(item, transform.position, Quaternion.identity);
        spawnedItem.transform.SetParent(this.transform);
        // Put the item into the inventory
        inventory.AddPassiveItem(itemId, spawnedItem.GetComponent<PassiveItem>());
        itemId += 1;
    }

    // Update health bar UI
    public void UpdateHealthBar()
    {
        healthBar.fillAmount = currentHealth / characterData.MaxHealth;
    }

    // Update experience bar UI
    public void UpdateExpBar()
    {
        expBar.fillAmount = (float) exp / expCap;
    }

    // Update level text UI
    public void UpdateLevelText()
    {
        levelText.text = "Lv " + level.ToString();
    }
}
