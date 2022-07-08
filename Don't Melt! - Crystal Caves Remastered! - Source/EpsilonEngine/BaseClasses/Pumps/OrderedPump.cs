﻿//Approved 3/22/2022
namespace EpsilonEngine
{
    public sealed class OrderedPump
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
        private System.Collections.Generic.List<int> _invokeOrder = new System.Collections.Generic.List<int>();
        private PumpEvent[] _pumpEventCache = new PumpEvent[0];
        private bool _pumpEventCacheInvalid;
        private bool _pumpFull;
        #endregion
        #region Public Methods
        public void Invoke()
        {
            if (_pumpFull)
            {
                if (_pumpEventCacheInvalid)
                {
                    _pumpEventCache = _pumpEvents.ToArray();
                    _pumpEventCacheInvalid = true;
                }

                foreach (PumpEvent pumpEvent in _pumpEventCache)
                {
                    pumpEvent.Invoke();
                }
            }
        }
        public void RegisterPumpEvent(PumpEvent pumpEvent, int invokePriority)
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

            for (int i = 0; i < pumpEventsCount; i++)
            {
                if (invokePriority > _invokeOrder[i])
                {
                    _invokeOrder.Insert(i, invokePriority);
                    _pumpEvents.Insert(i, pumpEvent);

                    return;
                }
            }

            _invokeOrder.Add(invokePriority);
            _pumpEvents.Add(pumpEvent);

            _pumpEventCacheInvalid = true;
            _pumpFull = true;
        }
        public bool UnregisterPumpEvent(PumpEvent pumpEvent)
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
                    _invokeOrder.RemoveAt(i);
                    _pumpEvents.RemoveAt(i);

                    _pumpEventCacheInvalid = true;

                    if (pumpEventsCount == 0)
                    {
                        _pumpFull = false;
                    }

                    return true;
                }
            }

            return false;
        }
        #endregion
        #region Internal Methods
        internal void RegisterPumpEventUnsafe(PumpEvent pumpEvent, int invokePriority)
        {
            _pumpEventCacheInvalid = true;
            _pumpFull = true;

            int pumpEventsCount = _pumpEvents.Count;
            for (int i = 0; i < pumpEventsCount; i++)
            {
                if (invokePriority > _invokeOrder[i])
                {
                    _invokeOrder.Insert(i, invokePriority);
                    _pumpEvents.Insert(i, pumpEvent);

                    return;
                }
            }

            _invokeOrder.Add(invokePriority);
            _pumpEvents.Add(pumpEvent);
        }
        internal void UnregisterPumpEventUnsafe(PumpEvent pumpEvent)
        {
            int pumpEventsCount = _pumpEvents.Count;
            for (int i = 0; i < pumpEventsCount; i++)
            {
                if (pumpEvent == _pumpEvents[i])
                {
                    _invokeOrder.RemoveAt(i);
                    _pumpEvents.RemoveAt(i);

                    _pumpEventCacheInvalid = true;

                    if (pumpEventsCount == 0)
                    {
                        _pumpFull = false;
                    }

                    return;
                }
            }
        }
        #endregion
        #region Public Overrides
        public override string ToString()
        {
            return $"EpsilonEngine.OrderedPump({EventCount})";
        }
        #endregion
    }
}