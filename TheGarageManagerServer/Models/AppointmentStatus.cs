using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TheGarageManagerServer.Models;

[Table("AppointmentStatus")]
public partial class AppointmentStatus
{
    [Key]
    public int StatusId { get; set; }

    [StringLength(50)]
    public string StatusText { get; set; } = null!;

    [InverseProperty("AppointmentStatus")]
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
