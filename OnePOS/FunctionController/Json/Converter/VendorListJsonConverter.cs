
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OnePOS.Models.Dashboard.Vendors;

namespace OnePOS.FunctionController.Json.Converter
{
    public class VendorListJsonConverter : JsonConverter
    {
        private Type Type;
        public VendorListJsonConverter()
        {
            Type = typeof(ListVendorViewModels);
        }

        public override bool CanRead
        {
            get
            {
                return false;
            }
        }


        public override bool CanConvert(Type objectType)
        {
            return Type.IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            ListVendorViewModels target = value as ListVendorViewModels;
            JObject jObject = new JObject();

            if (target != null)
            {
                
               
                jObject.Add("vendorName", JToken.FromObject(target.VendorName));
                jObject.Add("vendorAddress", JToken.FromObject(target.VendorAddress));    
                
                jObject.Add("vendorEmail", JToken.FromObject(target.VendorEmail));
                jObject.Add("vendorPhone", JToken.FromObject(target.VendorPhone));
              
                jObject.Add("vendorOwner", JToken.FromObject(target.VendorOwner));
                jObject.Add("vendorId", JToken.FromObject(target.VendorId));

            }

            jObject.WriteTo(writer);
        }
    }
}