namespace TheGarageManagerServer.DTO
{

    public class CarRepairDTO
    {
        public int RepairID { get; set; }
        public string LicensePlate { get; set; }
        public int? GarageID { get; set; }
        public DateTime? RepairDate { get; set; }
        public string DescriptionCar { get; set; }
        public int? Cost { get; set; }
        public string? GarageName { get; set; }


        public CarRepairDTO() { }

        public CarRepairDTO(Models.CarRepair modelRepair)
        {
            this.RepairID = modelRepair.RepairId;
            this.LicensePlate = modelRepair.LicensePlate;
            this.GarageID = modelRepair.GarageId;
            this.RepairDate = modelRepair.RepairDate;
            this.DescriptionCar = modelRepair.DescriptionCar;
            this.Cost = modelRepair.Cost;
        }

        public Models.CarRepair GetModels()
        {
            Models.CarRepair modelsRepair = new Models.CarRepair()
            {
                RepairId = this.RepairID,
                LicensePlate = this.LicensePlate,
                GarageId = this.GarageID,
                RepairDate = this.RepairDate,
                DescriptionCar = this.DescriptionCar,
                Cost = this.Cost
            };
            return modelsRepair;
        }
    }
}
