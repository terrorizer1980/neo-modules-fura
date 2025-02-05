using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Entities;
using Neo.Plugins.Attribute;

namespace Neo.Plugins.Models
{
    [Collection("Execution")]
    public class ExecutionModel : Entity
    {
        [UInt256AsString]
        [BsonElement("txid")]
        public UInt256 Txid { get; set; }

        [UInt256AsString]
        [BsonElement("blockhash")]
        public UInt256 BlockHash { get; set; }

        [BsonElement("trigger")]
        public string Trigger { get; set; }

        [BsonElement("vmstate")]
        public string VmState { get; set; }

        [BsonElement("exception")]
        public string Exception { get; set; }

        [BsonElement("gasconsumed")]
        public long GasConsumed { get; set; }

        [BsonElement("timestamp")]
        public ulong Timestamp { get; set; }

        public ExecutionModel(UInt256 txid, UInt256 blockHash, ulong timestamp, string trigger, string vmstate, string exception, long gasconsumed)
        {
            Txid = txid;
            BlockHash = blockHash;
            Trigger = trigger;
            VmState = vmstate;
            Exception = exception;
            GasConsumed = gasconsumed;
            Timestamp = timestamp;
        }

        public ExecutionModel(ExecutionModel executionModel)
        {
            Txid = executionModel.Txid;
            BlockHash = executionModel.BlockHash;
            Trigger = executionModel.Trigger;
            VmState = executionModel.VmState;
            Exception = executionModel.Exception;
            GasConsumed = executionModel.GasConsumed;
        }

        public static ExecutionModel Get(UInt256 txid,UInt256 blockHash, string trigger)
        {
            ExecutionModel executionModel = DB.Find<ExecutionModel>().Match( e => e.Txid == txid && e.BlockHash == blockHash && e.Trigger == trigger ).ExecuteFirstAsync().Result;
            return executionModel is null ? null : new ExecutionModel(executionModel);
        }

        /// <summary>
        /// 查询某个高度是否已经存入execution
        /// </summary>
        /// <param name="blockHash"></param>
        /// <returns>true代表存在，false代表不存在</returns>
        public static bool ExistByBlockHash(UInt256 blockHash)
        {
            ExecutionModel executionModel = DB.Find<ExecutionModel>().Match( e => e.BlockHash == blockHash).ExecuteFirstAsync().Result;
            return executionModel is not null;
        }
    }
}
