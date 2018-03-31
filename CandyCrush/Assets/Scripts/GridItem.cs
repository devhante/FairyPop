using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridItem : MonoBehaviour
{
	public int X { get; private set; }
	public int Y { get; private set; }

	public int id;

	public void OnItemPositionChanged(int newX, int newY)
	{
		X = newX;
		Y = newY;

		gameObject.name = string.Format("Sprite [{0}][{1}]", X, Y);
	}

	private void OnMouseDown()
	{
		if(OnMouseOverItemEventHandler != null)
		{
			OnMouseOverItemEventHandler(this);
		}
	}

	public delegate void OnMouseOverItem(GridItem item);
	public static event OnMouseOverItem OnMouseOverItemEventHandler;
}
