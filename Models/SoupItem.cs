using System;
using System.Collections.Generic;
using System.Text;

namespace SoupAIConversationalAgent.Models
{
    public class SoupItem
    {
        public int Id { get; set; }
        public SoupSize Size { get; set; }
        public List<SoupToppings> Toppings { get; set; } = new();
        public int Quantity { get; set; }
        public string? SpecialInstructions { get; set; }
        public decimal Price { get; set; }
    }
}
