using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stage1 : MonoBehaviour
{
	int t = 60;

	public GameObject Image1;
	public GameObject Image2;
	public Text text;
	public Text time;

	public Image Blue;
	public Image Red;

	private void Awake()
	{
		Image1.SetActive(true);
		Image2.SetActive(false);
		text.gameObject.SetActive(false);

		Blue.fillAmount = 0.5f;
		Red.fillAmount = 0.5f;
	}

	private void Start()
	{
		StartCoroutine("Image1Coroutine");
	}

	private void Update()
	{
		text.text = GameGrid.score.ToString();
		//if (GameGrid.score >= 1000) StartCoroutine("ClearCoroutine");
	}

	IEnumerator Image1Coroutine()
	{

		yield return new WaitForSeconds(3.0f);

		Image1.SetActive(false);
		text.gameObject.SetActive(true);
		StartCoroutine("TimeCoroutine");
	}

	IEnumerator Image2Coroutine()
	{
		Image2.SetActive(true);
		text.gameObject.SetActive(false);

		yield return new WaitForSeconds(3.0f);
		GameManager.flag2 = true;
		SceneManager.LoadScene(0);
	}

	IEnumerator ClearCoroutine()
	{
		yield return new WaitForSeconds(1.0f);
		text.text = "Clear!";
		yield return new WaitForSeconds(2.0f);
		StartCoroutine("Image2Coroutine");
	}

	IEnumerator TimeCoroutine()
	{
		while (t > 0)
		{
			time.text = t.ToString();

			yield return new WaitForSeconds(1.0f);
			t--;
		}

		yield return new WaitForSeconds(1.0f);
		text.text = "Failed!";
		yield return new WaitForSeconds(2.0f);
		SceneManager.LoadScene(0);
	}

	public void Fill(string type)
	{
		if(type == "Red")
		{
			Red.fillAmount += 0.01f;
		}

		else if(type == "Blue")
		{
			Blue.fillAmount += 0.01f;
		}

		if (Red.fillAmount >= 1 && Blue.fillAmount >= 1)
		{
			StartCoroutine("ClearCoroutine");
		}
	}
}
