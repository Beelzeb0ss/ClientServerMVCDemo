﻿namespace ClientServerMVCDemo.Data.Models
{
    public class ClientProperty
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }

        public int ClientId { get;set; }
        public Client Client { get; set; }
    }
}
