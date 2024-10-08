﻿namespace EpsilonEngine
{
    [System.AttributeUsage(System.AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
    public sealed class RegisterVirtualInputAttribute : System.Attribute
    {
        private string _name = "Unnamed Virtual Input";
        public string Name
        {
            get { return _name; }
        }
        private RegisterVirtualInputAttribute()
        {
            _name = "Unnamed Virtual Input";
        }
        public RegisterVirtualInputAttribute(string name)
        {
            if (name is null)
            {
                throw new System.Exception("name cannot be null.");
            }
            if (name == "")
            {
                throw new System.Exception("name cannot be empty.");
            }
            _name = name;
        }
        public override string ToString()
        {
            return $"Epsilon.RegisterVirtualInputAttribute({_name})";
        }
    }
}