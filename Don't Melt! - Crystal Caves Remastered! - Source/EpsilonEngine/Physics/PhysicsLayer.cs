using System;
using System.Collections.Generic;
namespace EpsilonEngine
{
    public sealed class PhysicsLayer
    {
        #region Internal Variables
        internal PhysicsObject[] _physicsObjectCache = new PhysicsObject[0];
        #endregion
        #region Private Variables
        private List<PhysicsObject> _physicsObjects = new List<PhysicsObject>();
        private bool _physicsObjectCacheValid = true;
        #endregion
        #region Properties
        public bool IsDestroyed { get; private set; } = false;

        public Game Game { get; private set; } = null;
        public Scene Scene { get; private set; } = null;
        public PhysicsScene PhysicsScene { get; private set; } = null;
        #endregion
        #region Constructors
        public PhysicsLayer(PhysicsScene physicsScene)
        {
            if (physicsScene is null)
            {
                throw new Exception("physicsScene cannot be null.");
            }

            PhysicsScene = physicsScene;
            Scene = physicsScene;
            Game = physicsScene.Game;

            physicsScene.AddPhysicsLayer(this);
        }
        #endregion
        #region Overrides
        public override string ToString()
        {
            return $"EpsilonEngine.PhysicsLayer()";
        }
        #endregion
        #region Methods
        public void Destroy()
        {
            foreach (PhysicsObject physicsObject in _physicsObjectCache)
            {
                physicsObject.Destroy();
            }

            PhysicsScene.RemovePhysicsLayer(this);

            _physicsObjectCache = null;
            _physicsObjects = null;

            PhysicsScene = null;
            Scene = null;
            Game = null;

            IsDestroyed = true;
        }
        public PhysicsObject GetPhysicsObject(int index)
        {
            if (index < 0 || index >= _physicsObjectCache.Length)
            {
                throw new Exception("index was out of range.");
            }

            return _physicsObjectCache[index];
        }
        public PhysicsObject GetPhysicsObject(Type type)
        {
            if (type is null)
            {
                throw new Exception("type cannot be null.");
            }

            if (!type.IsAssignableFrom(typeof(PhysicsObject)))
            {
                throw new Exception("type must be equal to PhysicsObject or be assignable from PhysicsObject.");
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
        public List<PhysicsObject> GetPhysicsObjects()
        {
            return new List<PhysicsObject>(_physicsObjectCache);
        }
        public List<PhysicsObject> GetPhysicsObjects(Type type)
        {
            if (type is null)
            {
                throw new Exception("type cannot be null.");
            }

            if (!type.IsAssignableFrom(typeof(PhysicsObject)))
            {
                throw new Exception("type must be equal to PhysicsObject or be assignable from PhysicsObject.");
            }

            List<PhysicsObject> output = new List<PhysicsObject>();

            foreach (PhysicsObject physicsObject in _physicsObjectCache)
            {
                if (physicsObject.GetType().IsAssignableFrom(type))
                {
                    output.Add(physicsObject);
                }
            }

            return output;
        }
        public List<T> GetPhysicsObjects<T>() where T : PhysicsObject
        {
            List<T> output = new List<T>();

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
        public PhysicsObject GetPhysicsObjectUnsafe(Type type)
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
        public List<PhysicsObject> GetPhysicsObjectsUnsafe(Type type)
        {
            List<PhysicsObject> output = new List<PhysicsObject>();

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
        internal void ClearCache()
        {
            if (!_physicsObjectCacheValid)
            {
                _physicsObjectCache = _physicsObjects.ToArray();
                _physicsObjectCacheValid = true;
            }
        }
        public void RemovePhysicsObject(PhysicsObject physicsObject)
        {
            if (physicsObject is null)
            {
                throw new Exception("physicsObject cannot be null.");
            }
            if (physicsObject.PhysicsScene != PhysicsScene)
            {
                throw new Exception("physicsObject belongs to a different PhysicsScene.");
            }

            _physicsObjectCacheValid = false;

            int physicsObjectsCount = _physicsObjects.Count;
            for (int i = 0; i < physicsObjectsCount; i++)
            {
                if (_physicsObjects[i] == physicsObject)
                {
                    _physicsObjects.RemoveAt(i);
                }
            }

            throw new Exception("physicsObject was not present on this PhysicsLayer.");
        }
        public void AddPhysicsObject(PhysicsObject physicsObject)
        {
            if (physicsObject is null)
            {
                throw new Exception("physicsObject cannot be null.");
            }
            if (physicsObject.PhysicsScene != PhysicsScene)
            {
                throw new Exception("physicsObject belongs to a different PhysicsScene.");
            }

            int physicsObjectsCount = _physicsObjects.Count;
            for (int i = 0; i < physicsObjectsCount; i++)
            {
                if(_physicsObjects[i] == physicsObject)
                {
                    throw new Exception("physicsObject was already added to this PhysicsLayer.");
                }
            }

            _physicsObjects.Add(physicsObject);

            _physicsObjectCacheValid = false;
        }
        #endregion
    }
}