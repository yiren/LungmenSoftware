using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Script.Serialization;

namespace LungmenSoftware.Helper
{
    public class EFToJSONConverter:JavaScriptConverter
    {
        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            IDictionary<string, object> result = new Dictionary<string, object>();
            //
            Type type = obj.GetType();
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                bool allowSerialize = IsAllowSerialize(property);
                if (allowSerialize)
                {
                    result[property.Name] = property.GetValue(obj, null);
                }
            }
            //
            return result;
        }
        private bool IsAllowSerialize(PropertyInfo property)
        {
            object[] attrs = property.GetCustomAttributes(true);
            foreach (object attr in attrs)
            {
                if (attr is EdmScalarPropertyAttribute)
                {
                    return true;
                }
            }
            //
            return false;
        }

        public override IEnumerable<Type> SupportedTypes
        {
            get
            {
               yield return typeof(EntityObject);
            }
        }
    }
}