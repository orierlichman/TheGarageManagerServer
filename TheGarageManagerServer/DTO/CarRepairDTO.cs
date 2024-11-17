namespace TheGarageManagerServer.DTO
{
    public class CarRepairDTO
    {
        public int RepairID { get; set; }
        public string LicensePlate { get; set; } 
        public int GarageID { get; set; }
        public DateTime RepairDate { get; set; }
        public string DescriptionCar { get; set; }
        public decimal Cost { get; set; }  

        //ca
    }
}
