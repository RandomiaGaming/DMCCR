﻿using System;
using System.Collections.Generic;
namespace EpsilonEngine
{
    public sealed class InverseOrderedPump
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
        private List<int> _invokeOrder = new List<int>();
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
        public void RegisterPumpEvent(PumpEvent pumpEvent, int invokePriority)
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

            RegisterPumpEventUnsafe(pumpEvent, invokePriority);
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
        internal void RegisterPumpEventUnsafe(PumpEvent pumpEvent, int invokePriority)
        {
            int insertPosition = 0;

            int pumpEventsCount = _pumpEvents.Count;
            for (int i = 0; i < pumpEventsCount; i++)
            {
                if (invokePriority >= _invokeOrder[i])
                {
                    insertPosition = i + 1;
                }
            }

            _invokeOrder.Insert(insertPosition, invokePriority);
            _pumpEvents.Insert(insertPosition, pumpEvent);

            _pumpEventCacheValid = false;
            _pumpEmpty = false;
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
                    _invokeOrder.RemoveAt(i);
                    _pumpEvents.RemoveAt(i);
                    return;
                }
            }
        }
        #endregion
        #region Override Methods
        public override string ToString()
        {
            return $"EpsilonEngine.InverseOrderedPump({EventCount})";
        }
        #endregion
    }
}