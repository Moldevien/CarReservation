namespace Domain.Entities
{
    public class CarFilter
    {
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string? Gas { get; set; }
        public int? Passengers { get; set; }
        public double? Volume { get; set; }
        public string? Gear_box { get; set; }
    }
}
