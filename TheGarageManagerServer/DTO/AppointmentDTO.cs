namespace TheGarageManagerServer.DTO
{
    public class AppointmentDTO
    {
        public int AppointmentID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int GarageID { get; set; }
        public int LicensePlate { get; set; }
        public string AppointmentStatus { get; set; }
        public DateTime ConfirmDate { get; set; }
    }
}
