namespace TheGarageManagerServer.DTO
{
    public class GarageDTO
    {
        public int GarageID { get; set; }
        public int MosahNumber { get; set; }
        public string GarageName { get; set; }
        public int TypeCode { get; set; }
        public string GarageType { get; set; }
        public string GarageAddress { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }  
        public string ZipCode { get; set; }  
        public int SpecializationCode { get; set; } 
        public string Specialization { get; set; }
        public string ManagerSpecialization { get; set; }
        public string License { get; set; } 

    }
}
