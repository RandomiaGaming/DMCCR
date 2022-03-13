//Approved 3/1/2022
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
        private System.Collections.Generic.List<PumpEvent> _pumpEvents = new System.Collections.Generic.List<PumpEvent>();
        private bool _pumpEmpty = true;
        #endregion
        #region Public Methods
        public void Invoke()
        {
            if (_pumpEmpty)
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

            _pumpEvents.Add(pumpEvent);
            _pumpEmpty = false;
        }
        #endregion
        #region Internal Methods
        public void RegisterPumpEventUnsafe(PumpEvent pumpEvent)
        {
            _pumpEvents.Add(pumpEvent);
            _pumpEmpty = false;
        }
        #endregion
        #region Public Overrides
        public override string ToString()
        {
            return $"EpsilonEngine.SingleRunPump({EventCount})";
        }
        #endregion
    }
}