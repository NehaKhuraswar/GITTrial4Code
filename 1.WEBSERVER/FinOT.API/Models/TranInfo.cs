using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RAP.API.Models
{
    public class TranInfo<T>
    {
        public bool status { get; set; }
        //exceptions that occured during transactions that resulted in rollbacks
        public IList<string> exceptions { get; set; }
        //warnings during transactions (like insert/update/save), but tran is committed 
        public IList<string> warnings { get; set; }
        //model errors (fluent validation errors)
        public Dictionary<string, List<string>> errors;
        public T data { get; set; }
        
        public TranInfo()
        {
            exceptions = new List<string>();
            status = false;
            data = default(T);
        }

        public void AddException(string _message)
        {
            if (this.exceptions == null) { this.exceptions = new List<string>(); }
            this.exceptions.Add(_message);
        }
    }
}