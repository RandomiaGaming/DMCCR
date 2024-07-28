namespace EpsilonEngine
{
    public class PhysicsScene : Scene
    {
        #region Variables
        private System.Collections.Generic.List<PhysicsObject> _physicsObjects =
            new System.Collections.Generic.List<PhysicsObject>();
        private PhysicsObject[] _physicsObjectCache = new PhysicsObject[0];
        private bool _physicsObjectCacheValid = true;
        private System.Collections.Generic.List<PhysicsLayer> _physicsLayers =
            new System.Collections.Generic.List<PhysicsLayer>();
        private PhysicsLayer[] _physicsLayerCache = new PhysicsLayer[0];
        private bool _physicsLayerCacheValid = true;
        #endregion
        #region Constructors
        public PhysicsScene(Game game, ushort width, ushort height, int drawPriority) : base(game, width, height, 0, 0,
            drawPriority)
        {
        }
        #endregion
        #region Overrides
        public override string ToString()
        {
            return $"EpsilonEngine.PhysicsScene()";
        }
        #endregion
        #region Methods
        public PhysicsLayer GetPhysicsLayer(int index)
        {
            if (index < 0 || index >= _physicsLayerCache.Length)
            {
                throw new System.Exception("index was out of range.");
            }
            return _physicsLayerCache[index];
        }
        public System.Collections.Generic.List<PhysicsLayer> GetPhysicsLayers()
        {
            return new System.Collections.Generic.List<PhysicsLayer>(_physicsLayerCache);
        }
        public int GetPhysicsLayerCount()
        {
            return _physicsLayerCache.Length;
        }
        public PhysicsLayer GetPhysicsLayerUnsafe(int index)
        {
            return _physicsLayerCache[index];
        }
        public PhysicsObject GetPhysicsObject(int index)
        {
            if (index < 0 || index >= _physicsObjectCache.Length)
            {
                throw new System.Exception("index was out of range.");
            }
            return _physicsObjectCache[index];
        }
        public PhysicsObject GetPhysicsObject(System.Type type)
        {
            if (type is null)
            {
                throw new System.Exception("type cannot be null.");
            }
            if (!type.IsAssignableFrom(typeof(PhysicsObject)))
            {
                throw new System.Exception("type must be equal to PhysicsObject or be assignable from PhysicsObject.");
            }
            foreach (PhysicsObject physicsObject in _physicsObjectCache)
            {
                if (physicsObject.GetType().IsAssignableFrom(type))
                {
                    return physicsObject;
                }
            }
            return null;
        }
        public T GetPhysicsObject<T>() where T : PhysicsObject
        {
            foreach (PhysicsObject physicsObject in _physicsObjectCache)
            {
                if (physicsObject.GetType().IsAssignableFrom(typeof(T)))
                {
                    return (T)physicsObject;
                }
            }
            return null;
        }
        public System.Collections.Generic.List<PhysicsObject> GetPhysicsObjects()
        {
            return new System.Collections.Generic.List<PhysicsObject>(_physicsObjectCache);
        }
        public System.Collections.Generic.List<PhysicsObject> GetPhysicsObjects(System.Type type)
        {
            if (type is null)
            {
                throw new System.Exception("type cannot be null.");
            }
            if (!type.IsAssignableFrom(typeof(PhysicsObject)))
            {
                throw new System.Exception("type must be equal to PhysicsObject or be assignable from PhysicsObject.");
            }
            System.Collections.Generic.List<PhysicsObject>
                output = new System.Collections.Generic.List<PhysicsObject>();
            foreach (PhysicsObject physicsObject in _physicsObjectCache)
            {
                if (physicsObject.GetType().IsAssignableFrom(type))
                {
                    output.Add(physicsObject);
                }
            }
            return output;
        }
        public System.Collections.Generic.List<T> GetPhysicsObjects<T>() where T : PhysicsObject
        {
            System.Collections.Generic.List<T> output = new System.Collections.Generic.List<T>();
            foreach (PhysicsObject physicsObject in _physicsObjectCache)
            {
                if (physicsObject.GetType().IsAssignableFrom(typeof(T)))
                {
                    output.Add((T)physicsObject);
                }
            }
            return output;
        }
        public int GetPhysicsObjectCount()
        {
            return _physicsObjectCache.Length;
        }
        public PhysicsObject GetPhysicsObjectUnsafe(int index)
        {
            return _physicsObjectCache[index];
        }
        public PhysicsObject GetPhysicsObjectUnsafe(System.Type type)
        {
            foreach (PhysicsObject physicsObject in _physicsObjectCache)
            {
                if (physicsObject.GetType().IsAssignableFrom(type))
                {
                    return physicsObject;
                }
            }
            return null;
        }
        public System.Collections.Generic.List<PhysicsObject> GetPhysicsObjectsUnsafe(System.Type type)
        {
            System.Collections.Generic.List<PhysicsObject>
                output = new System.Collections.Generic.List<PhysicsObject>();
            foreach (PhysicsObject physicsObject in _physicsObjectCache)
            {
                if (physicsObject.GetType().IsAssignableFrom(type))
                {
                    output.Add(physicsObject);
                }
            }
            return output;
        }
        #endregion
        #region Internals
        private void ClearCache()
        {
            if (!_physicsObjectCacheValid)
            {
                _physicsObjectCache = _physicsObjects.ToArray();
                _physicsObjectCacheValid = true;
            }
            if (!_physicsLayerCacheValid)
            {
                _physicsLayerCache = _physicsLayers.ToArray();
                _physicsLayerCacheValid = true;
            }
        }
        internal void RemovePhysicsLayer(PhysicsLayer physicsLayer)
        {
            Game.InitializationPump.RegisterPumpEventUnsafe(ClearCache);
            _physicsLayers.Remove(physicsLayer);
            _physicsLayerCacheValid = false;
        }
        internal void AddPhysicsLayer(PhysicsLayer physicsLayer)
        {
            Game.InitializationPump.RegisterPumpEventUnsafe(ClearCache);
            _physicsLayers.Add(physicsLayer);
            _physicsLayerCacheValid = false;
        }
        internal void RemovePhysicsObject(PhysicsObject physicsObject)
        {
            Game.InitializationPump.RegisterPumpEventUnsafe(ClearCache);
            _physicsObjects.Remove(physicsObject);
            _physicsObjectCacheValid = false;
        }
        internal void AddPhysicsObject(PhysicsObject physicsObject)
        {
            Game.InitializationPump.RegisterPumpEventUnsafe(ClearCache);
            _physicsObjects.Add(physicsObject);
            _physicsObjectCacheValid = false;
        }
        #endregion
    }
}