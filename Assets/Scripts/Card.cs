namespace SMG
{
    public class Card
    {
        public int Id { get; private set; }
        public ECardType CardType { get; private set; }

        public Card(int id, ECardType cardType)
        {
            Id = id;
            CardType = cardType;
        }

        public override string ToString()
        {
            return string.Format("Id = {0}, CardType = {1}", Id, CardType);
        }
    }
}