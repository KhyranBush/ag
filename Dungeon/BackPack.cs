using System;
using System.Collections.Generic;
using System.Linq;
using static DungeonGame.Interfaces;

//This class allows the player to store items like in any rpg
//to add a more functional and organized gameplay experience I have created multiple bags for the player for
//differennt pllayer items, one for misc items and keys and one for weapons 
//
namespace DungeonGame
{
	
	public class BackPack : IGameItemsContainer, IGameItems
	{//one dictionary contains miscellaneous loree items and one dictionary contains weapon items
		private Dictionary<string, IGameItems> GItems;
		
		public string Name { get; set; }
		private float _weight;
        public int Damage { get; set; }
        public int ArmorValue { get; set; }
		public string Description { get; set; }
		public int Health { get; set; }
        public bool IsUsable { get; set; }
		public float _carryMax { get; set; }
        private float _carryCapacity { get; set; }
		public float Weight {
			get
			{
				float Weight = _weight;
				foreach(IGameItems gameitem in GItems.Values)
				{
					Weight += gameitem.Weight;
				}
				return Weight;
			}
			set
			{
				_weight = value;
			}
				}
        public float CarryCapacity { // come back to this if you have time to add more functionality with level increase
			get
			{
				float Max = _carryCapacity;
				return Max;
			}
			set
			{
				_carryMax = 1000.0f;
			}
				 }

		//---------backpack control sector------------------
		public BackPack()
		{
			GItems = new Dictionary<string, IGameItems>();
			
		}
		public void put(IGameItems Gitem)
		{
			if(Weight > CarryCapacity)
			{
				Console.WriteLine("This is too heavy!");
			}
			else
			{
				GItems[Gitem.Name] = Gitem;
			}
		}
		public bool contains(IGameItems GI)
		{
			
            Dictionary<string, IGameItems>.ValueCollection keys = GItems.Values;
            if (keys.Contains(GI))
			{
				return true; 
			}
			return false;
			
		}
		
		public IGameItems remove ( string GItemName)
		{
			IGameItems GItem = null;
			GItems.Remove(GItemName, out GItem);
			return GItem;
		}
		
		public void  AddDecorator(IGameItems decorator)
		{

		}

		public string contents()
		{
			string GItemNames = "Items: \n";
			Dictionary<string, IGameItems>.KeyCollection keys = GItems.Keys;
			foreach(string GitemName in keys)
			{
				GItemNames += " " + GItems[GitemName].ToString() + "\n";
			}
			return GItemNames;
		}
      
		public string LongName()
		{
            return "";
        }
		override
		public string ToString()
		{
			return Name + ", Weight: " + Weight;
		}

		public IGameItems put(string gitemName)
		{
			throw new NotImplementedException();
		}
	}
}
