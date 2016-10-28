using System.Runtime.Serialization;

namespace RAP.Core.DataModels
{
    [DataContract]
    public class IDDescription
    {
        [DataMember]
        public int ID { get; set; }
        
        [DataMember]
        public string Description { get; set; }
    }
}
