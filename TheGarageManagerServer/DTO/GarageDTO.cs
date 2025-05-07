namespace TheGarageManagerServer.DTO
{
    public class GarageDTO
    {
        public int GarageID { get; set; }
        public string? RashamHavarot { get; set; }
        public int? MosahNumber { get; set; }
        public string GarageName { get; set; }
        public int TypeCode { get; set; }
        public string GarageType { get; set; }
        public string GarageAddress { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }  
        public string ZipCode { get; set; }  
        public int? SpecializationCode { get; set; } 
        public string Specialization { get; set; }
        public string ManagerSpecialization { get; set; }
        public int? License { get; set; } 

        
        public GarageDTO() { }

        public GarageDTO(Models.Garage garage)
        {
            this.GarageID = garage.GarageId;
            this.RashamHavarot = garage.RashamHavarot;
            this.MosahNumber = garage.MosahNumber;
            this.GarageName = garage.GarageName;
            this.GarageType = garage.GarageType;
            this.GarageAddress = garage.GarageAddress;
            this.City = garage.City;
            this.Phone = garage.Phone;
            this.ZipCode = garage.ZipCode;
            this.SpecializationCode = garage.SpecializationCode;
            this.Specialization = garage.Specialization;
            this.ManagerSpecialization = garage.ManagerSpecialization;
            this.License = garage.License;
        }



        public Models.Garage GetGarage()
        {
            Models.Garage garage = new Models.Garage()
            {
                GarageId = this.GarageID,
                RashamHavarot = this.RashamHavarot,
                MosahNumber = this.MosahNumber,
                GarageName = this.GarageName,
                GarageType = this.GarageType,
                GarageAddress = this.GarageAddress,
                City = this.City,
                Phone = this.Phone,
                ZipCode = this.ZipCode,
                SpecializationCode = this.SpecializationCode,
                Specialization = this.Specialization,
                ManagerSpecialization = this.ManagerSpecialization,
                License = this.License,
            };
            return garage;
        }


    }
}
