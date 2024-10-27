namespace TheGarageManagerServer.DTO
{
    public class EmployeeDTO
    {
        public int EmployeeID { get; set; }
        public string NameEmployee { get; set; }
        public string Position { get; set; }
        public int GarageID { get; set; }  
        public string Phone { get; set; }  
        public string Email { get; set; }
        public DateTime HireDate { get; set; }
        public string Specialization { get; set; }  
    }
}
