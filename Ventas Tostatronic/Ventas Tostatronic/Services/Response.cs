﻿using System;
namespace Ventas_Tostatronic.Services
{
	public class Response
	{
        public bool succes { get; set; }
        public int statusCode { get; set; }
        public string message { get; set; }
        public object data { get; set; }
    }
}

