﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Geo.Configuration
{
    public class RabbitMqConfiguration
    {
        public string Hostname { get; set; }
        public string Port { get; set; }
        public string Password { get; set; }
        public string QueueName { get; set; }
        public string UserName { get; set; }
        public string QueueNameResult { get; set; }
    }
}
