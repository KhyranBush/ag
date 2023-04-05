using DungeonGame;
using System;
using System.Runtime.CompilerServices;
using static DungeonGame.Interfaces;

namespace DungeonGame

{

    //This is where we used the software design pattern : State.
    //This is also used to open and shut doors if we so chose top.
    public enum OCState { Open, Closed }
    public class Door : IOCState
    {
        public OCState State { get { return _state.State; } }
       
        private Room SideA;
        private Room SideB;
        private IOCState _state;
        private ILockable _lock;
        private string _keyname;
        private bool _Open;
        public string KeyName { set { _keyname = value; } get { return _keyname; } }
        public Door(Room one, Room two)
        {
            SideA = one;
            SideB = two;
            _state = new OpenState();
            _keyname = null;
            _Open = true;
            _lock = null; 
        }
        public Door(Room one, Room two, string keyname)
        {
            SideA = one;
            SideB = two;
            _state = new OpenState();
            _keyname = keyname;
            _Open = true;
            _lock = null;
        }
        public Room getRoom(Room from)
        {
            if(from == SideA)
            {
                return SideB;
            }
            else
            {
                return SideA;
            }
        }
        //locks and unlock the door 
        public void Lock()
        {
            if(_lock != null)
            {
                _lock.Lock();
            }
        }
        public void Unlock()
        {
            if(_lock != null)
            {
                _lock.Unlock(); 
            }

        }
        // makes the lock 


        //Opens and closes doors
        //public static Door UnlockWithKey();
       


        public static Door MakeDoor(Room one, Room two, string oneLabel, string twoLabel) 
        {
            Door door = new Door(one, two);
            one.setExit(oneLabel, door);
            two.setExit(twoLabel, door);
            return door;
        }

        public static Door CreateClosedDoor(Room room1, Room room2, string label1, string label2)
        {
            Door door = new Door(room1, room2);
            door.close();
            room1.setExit(label1, door);
            room2.setExit(label2, door);
            return door;
        }
        public static Door CreateLockedDoor(Room room1, Room room2, string label1, string label2, string keyname)
        {
            Door door = new Door(room1, room2, keyname);
            door.close();
            FloorLock aLock = new FloorLock();
            door.InstallLock(aLock);
            door.Lock();
            room1.setExit(label1, door);
            room2.setExit(label2, door);
            return door;
        }
        public ILockable InstallLock(ILockable theLock)
        {
            ILockable oldLock = _lock;
            _lock = theLock;
            return oldLock;
        }
        public class KeycloseState : IOCState
        {
            bool IsLocked { get; }
            bool IsUnlocked { get; }
            bool CanClose { get; }
            public OCState State { get { return OCState.Open; } }
            public IOCState keyclose()
            {
                return this;
            }
            public IOCState open()
            {
                return new OpenState();
            }
            public IOCState close()
            {
                return this;
            }
        }

        public class OpenState : IOCState
        {
            public OCState State { get { return OCState.Open; } }
            public IOCState open()
            {
                return this;
            }
            public IOCState close()
            {
                return new ClosedState();
            }
            public IOCState keyclose()
            {
                return this;
            }
        }

        public class ClosedState : IOCState
        {
            public OCState State { get { return OCState.Closed; } }
            public IOCState open()
            {
                return new OpenState();
            }
            public IOCState close()
            {
                return this;
            }
            public IOCState keyclose()
            {
                return this;
            }
        }
        public IOCState open()
        {
            _state = _state.open();
            return _state;
        }
        public IOCState close()
        {
            _state = _state.close();
            return _state;
        }
        public IOCState keyclose()
        {
            _state = _state.keyclose();
            return _state;
        }
    }

}