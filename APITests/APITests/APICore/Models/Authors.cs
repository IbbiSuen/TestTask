﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITests.APICore.Models
{
    public class Authors
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("idBook")]
        public int IdBook { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }
    }
}
