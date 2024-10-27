namespace TheGarageManagerServer.DTO
{
    public class VehiclePartDTO
    {
        public int PartID { get; set; }
        public string PartName { get; set; }
        public string PartNumber { get; set; } 
        public decimal Cost { get; set; }  
        public string Supplier { get; set; }
        public string ImageURL { get; set; }
    }
}
