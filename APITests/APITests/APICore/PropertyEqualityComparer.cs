using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace APITests.APICore
{
    public class PropertyEqualityComparer<T> : IEqualityComparer<T> where T : class
    {
        public bool Equals(T x, T y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null) || ReferenceEquals(y, null)) return false;

            PropertyInfo[] properties = typeof(T).GetProperties();

            foreach (PropertyInfo property in properties)
            {
                object xValue = property.GetValue(x);
                object yValue = property.GetValue(y);

                if (!object.Equals(xValue, yValue)) return false;
            }

            return true;
        }

        public int GetHashCode(T obj)
        {
            if (ReferenceEquals(obj, null)) return 0;

            PropertyInfo[] properties = typeof(T).GetProperties();
            int hash = 0;

            foreach (PropertyInfo property in properties)
            {
                object propertyValue = property.GetValue(obj);
                if (propertyValue != null)
                    hash ^= propertyValue.GetHashCode();
            }

            return hash;
        }
    }
}
