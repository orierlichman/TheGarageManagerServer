namespace TheGarageManagerServer.DTO
{
    public class AppointmentDTO
    {
        public int AppointmentID { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public int? GarageID { get; set; }
        public string? LicensePlate { get; set; }
        public string? AppointmentStatus { get; set; }
        public DateTime? ConfirmDate { get; set; }


        public AppointmentDTO() { } 

        public AppointmentDTO(Models.Appointment modelStudent)
        {
            this.AppointmentID = modelStudent.AppointmentId;
            this.AppointmentDate = modelStudent.AppointmentDate;
            this.GarageID = modelStudent.GarageId;
            this.LicensePlate = modelStudent.LicensePlate;
            this.AppointmentStatus = modelStudent.AppointmentStatus;
            this.ConfirmDate = modelStudent.ConfirmDate;

        }


        public Models.Appointment GetModels()
        {
            Models.Appointment modelsUser = new Models.Appointment()
            {
                AppointmentId = this.AppointmentID,
                AppointmentDate = this.AppointmentDate,
                GarageId = this.GarageID,
                LicensePlate = this.LicensePlate,
                AppointmentStatus = this.AppointmentStatus,
                ConfirmDate = this.ConfirmDate,
            };
            return modelsUser;
        }

    }
}
