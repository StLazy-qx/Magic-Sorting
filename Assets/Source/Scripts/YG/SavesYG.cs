using Assets.Source.Scripts.Extensions;
using System.Collections.Generic;
using YG.Insides;

namespace YG
{
    public partial class SavesYG
    {
        private const int FirstRoundNumber = 1;

        private int TotalPoints;
        private int MainPoints;
        private int ActualRoundNumber;
        private string EquippedItem;
        private List<string> ItemIDs = new();

        public int Points => MainPoints;
        public string EquippedItemID => EquippedItem;

        public void SaveScore(int points)
        {
            Guard.NotNegative(points, nameof(points));

            MainPoints = points;
            TotalPoints += points;

            if (YG2.isSDKEnabled)
                YG2.SetLeaderboard("GameLeaderboard", TotalPoints);
        }

        public void DecreaseScore(int points)
        {
            Guard.NotNegative(points, nameof(points));

            MainPoints = points;
        }

        public void AddItem(string itemID)
        {
            Guard.NotNullOrWhiteSpace(itemID, nameof(itemID));

            if (ItemIDs.Contains(itemID))
                return;

            ItemIDs.Add(itemID);
            YG2.SaveProgress();
        }

        public void SaveRoundNumber(int number)
        {
            Guard.NotNegative(number, nameof(number));

            if (ActualRoundNumber == number)
                return;

            ActualRoundNumber = number;
        }

        public void SaveEquippedItem(string itemID)
        {
            Guard.NotNullOrWhiteSpace(itemID, nameof(itemID));

            EquippedItem = string.IsNullOrWhiteSpace(itemID)
                ? string.Empty
                : itemID;

            YG2.SaveProgress();
        }

        public int GetRoundNumber()
        {
            if (ActualRoundNumber <= 0)
                ActualRoundNumber = FirstRoundNumber;

            return ActualRoundNumber;
        }

        public IReadOnlyList<string> GetPurchasedItems()
        {
            YGInsides.LoadProgress();

            return ItemIDs.AsReadOnly();
        }
    }
}