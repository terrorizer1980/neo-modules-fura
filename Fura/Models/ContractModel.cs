using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Entities;
using Neo.IO.Json;
using Neo.Plugins.Attribute;

namespace Neo.Plugins.Models
{
    [Collection("Contract")]
    public class ContractModel : Entity
    {
        [UInt160AsString]
        [BsonElement("hash")]
        public UInt160 Hash { get; set; }

        [BsonElement("id")]
        public int _ID { get; set; }

        [BsonElement("updatecounter")]
        public ushort UpdateCounter { get; set; }

        [BsonElement("nef")]
        public BsonString Nef { get; set; }

        [BsonElement("manifest")]
        public BsonString Manifest { get; set; }

        [BsonElement("createtime")]
        public ulong CreateTime { get; set; }

        [UInt256AsString]
        [BsonElement("createTxid")]
        public UInt256 CreateTxid { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        public ContractModel(UInt160 hash,string name, int id, ushort updateCounter, JObject nef,JObject manifest, ulong createTime, UInt256 txid)
        {
            Hash = hash;
            Name = name;
            _ID = id;
            UpdateCounter = updateCounter;
            Nef = BsonString.Create(nef.ToString());
            Manifest = BsonString.Create(manifest.ToString());
            CreateTime = createTime;
            CreateTxid = txid;
        }

        public static ContractModel Get(UInt160 hash)
        {
            ContractModel contractModel = DB.Find<ContractModel>().Match(c => c.Hash == hash).ExecuteFirstAsync().Result;
            return contractModel;
        }
    }
}
