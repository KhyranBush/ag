namespace DungeonGame
{
    public class Interfaces
    {
        public interface ILockable
        {
            bool IsLocked { get; }
            bool IsUnlocked { get; }
            bool CanClose { get; }
            void Lock();
            void Unlock();
        }
        public interface IRoomDelagate
        {
            //Software design pattern: Delegates
             Room RoomContainer { get; set; }
           
            Door getExit(string exitName);
            string getExits();
        }
        public interface IEnemyDelagate
        {
            //Software design pattern: Delegates
            Enemies EnemyContainer { get; set; }
            Enemies GetEnemy(string enemyName);
            string getEnemies();
        }
        public interface IMerchantDelagate
        {
            //Software design pattern: Delegates
            Merchant MerchantContainer { get; set; }
            Merchant GetMerchant(string merchantName);
            string getMerchants();
        }
        public interface IOCState
        {
            OCState State { get; }
            IOCState open();
            IOCState close();
            IOCState keyclose();
        }
        public interface IGameItemsContainer : IGameItems
        {
            void put(IGameItems item);
            IGameItems remove(string itemName);

            string contents();
            IGameItems getItems(string Gitem);
            string Name { get; set; }
            float Weight { get; set; }
            int Damage { get; set; }
            int ArmorValue { get; set; }
            int Health { get; set; }
            float CarryCapacity { get; set; }
            void AddDecorator(IGameItems decorator);
            string LongName();


            string ToString();
           // IGameItems put(string gitemName);
        }

        public interface IGameItems
        {
            string Name { get; set; }
            float Weight { get; set; }
            int Damage { get; set; }
            int ArmorValue { get; set; }
            float CarryCapacity { get; set; }
            bool IsUsable { get; set; }
            string Description { get; set; }
            int Health { get; set; }
            void AddDecorator( IGameItems decorator);
            string LongName();


            string ToString();


        }

    }
}
