using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using static DungeonGame.Interfaces;

namespace DungeonGame
{
    public class Merchant
    {
        private string _name;
        public string Name { get { return Name = _name; } set { } }
        private Dictionary<string, Merchant> merchants;
        private int Health = 100000; //Merchants are vitually immortal
        private String Dialogue;
        private Dictionary<string, IGameItems> GItems;
        private IGameItemsContainer Inventory;
       
        public Dictionary<string, Merchant>.KeyCollection Keys { get; internal set; }
        public Merchant(string name)
        {
            _name = name;
            Dialogue = "Hi I am Merchant " + name + "Please buy some items to aid in your acension";
            GItems = new Dictionary<string, IGameItems>();
            merchants = new Dictionary<string, Merchant>();
            Inventory = new BackPack();
            BasicItems();
        }
        public void give(IGameItems gameItems)
        {

            Inventory.put(gameItems);

        }
        public IGameItems take(string gameItems)
        {
            return Inventory.remove(gameItems);

        }
        public void showInventory()

        {
            //Hero hero = new Hero();
            Console.WriteLine("\nBazaars Randomness Emporiums Inventory:\n" + Inventory.contents() );

        }
        public void AddItems(string GitemName,int weight, int damage, int health, int armor, string desc, bool usableOrNot)
        
        {
            IGameItems shopWeps = new IGameItem(" ", 0, 0, 0, 0, " ", true);
            

            if(shopWeps!= null)
            {
                shopWeps.Name = GitemName;
                shopWeps.Weight = weight;
                shopWeps.Damage = damage;
                shopWeps.ArmorValue = armor;
                shopWeps.Description = desc;
                shopWeps.IsUsable = usableOrNot;

                give(shopWeps);
                
            }
            else
            {
                Console.WriteLine("Item does not exist!!!!!!");
            }
        }
        public void BasicItems()

        { 
            AddItems("Obadachi", 1, 15, 10, 10, "Legend has it the ancient Ascenders used this to defeat unbreakable foes", true);
            AddItems("FluxGrimoire", 1, 12, 10, 10, "A book detailing a marvelously devious tale... Gives off a white-gold luminescence", true);
        }
        
        public void RemoveItemsWhenBought(string Gitem)
        {


            Dictionary<string, IGameItems>.KeyCollection keys = GItems.Keys;

            
            foreach (string GitemName in keys)
            {
                if (GitemName != null)
                {
                   
                    Inventory.remove(GitemName);
                }
                else
                {
                    Console.WriteLine("There is no such thing as " + GitemName);
                    
                }

            }

        }
       
        public void put(IGameItems Gitem)
        {
            if (Gitem == null)
            {
                Console.WriteLine(Gitem + " Doesnt Exist!!!");
            }
            else
            {
                Inventory.put(Gitem);
            }
        }
        public IGameItems getItems(string Gitem)
        {
           

            if (Inventory.getItems(Gitem) != null)
            {
                return Inventory.getItems(Gitem);
            }
            else
            {
                throw new ArgumentException($"Weapon {Gitem} not found");
            }

        }

        public IGameItems remove(string GItemName)
        {
            IGameItems GItem = null;
            GItems.Remove(GItemName, out GItem);
            return GItem;
        }

        public void AddDecorator(IGameItems decorator)
        {

        }

        public string contents()
        {
            string GItemNames = " Merchant Items: \n";
            Dictionary<string, IGameItems>.KeyCollection keys = GItems.Keys;
            foreach (string GitemName in keys)
            {
                GItemNames += " " + GItems[GitemName].ToString() + "\n";
            }
            return GItemNames;
        }

       



    }
}
