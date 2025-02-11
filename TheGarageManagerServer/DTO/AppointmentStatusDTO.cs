using System.ComponentModel.DataAnnotations;

namespace TheGarageManagerServer.DTO
{
    public class AppointmentStatusDTO
    {
        public int StatusId { get; set; }

        public string StatusText { get; set; }

        public AppointmentStatusDTO() { }
        public AppointmentStatusDTO(Models.AppointmentStatus status) 
        {
            this.StatusId = status.StatusId;
            this.StatusText = status.StatusText;
        }

    }
}
