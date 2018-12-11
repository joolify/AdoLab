using System;

namespace DBContactLibrary
{
    public class Address
    {
        public int ID { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }

        public override string ToString()
        {
            return $"{ID} {Street} {City} {Zip}";
        }
    }
}
