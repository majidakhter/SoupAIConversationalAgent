using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SoupAIConversationalAgent.Models
{
    public class SoupItem
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("size")]
        public SoupSize Size { get; set; }

        [JsonPropertyName("toppings")]
        public List<SoupToppings> Toppings { get; set; } = new();

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("specialinstructions")]
        public string? SpecialInstructions { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }
    }
}
