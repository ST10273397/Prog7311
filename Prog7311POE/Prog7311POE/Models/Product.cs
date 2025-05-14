namespace Prog7311POE.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public DateTime productionDate {get; set; }

        public Product()
        { }

        public Product(int id, string name, string category, DateTime productionDate, int farmerId)
        {
            Id = id;
            Name = name;
            Category = category;
            this.productionDate = productionDate;
            FarmerId = farmerId;
        }

        public int FarmerId { get; set; }
        public Farmer Farmer { get; set; }
    }
}
