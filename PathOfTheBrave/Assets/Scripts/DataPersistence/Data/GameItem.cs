using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.DataPersistence.Data
{
    public class GameItem
    {
        public int Id { get; set; }

        public string SpriteName { get; set; }

        public string Description { get; set; }

        public bool IsUsedInInventory {  get; set; }

        public bool IsForever { get;set; }  

        public bool IsEffectByTime { get; set; }

        public float EffectTime { get; set; }

        public float Coin { get;set; }

        public GameItem()
        {
        }

        public GameItem(int id, string spriteName, string description, bool isUsedInInventory, bool isForever, bool isEffectByTime, float effectTime, float coin)
        {
            Id = id;
            SpriteName = spriteName;
            Description = description;
            IsUsedInInventory = isUsedInInventory;
            IsForever = isForever;
            IsEffectByTime = isEffectByTime;
            EffectTime = effectTime;
            Coin = coin;
        }
    }
}
