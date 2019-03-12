using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Lives : MonoBehaviour 
{

	public Text gameOver = null;
    public Text LivesText = null;
	public int lives = 3;

	void OnCollisionEnter2D(Collision2D other)
	{
		lives--;
		LivesText.text = ("Lives :" + lives);
		if (lives == 0)
		{
			Destroy(GameObject.Find("Player"));
			gameOver.gameObject.SetActive(true);
			SceneManager.LoadScene("SampleScene", LoadSceneMode.Additive);
            
		}
	}	
}

