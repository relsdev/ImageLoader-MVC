using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImageLoader.Models
{
    public class UserAgentEntity : TableEntity
    {
        public UserAgentEntity(string browserId)
        {
            this.PartitionKey = browserId;
            this.RowKey = DateTime.Now.Ticks.ToString();
        }

        public UserAgentEntity() { }

        public string Description { get; set; }        
    }
}