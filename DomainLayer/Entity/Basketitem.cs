﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entity
{
    public class Basketitem
    {
        public int Id { get; set; }
        public string productName { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }

        //product b5taroka item gwa basket
    }
}
