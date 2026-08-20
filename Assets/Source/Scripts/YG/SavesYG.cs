//using Assets.Source.Scripts.Extensions;
using Assets.Source.Scripts.YG;
using System.Collections.Generic;
//using UnityEngine;

namespace YG
{
    public partial class SavesYG
    {
        //private const int FirstRoundNumber = 1;
        private const float BeginVolume = 0.5f;

        public int MainPoints;
        public int ActualRoundNumber;
        public float MasterVolume = BeginVolume;
        public float AmbientVolume = BeginVolume;
        public float EffectVolume = BeginVolume;
        public string EquippedItem;
        public List<string> ItemIDs = new();

        private ScoreSection _score;
        private RoundSection _round;
        private AudioSettingsSection _audio;
        private InventorySection _inventory;

        public ScoreSection Score => _score ??= new ScoreSection(this);
        public RoundSection Round => _round ??= new RoundSection(this);
        public AudioSettingsSection Audio => _audio ??= new AudioSettingsSection(this);
        public InventorySection Inventory => _inventory ??= new InventorySection(this);

        public int Points => Score.Points;
        public string EquippedItemID => Inventory.EquippedItemID;

        public void SaveScore(int points) 
            => Score.SaveScore(points);

        public void DecreaseScore(int points) 
            => Score.DecreaseScore(points);

        public void AddItem(string itemID) 
            => Inventory.AddItem(itemID);

        public void SaveEquippedItem(string itemID) 
            => Inventory.SaveEquippedItem(itemID);

        public IReadOnlyList<string> GetPurchasedItems() 
            => Inventory.GetPurchasedItems();

        public void SaveRoundNumber(int number) 
            => Round.SaveRoundNumber(number);

        public int GetRoundNumber() 
            => Round.GetRoundNumber();

        public void SaveMasterVolume(float value) 
            => Audio.SaveMasterVolume(value);

        public void SaveAmbientVolume(float value) 
            => Audio.SaveAmbientVolume(value);

        public void SaveEffectVolume(float value) 
            => Audio.SaveEffectVolume(value);

        //public int Points => MainPoints;
        //public string EquippedItemID => EquippedItem;

        //public void SaveScore(int points)
        //{
        //    Guard.NotNegative(points, nameof(points));

        //    MainPoints = points;

        //    if (YG2.isSDKEnabled)
        //        YG2.SetLeaderboard("GameLeaderboard", MainPoints);

        //    YG2.SaveProgress();
        //}

        //public void DecreaseScore(int points)
        //{
        //    Guard.NotNegative(points, nameof(points));

        //    MainPoints = points;

        //    YG2.SaveProgress();
        //}

        //public void AddItem(string itemID)
        //{
        //    Guard.NotNullOrWhiteSpace(itemID, nameof(itemID));

        //    if (ItemIDs.Contains(itemID))
        //        return;

        //    ItemIDs.Add(itemID);
        //    YG2.SaveProgress();
        //}

        //public void SaveRoundNumber(int number)
        //{
        //    Guard.NotNegative(number, nameof(number));

        //    if (ActualRoundNumber == number)
        //        return;

        //    ActualRoundNumber = number;

        //    YG2.SaveProgress();
        //}

        //public void SaveMasterVolume(float value)
        //{
        //    float clamped = Mathf.Clamp01(value);

        //    if (MasterVolume == clamped)
        //        return;

        //    MasterVolume = clamped;

        //    YG2.SaveProgress();
        //}

        //public void SaveAmbientVolume(float value)
        //{
        //    float clamped = Mathf.Clamp01(value);

        //    if (AmbientVolume == clamped)
        //        return;

        //    AmbientVolume = clamped;

        //    YG2.SaveProgress();
        //}

        //public void SaveEffectVolume(float value)
        //{
        //    float clamped = Mathf.Clamp01(value);

        //    if (EffectVolume == clamped)
        //        return;

        //    EffectVolume = clamped;

        //    YG2.SaveProgress();
        //}

        //public void SaveEquippedItem(string itemID)
        //{
        //    Guard.NotNullOrWhiteSpace(itemID, nameof(itemID));

        //    EquippedItem = string.IsNullOrWhiteSpace(itemID)
        //        ? string.Empty
        //        : itemID;

        //    YG2.SaveProgress();
        //}

        //public int GetRoundNumber()
        //{
        //    return ActualRoundNumber > 0 ? 
        //        ActualRoundNumber : 
        //        FirstRoundNumber;
        //}

        //public IReadOnlyList<string> GetPurchasedItems()
        //{
        //    return ItemIDs.AsReadOnly();
        //}
    }
}