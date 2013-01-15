using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameScript : MonoBehaviour {
	public GUISkin customSkin;
	int cols = 4;
	int rows = 4;
	int totalCards = 16;
	int matchesNeededToWin;
	int matchesMade = 0;
	int cardW = 100;
	int cardH = 100;
	List<Card> cards;
	List<IList> grid;
	List<Card> cardsFlipped;
	bool playerCanClick;
	bool playerHasWon = false;
	
	// Use this for initialization
	void Start () 
	{
		playerCanClick = true;
		matchesNeededToWin = (int)(totalCards * 0.5);
		
		cards = new List<Card>();
		grid = new List<IList>();
		cardsFlipped = new List<Card>();
		
		BuildDeck();
		
		for(int i = 0; i < rows; i++)
		{
			grid.Add(new ArrayList());
			
			for(int j = 0; j < cols; j++)
			{
				int someNumber = Random.Range(0, cards.Count);
				grid[i].Add(cards[someNumber]);
				cards.RemoveAt(someNumber);
			}
		}
	}
	
	// Update is called once per frame
	void OnGUI () {
		GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
		BuildGrid();
		if(playerHasWon) BuildWinPrompt();
		GUILayout.EndArea();
	}
	
	private void BuildGrid()
	{
		GUILayout.BeginVertical();
		GUILayout.FlexibleSpace();
		
		for(int i = 0; i < rows; i++)
		{
			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			
			for(int j = 0; j < rows; j++)
			{
				Card card = (Card)grid[i][j];
				string image;
				
				if(card.IsMatched)
				{
					image = "blank";
				}
				else
				{
					if(card.IsFaceUp)
					{
						image = card.Image;
					}
					else
					{
						image = "wrench";
					}
				}
				
				GUI.enabled = !card.IsMatched;
				if(GUILayout.Button((Texture)Resources.Load(image), GUILayout.Width(cardW)))
				{	
					if(playerCanClick)
					{
						StartCoroutine(FlipCardFaceUp(card));
					}
				}
			}
			
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
		}
		
		GUILayout.FlexibleSpace();
		GUILayout.EndVertical();
	}
	
	private void BuildDeck()
	{
		int totalRobots = 4;
		Card card;
		int id = 0;
		
		for(int i = 0; i < totalRobots; i++)
		{
			List<string> robotParts = new List<string>() {"Head", "Arm", "Leg"};
			
			for(int j = 0; j < 2; j++)
			{
				int someNumber = Random.Range(0, robotParts.Count);
				string theMissingPart = robotParts[someNumber];
				
				robotParts.RemoveAt(someNumber);
				
				card = new Card("robot" + (i + 1) + "Missing" + theMissingPart, id);
				cards.Add(card);
				
				card = new Card("robot" + (i + 1) + theMissingPart, id);
				cards.Add(card);
				id++;
			}
		}
	}
	
	private IEnumerator FlipCardFaceUp(Card card)
	{
		card.IsFaceUp = true;
		cardsFlipped.Add(card);
		
		if(cardsFlipped.IndexOf(card) > 0)
		{
			playerCanClick = false;
			yield return new WaitForSeconds(1);
			
			if(cardsFlipped[0].ID == cardsFlipped[1].ID)
			{
				cardsFlipped[0].IsMatched = true;
				cardsFlipped[1].IsMatched = true;
				matchesMade++;
				
				if(matchesMade >= matchesNeededToWin)
				{
					playerHasWon = true;
				}
			}
			else
			{
				cardsFlipped[0].IsFaceUp = false;
				cardsFlipped[1].IsFaceUp = false;
			}

			cardsFlipped = new List<Card>();
			playerCanClick = true;
		}
	}
	
	private void BuildWinPrompt()
	{
		int winPromptW = 100;
		int winPromptH = 90;
		
		float halfScreenW = Screen.width/2;
		float halfScreenH = Screen.height/2;
		
		int halfPromptW = winPromptW/2;
		int halfPromptH = winPromptH/2;
		
		GUI.BeginGroup(new Rect(halfScreenW - halfPromptW, halfScreenH - halfPromptH, winPromptW, winPromptH));
		GUI.Box(new Rect(0, 0, winPromptW, winPromptH), "A Winner is You!!");
		
		if(GUI.Button(new Rect(10, 40, 80, 20), "Play Again"))
		{
			Application.LoadLevel("Title");
		}
		
		GUI.EndGroup();
	}
}
