public class Card {
	public bool IsFaceUp { get; set; }
	public bool IsMatched { get; set; }
	public string Image { get; set; }
	public int ID { get; set; }

	public Card(string img, int id)
	{
		Image = img;
		ID = id;
	}
}
