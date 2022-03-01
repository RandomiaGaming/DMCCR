using System;
using System.Collections.Generic;
namespace EpsilonEngine
{
    public sealed class UnorderedPump
    {
        #region Public Variables
        public int EventCount
        {
            get
            {
                return _pumpEvents.Count;
            }
        }
        #endregion
        #region Private Variables
        private List<PumpEvent> _pumpEvents = new List<PumpEvent>();
        private PumpEvent[] _pumpEventCache = new PumpEvent[0];
        private bool _pumpEventCacheValid = true;
        private bool _pumpEmpty = true;
        #endregion
        #region Public Methods
        public void Invoke()
        {
            if (_pumpEmpty)
            {
                return;
            }

            if (!_pumpEventCacheValid)
            {
                _pumpEventCache = _pumpEvents.ToArray();
                _pumpEventCacheValid = true;
            }

            foreach (PumpEvent pumpEvent in _pumpEventCache)
            {
                pumpEvent.Invoke();
            }
        }
        public void RegisterPumpEvent(PumpEvent pumpEvent)
        {
            if (pumpEvent is null)
            {
                throw new Exception("pumpEvent cannot be null.");
            }

            int pumpEventsCount = _pumpEvents.Count;
            for (int i = 0; i < pumpEventsCount; i++)
            {
                if (pumpEvent == _pumpEvents[i])
                {
                    throw new Exception("pumpEvent has already been added to this pump.");
                }
            }

            RegisterPumpEventUnsafe(pumpEvent);
        }
        public void UnregisterPumpEvent(PumpEvent pumpEvent)
        {
            if (pumpEvent is null)
            {
                throw new Exception("pumpEvent cannot be null.");
            }

            bool pumpEventFound = false;

            int pumpEventsCount = _pumpEvents.Count;
            for (int i = 0; i < pumpEventsCount; i++)
            {
                if (pumpEvent == _pumpEvents[i])
                {
                    pumpEventFound = true;
                    break;
                }
            }

            if (pumpEventFound)
            {
                throw new Exception("pumpEvent was not found on this pump.");
            }

            UnregisterPumpEventUnsafe(pumpEvent);
        }
        #endregion
        #region Internal Methods
        internal void RegisterPumpEventUnsafe(PumpEvent pumpEvent)
        {
            _pumpEmpty = false;
            _pumpEvents.Add(pumpEvent);
            _pumpEventCacheValid = false;
        }
        internal void UnregisterPumpEventUnsafe(PumpEvent pumpEvent)
        {
            _pumpEventCacheValid = false;
            int pumpEventsCount = _pumpEvents.Count;
            if (pumpEventsCount < 2)
            {
                _pumpEmpty = true;
            }
            for (int i = 0; i < pumpEventsCount; i++)
            {
                if (pumpEvent == _pumpEvents[i])
                {
                    _pumpEvents.RemoveAt(i);
                    return;
                }
            }
        }
        #endregion
        #region Override Methods
        public override string ToString()
        {
            return $"EpsilonEngine.UnorderedPump({EventCount})";
        }
        #endregion
    }
}