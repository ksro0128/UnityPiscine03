using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class GameManager : MonoBehaviour
{
	public static GameManager instance { get; private set; }
	[SerializeField] private int score;
	[SerializeField] private int totalScore;
	[SerializeField] private string rank = "F";
	[SerializeField] private string totalRank = "F";
	private string[] ranks = { "F", "D", "C", "B", "A", "S" };

	[SerializeField] private int currentIdx = 0;
	[SerializeField] private string[] scenes = { "Map1", "Map2", "Map3" };

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	public void GameOver()
	{
		Debug.Log("Game Over!");
		


		EnemySpawner spawner = FindObjectOfType<EnemySpawner>();
		if (spawner != null)
			spawner.StopSpawning();
		EnemySpawner2 spawner2 = FindObjectOfType<EnemySpawner2>();
		if (spawner2 != null)
			spawner2.StopSpawning();
		EnemySpawner3 spawner3 = FindObjectOfType<EnemySpawner3>();
		if (spawner3 != null)
			spawner3.StopSpawning();

		foreach (EnemyController enemy in FindObjectsOfType<EnemyController>())
		{
			Destroy(enemy.gameObject);
		}
		SceneManager.Instance.LoadScene("GameOver");
	}

	public void ExitGame()
	{
		Application.Quit();

		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#endif
	}

	public void PauseGame()
	{
		Time.timeScale = 0;
	}

	public void ResumeGame()
	{
		Time.timeScale = 1;
	}

	public void RestartGame()
	{
		Time.timeScale = 1;
		UnityEngine.SceneManagement.SceneManager.LoadScene(scenes[currentIdx]);
	}

	public void StageClear(int money, int health)
	{
		score = 0;
		score += money;
		score += health * 100;

		if (score >= 600 && health == 5)
		{
			rank = ranks[5];
		}
		else if (score >= 500)
		{
			rank = ranks[4];
		}
		else if (score >= 400)
		{
			rank = ranks[3];
		}
		else if (score >= 300)
		{
			rank = ranks[2];
		}
		else if (score >= 200)
		{
			rank = ranks[1];
		}
		else
		{
			rank = ranks[0];
		}
		if (currentIdx == 2)
		{
			totalScore += score;
			if (totalScore >= 2000)
			{
				totalRank = ranks[5];
			}
			else if (totalScore >= 1500)
			{
				totalRank = ranks[4];
			}
			else if (totalScore >= 1200)
			{
				totalRank = ranks[3];
			}
			else if (totalScore >= 900)
			{
				totalRank = ranks[2];
			}
			else if (totalScore >= 600)
			{
				totalRank = ranks[1];
			}
			else
			{
				totalRank = ranks[0];
			}
			SceneManager.Instance.LoadScene("Clear");
		}
		else
			SceneManager.Instance.LoadScene("Score");
	}

	public void NextStage()
	{
		if (currentIdx < scenes.Length - 1)
		{
			currentIdx++;
			totalScore += score;
			SceneManager.Instance.LoadScene(scenes[currentIdx]);
		}
		else
		{
			GameClear();
		}
	}

	public void RestartStage()
	{
		SceneManager.Instance.LoadScene(scenes[currentIdx]);
	}

	public void GameClear()
	{
		Debug.Log("Game Clear!");
	}

	public string GetRank()
	{
		return rank;
	}

	public int GetScore()
	{
		return score;
	}

	public int GetStage()
	{
		return currentIdx + 1;
	}

	public void SetStage(int idx)
	{
		currentIdx = idx;
	}

	public int GetTotalScore()
	{
		return totalScore;
	}

	public void ResetTotalScore()
	{
		totalScore = 0;
	}

	public string GetTotalRank()
	{
		return totalRank;
	}

}
