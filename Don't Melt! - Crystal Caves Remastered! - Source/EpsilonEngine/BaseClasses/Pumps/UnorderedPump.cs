//Approved 3/1/2022
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
        private System.Collections.Generic.List<PumpEvent> _pumpEvents = new System.Collections.Generic.List<PumpEvent>();
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
                throw new System.Exception("pumpEvent cannot be null.");
            }

            int pumpEventsCount = _pumpEvents.Count;
            for (int i = 0; i < pumpEventsCount; i++)
            {
                if (pumpEvent == _pumpEvents[i])
                {
                    throw new System.Exception("pumpEvent has already been added to this pump.");
                }
            }

            _pumpEmpty = false;

            _pumpEventCacheValid = false;

            _pumpEvents.Add(pumpEvent);
        }
        public void UnregisterPumpEvent(PumpEvent pumpEvent)
        {
            if (pumpEvent is null)
            {
                throw new System.Exception("pumpEvent cannot be null.");
            }

            _pumpEventCacheValid = false;

            if (_pumpEvents.Count < 2)
            {
                _pumpEmpty = true;
            }

            bool pumpEventFound = _pumpEvents.Remove(pumpEvent);

            if (pumpEventFound)
            {
                throw new System.Exception("pumpEvent was not found on this pump.");
            }
        }
        #endregion
        #region Internal Methods
        internal void RegisterPumpEventUnsafe(PumpEvent pumpEvent)
        {
            _pumpEmpty = false;

            _pumpEventCacheValid = false;

            _pumpEvents.Add(pumpEvent);
        }
        internal void UnregisterPumpEventUnsafe(PumpEvent pumpEvent)
        {
            _pumpEventCacheValid = false;

            if (_pumpEvents.Count < 2)
            {
                _pumpEmpty = true;
            }

            _pumpEvents.Remove(pumpEvent);
        }
        #endregion
        #region Public Overrides
        public override string ToString()
        {
            return $"EpsilonEngine.UnorderedPump({EventCount})";
        }
        #endregion
    }
}