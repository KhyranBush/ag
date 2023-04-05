using System;
using System.Collections.Generic;
using System.Text;
using static DungeonGame.Interfaces;

namespace DungeonGame
{
    public class FloorLock : ILockable
    {
        private bool _locked;

        public bool IsLocked { get { return _locked; } }

        public bool IsUnlocked { get { return !_locked; } }

        public bool CanClose { get { return true; } }

        public FloorLock()
        {
            _locked = true;
            
        }

        public void Lock()
        {
            _locked = true;
        }

        public void Unlock()
        {
            _locked = false;
        }
    }
}
