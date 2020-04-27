using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Tactical.DDD;

namespace Auction.Persistence.Services
{
    public class JsonSerializerService
    {

        public IDomainEvent TransformEvent(EventStoreDao eventSelected)
        {
            var o = JsonConvert.DeserializeObject(eventSelected.Data, _jsonSerializerSettings);
            var evt = (IDomainEvent)o;

            return evt;
        }

        public readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.All,
            NullValueHandling = NullValueHandling.Ignore
        };
    }
}
