namespace TheGarageManagerServer.DTO
{
    public class VehicleDTO
    {
        public string LicensePlate { get; set; }  
        public string? Model { get; set; }
        public string? VehicleYear { get; set; }
        public string? FuelType { get; set; }
        public string? Color { get; set; }
        public string? Manufacturer { get; set; }
        public int? CurrentMileage { get; set; }
        public string ImageURL { get; set; }
        public List<CarRepairDTO>? CarRepairs { get; set; } = new List<CarRepairDTO>();

        public VehicleDTO() { }

        public VehicleDTO(Models.Vehicle modelVehicle)
        {
            this.LicensePlate = modelVehicle.LicensePlate;
            this.Model = modelVehicle.Model;
            this.VehicleYear = modelVehicle.YearVehicle.ToString();
            this.FuelType = modelVehicle.FuelType;
            this.Color = modelVehicle.Color;
            this.Manufacturer = modelVehicle.Manufacturer;
            this.CurrentMileage = modelVehicle.CurrentMileage;
            if (modelVehicle.CarRepairs != null)
            {
                foreach (var carRepair in modelVehicle.CarRepairs)
                {
                    this.CarRepairs.Add(new CarRepairDTO(carRepair));
                }
            }
        }


        public Models.Vehicle GetVehicle()
        {
            Models.Vehicle modelVehicle = new Models.Vehicle()
            {
                LicensePlate = this.LicensePlate,
                Model = this.Model,
                YearVehicle = int.Parse(this.VehicleYear),
                FuelType = this.FuelType,
                Color = this.Color,
                Manufacturer = this.Manufacturer,
                CurrentMileage = this.CurrentMileage
            };
            return modelVehicle;
        }




     }
}
