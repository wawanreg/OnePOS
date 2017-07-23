
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OnePOS.Models.Dashboard.Items;
using OnePOS.Models.Dashboard.Vendors;

namespace OnePOS.FunctionController.Json.Converter
{
    public class ItemListJsonConverter : JsonConverter
    {
        private Type Type;
        public ItemListJsonConverter()
        {
            Type = typeof(ListItemViewModels);
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
            ListItemViewModels target = value as ListItemViewModels;
            JObject jObject = new JObject();

            if (target != null)
            {
                
               
                jObject.Add("itemName", JToken.FromObject(target.ItemName));
                jObject.Add("itemBuyPrice", JToken.FromObject(target.ItemBuyPrice));
                jObject.Add("itemSalePrice", JToken.FromObject(target.ItemSalePrice));
                jObject.Add("itemQuantitiy", JToken.FromObject(target.ItemQuantitiy));
                jObject.Add("itemLocation", JToken.FromObject(target.ItemLocation));
                jObject.Add("itemVendorName", JToken.FromObject(target.ItemVendorName));
                jObject.Add("itemId", JToken.FromObject(target.ItemId));
                
            }

            jObject.WriteTo(writer);
        }
    }
}