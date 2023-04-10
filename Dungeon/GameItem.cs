using System;
using System.Xml.Linq;
using static DungeonGame.Interfaces;
using static System.Net.Mime.MediaTypeNames;

//The IGameItem class is used throughout the game. The only part of it that has been
//unusable is the CarryCapacity, as we were unable to implement it properly.
namespace DungeonGame
{

	public class IGameItem : IGameItems
	{
		public string Name { get; set; }
		private float _weight { get; set; }
		private float _carryCapacity { get; set; }
		public int Damage { get; set; }
		public int ArmorValue { get; set; }
		public int Health { get; set; }
		public bool IsUsable { get; set; }
		public string Description { get; set; }

		public float CarryCapacity
		{
			get
			{
				return _carryCapacity;
			}
			set
			{
				_carryCapacity = 100f;
			}
		}
		public float Weight
		{
			get
			{
				return _weight + (_decorator == null ? 0 : _decorator.Weight);
			}
			set
			{
				_weight = value;
			}
		}

		private IGameItems _decorator;

		public IGameItem(): this("Disheveled Waste", 0.0f, 1, 1, 1,"Disheveled waste leftover from the genesis, maybe it could be thrown?", false) 
		{
		}
		public IGameItem(string name, float weight, int damage, int health, int armorValue, string description, bool isUsable)
		{
			Name = name;
			Weight = weight;
			_decorator = null;
			Damage = damage;
			Health = health;
			ArmorValue = armorValue;
			Description = description;
			IsUsable = false;

		}
		
		public void AddDecorator(IGameItems decorator)
		{
			if (_decorator == null)
			{
				_decorator = decorator;
			}
			else
			{
				_decorator.AddDecorator(decorator);
			}

		}
		public string LongName()
		{
			return Name + (_decorator == null ? "" : ", " + _decorator.LongName());
		}

		override
	public string ToString()
		{
			if (IsUsable == true)
			{
				return Name + "(" + LongName() + ")" + ", Weight: " + Weight + "Damge:" + Damage + "HP:" + Health + "AV:" + ArmorValue;
			}
			else
			{
				return Name + "(" + LongName() + ")" + ", Weight: " + Weight;

			}


		}
	}
}
