using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreUIController : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI scoreText;
	[SerializeField] private TMPro.TextMeshProUGUI rankText;


	private bool RestartCheck = false;
	private bool NextCheck = false;


	void Start()
	{
		scoreText.text = "" + GameManager.instance.GetScore();
		rankText.text = GameManager.instance.GetRank();
		RestartCheck = false;
		NextCheck = false;
	}

	public void RestartStage()
	{
		if (RestartCheck)
			return;
		RestartCheck = true;
		GameManager.instance.RestartStage();
	}

	public void NextStage()
	{
		if (NextCheck)
			return;
		NextCheck = true;
		GameManager.instance.NextStage();
	}
}
