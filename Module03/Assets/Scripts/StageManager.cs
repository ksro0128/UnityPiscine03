using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
	public static StageManager instance { get; private set; }
	[SerializeField] private int money = 200;
	[SerializeField] private int health = 5;
	[SerializeField] private int enemyCount = 0;
	[SerializeField] private int spawnCount = 20;

	[SerializeField] private GameObject pauseMenu;

	[SerializeField] private TMPro.TextMeshProUGUI moneyText;
	[SerializeField] private TMPro.TextMeshProUGUI healthText;
	[SerializeField] private TMPro.TextMeshProUGUI stageText;
	[SerializeField] private GameObject checkPanel;

	private bool isPaused = false;
	private bool spawning = true;

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}

    void Start()
    {
		moneyText.text = "" + money;
		healthText.text = "" + health;
		stageText.text = "Stage " + GameManager.instance.GetStage();
		GameManager.instance.ResumeGame();
    }

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (isPaused)
			{
				GameManager.instance.ResumeGame();
				isPaused = false;
				pauseMenu.SetActive(false);
			}
			else
			{
				GameManager.instance.PauseGame();
				isPaused = true;
				pauseMenu.SetActive(true);
			}
		}
	}

	public void AddMoney(int amount)
	{
		money += amount;
		moneyText.text = "" + money;
	}

	public void RemoveMoney(int amount)
	{
		money -= amount;
		moneyText.text = "" + money;
	}

	public void TakeDamage(int damage)
	{
		health -= damage;
		healthText.text = "" + health;
		if (health <= 0)
		{
			GameManager.instance.GameOver();
		}
	}

	public int GetMoney()
	{
		return money;
	}

	public int GetHealth()
	{
		return health;
	}

	public int GetSpawnCount()
	{
		return spawnCount;
	}

	public void DecreaseSpawnCount()
	{
		spawnCount--;
		enemyCount++;
		if (spawnCount <= 0)
			spawning = false;
	}

	public int GetEnemyCount()
	{
		return enemyCount;
	}

	public void DecreaseEnemyCount()
	{
		enemyCount--;
		if (enemyCount <= 0 && !spawning)
		{
			Debug.Log("Stage Clear");
			GameManager.instance.StageClear(money, health);
			return ;
		}
	}

	public void pushPauseMenuButton()
	{
		if (isPaused)
		{
			GameManager.instance.ResumeGame();
			isPaused = false;
			pauseMenu.SetActive(false);
		}
		else
		{
			GameManager.instance.PauseGame();
			isPaused = true;
			pauseMenu.SetActive(true);
		}
	}

	public void pushPauseMenuResumeButton()
	{
		GameManager.instance.ResumeGame();
		pauseMenu.SetActive(false);
		isPaused = false;
	}

	public void pushPauseMenuRestartButton()
	{
		GameManager.instance.RestartGame();
	}

	public void pushPauseMenuHomeButton()
	{
		checkPanel.SetActive(true);
	}

	public void pushCheckPanelYesButton()
	{
		SceneManager.Instance.LoadScene("Menu");
	}

	public void pushCheckPanelNoButton()
	{
		checkPanel.SetActive(false);
	}
}
