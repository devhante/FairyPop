using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	private static GameManager instance = null;
	
	public static GameManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = FindObjectOfType(typeof(GameManager)) as GameManager;

				if(instance == null)
				{
					Debug.LogError("No GameManager");
				}
			}

			return instance;
		}
	}

	public static bool flag1 = true;
	public static bool flag2 = false;
	public static bool flag3 = false;

	private void Awake()
	{
		DontDestroyOnLoad(this);
	}
}
