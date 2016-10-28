using System.Runtime.Serialization;

namespace RAP.Core.DataModels
{
    public class CodeDescription
    {
        public string Code { get; set; }
  
        public string Description { get; set; }
    }

    public class CodeDescriptionByFY : CodeDescription
    {
        public int FY { get; set; }
    }
}
