using System;
using System.Collections.Generic;
namespace EpsilonEngine
{
    public sealed class SingleRunPump
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
        private bool _pumpEventsClear = true;
        private bool _pumpEmpty = true;
        #endregion
        #region Public Methods
        public void Invoke()
        {
            if (_pumpEventsClear)
            {
                return;
            }

            int pumpEventsCount = _pumpEvents.Count;
            for (int i = 0; i < pumpEventsCount; i++)
            {
                _pumpEvents[i].Invoke();
            }

            _pumpEvents.Clear();
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
        #endregion
        #region Internal Methods
        public void RegisterPumpEventUnsafe(PumpEvent pumpEvent)
        {
            _pumpEvents.Add(pumpEvent);
            _pumpEventsClear = false;
        }
        #endregion
        #region Override Methods
        public override string ToString()
        {
            return $"EpsilonEngine.SingleRunPump({EventCount})";
        }
        #endregion
    }
}