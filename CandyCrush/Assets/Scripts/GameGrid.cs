using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid : MonoBehaviour
{
	public int xSize;
	public int ySize;
	public float candyWidth = 1f;
	public static int minItemsForMatch = 3;
	public float delayBetweenMatches = 0.2f;
	public bool canPlay;
	public static int score = 0;

	public GameObject Pang;

	public Stage1 stage1;

	public GameObject[] candies;

	GridItem[,] items;
	GridItem currentlySelectedItem;

	private void Start()
	{
		canPlay = true;

		FillGrid();
		//ClearGrid();
		GridItem.OnMouseOverItemEventHandler += OnMouseOverItem;
	}

	private void OnDisable()
	{
		GridItem.OnMouseOverItemEventHandler -= OnMouseOverItem;
	}

	void FillGrid()
	{
		items = new GridItem[xSize, ySize];

		for(int x = 0; x < xSize; x++)
		{
			for(int y = 0; y < ySize; y++)
			{
				items[x, y] = InstantiateCandy(x, y);
			}
		}
	}

	void ClearGrid()
	{
		for(int x = 0; x < xSize; x++)
		{
			for(int y = 0; y < ySize; y++)
			{
				MatchInfo matchInfo = GetMatchInformation(items[x, y]);

				if(matchInfo.ValidMatch)
				{
					Debug.Log ("x : " + x + ", y : " + y);

					Destroy(items[x, y].gameObject);
					items[x, y] = InstantiateCandy(x, y);
					y--;
				}
			}
		}
	}

	GridItem InstantiateCandy(int x, int y)
	{
		GameObject randomCandy = candies[Random.Range(0, candies.Length)];
		GridItem newCandy = Instantiate(randomCandy, new Vector3(x * candyWidth, y), Quaternion.identity).GetComponent<GridItem>();
		newCandy.OnItemPositionChanged(x, y);

		return newCandy;
	}

	void OnMouseOverItem(GridItem item)
	{
		if (item == currentlySelectedItem || !canPlay)
		{
			return;
		}

		if (currentlySelectedItem == null)
		{
			currentlySelectedItem = item;
		}

		else
		{
			float xDiff = Mathf.Abs(item.X - currentlySelectedItem.X);
			float yDiff = Mathf.Abs(item.Y - currentlySelectedItem.Y);

			if(xDiff + yDiff == 1)
			{
				StartCoroutine(TryMatch(item, currentlySelectedItem)); 
			}

			currentlySelectedItem = null;
		}
	}

	IEnumerator TryMatch(GridItem a, GridItem b)
	{
		canPlay = false;

		yield return StartCoroutine(Swap(a, b));

		MatchInfo matchA = GetMatchInformation(a);
		MatchInfo matchB = GetMatchInformation(b);
		
		if(!matchA.ValidMatch && !matchB.ValidMatch)
		{
			yield return StartCoroutine(Swap(a, b));
		}

		if(matchA.ValidMatch)
		{
			yield return StartCoroutine(DestroyItems(matchA.match));
			yield return new WaitForSeconds(delayBetweenMatches);
			yield return StartCoroutine(UpdateGridAfterMatch(matchA));
		}
		else if (matchB.ValidMatch)
		{
			yield return StartCoroutine(DestroyItems(matchB.match));
			yield return new WaitForSeconds(delayBetweenMatches);
			yield return StartCoroutine(UpdateGridAfterMatch(matchB));
		}

		canPlay = true;
	}

	IEnumerator UpdateGridAfterMatch(MatchInfo match)
	{
		if (match.matchStartingY == match.matchEndingY)
		{
			for (int x = match.matchStartingX; x <= match.matchEndingX; x++)
			{
				for (int y = match.matchStartingY; y < ySize - 1; y++)
				{
					GridItem upperIndex = items[x, y + 1];
					GridItem current = items[x, y];
					items[x, y] = upperIndex;
					items[x, y + 1] = current;
					items[x, y].OnItemPositionChanged(items[x, y].X, items[x, y].Y - 1);
				}

				items[x, ySize - 1] = InstantiateCandy(x, ySize - 1);
			}
		}

		else if (match.matchStartingX == match.matchEndingX)
		{
			int matchHeight = (match.matchEndingY - match.matchStartingY) + 1;
			for (int y = match.matchStartingY + matchHeight; y <= ySize - 1; y++)
			{
				GridItem lowerIndex = items[match.matchStartingX, y - matchHeight];
				GridItem current = items[match.matchStartingX, y];

				items[match.matchStartingX, y - matchHeight] = current;
				items[match.matchStartingX, y] = lowerIndex;
			}

			for (int y = 0; y < ySize - matchHeight; y++)
			{
				items[match.matchStartingX, y].OnItemPositionChanged(match.matchStartingX, y);
			}

			for (int i = 0; i < match.match.Count; i++)
			{
				items[match.matchStartingX, (ySize - 1) - i] = InstantiateCandy(match.matchStartingX, (ySize - 1) - i);
			}
		}

		for (int x = 0; x < xSize; x++)
		{
			for (int y = 0; y < ySize; y++)
			{
				MatchInfo matchInfo = GetMatchInformation(items[x, y]);

				if (matchInfo.ValidMatch)
				{
					yield return new WaitForSeconds(delayBetweenMatches);
					yield return StartCoroutine(DestroyItems(matchInfo.match));
					yield return new WaitForSeconds(delayBetweenMatches);
					yield return StartCoroutine(UpdateGridAfterMatch(matchInfo));
				}
			}
		}
	}

	IEnumerator DestroyItems(List<GridItem> items)
	{
		foreach(GridItem i in items)
		{
			yield return StartCoroutine(i.transform.Scale(Vector3.zero, 0.1f));

			if (i.X <= 2) stage1.Fill("Blue");
			if (i.X >= 2) stage1.Fill("Red");
			Destroy(i.gameObject);
			score += 50;
		}
	}

	IEnumerator Swap(GridItem a, GridItem b)
	{
		float moveDuration = 0.1f;
		Vector3 aPosition = a.transform.position;

		ChangeRigidbodyStatus(false);
		StartCoroutine(a.transform.Move(b.transform.position, moveDuration));
		StartCoroutine(b.transform.Move(aPosition, moveDuration));
		yield return new WaitForSeconds(moveDuration);
		SwapIndices(a, b);
		ChangeRigidbodyStatus(true);
	} 

	void SwapIndices(GridItem a, GridItem b)
	{
		GridItem tempA = items[a.X, a.Y];
		items[a.X, a.Y] = b;
		items[b.X, b.Y] = tempA;

		int bOldX = b.X;
		int bOldY = b.Y;
		b.OnItemPositionChanged(a.X, a.Y);
		a.OnItemPositionChanged(bOldX, bOldY);
	}

	List<GridItem> SearchHorizontally(GridItem item)
	{
		List<GridItem> hItems = new List<GridItem> { item };

		int left = item.X - 1;
		int right = item.X + 1;

		while(left >= 0 && items[left, item.Y].id == item.id)
		{
			hItems.Add(items[left, item.Y]);
			left--;
		}

		while (right < xSize && items[right, item.Y].id == item.id)
		{
			hItems.Add(items[right, item.Y]);
			right++;
		}

		return hItems;
	}

	List<GridItem> SearchVertically(GridItem item)
	{
		List<GridItem> vItems = new List<GridItem> { item };

		int lower = item.Y - 1;
		int upper = item.Y + 1;

		while(lower >= 0 && items[item.X, lower].id == item.id)
		{
			vItems.Add(items[item.X, lower]);
			lower--;
		}

		while (upper < ySize && items[item.X, upper].id == item.id)
		{
			vItems.Add(items[item.X, upper]);
			upper++;
		}

		return vItems;
	}

	MatchInfo GetMatchInformation(GridItem item)
	{
		MatchInfo m = new MatchInfo { match = null };

		List<GridItem> hMatch = SearchHorizontally(item);
		List<GridItem> vMatch = SearchVertically(item);

		if(hMatch.Count >= minItemsForMatch && vMatch.Count >= minItemsForMatch)
		{
			m.isSpecial = true;
			m.matchStartingX = GetMinimumX(hMatch);
			m.matchEndingX = GetMaximumX(hMatch);
			m.matchStartingY = GetMinimumY(vMatch);
			m.matchEndingY = GetMaximumY(vMatch);
			
			m.match = new List<GridItem>();
			m.match.AddRange(hMatch);
			m.match.AddRange(vMatch);
		}

		if(hMatch.Count >= minItemsForMatch && hMatch.Count > vMatch.Count)
		{
			m.matchStartingX = GetMinimumX(hMatch);
			m.matchEndingX = GetMaximumX(hMatch);
			m.matchStartingY = m.matchEndingY = hMatch[0].Y;
			m.match = hMatch;
		}
		else if(vMatch.Count >= minItemsForMatch)
		{
			m.matchStartingY = GetMinimumY(vMatch);
			m.matchEndingY = GetMaximumY(vMatch);
			m.matchStartingX = m.matchEndingX = hMatch[0].X;
			m.match = vMatch;
		}

		return m;
	}

	int GetMinimumX(List<GridItem> items)
	{
		float[] indices = new float[items.Count];
		
		for(int i = 0; i < indices.Length; i++)
		{
			indices[i] = items[i].X;
		}

		return (int)Mathf.Min(indices);
	}

	int GetMaximumX(List<GridItem> items)
	{
		float[] indices = new float[items.Count];

		for (int i = 0; i < indices.Length; i++)
		{
			indices[i] = items[i].X;
		}

		return (int)Mathf.Max(indices);
	}

	int GetMinimumY(List<GridItem> items)
	{
		float[] indices = new float[items.Count];

		for (int i = 0; i < indices.Length; i++)
		{
			indices[i] = items[i].Y;
		}

		return (int)Mathf.Min(indices);
	}

	int GetMaximumY(List<GridItem> items)
	{
		float[] indices = new float[items.Count];

		for (int i = 0; i < indices.Length; i++)
		{
			indices[i] = items[i].Y;
		}

		return (int)Mathf.Max(indices);
	}

	void ChangeRigidbodyStatus(bool status)
	{
		foreach(GridItem g in items)
		{
			g.GetComponent<Rigidbody2D>().isKinematic = !status;
		}
	}
}
