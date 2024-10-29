using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TheGarageManagerServer.Models;

[Table("Appointment")]
public partial class Appointment
{
    [Key]
    [Column("AppointmentID")]
    public int AppointmentId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? AppointmentDate { get; set; }

    [Column("GarageID")]
    public int? GarageId { get; set; }

    public int? LicensePlate { get; set; }

    [StringLength(50)]
    public string? AppointmentStatus { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ConfirmDate { get; set; }

    [ForeignKey("GarageId")]
    [InverseProperty("Appointments")]
    public virtual Garage? Garage { get; set; }

    [ForeignKey("LicensePlate")]
    [InverseProperty("Appointments")]
    public virtual Vehicle? LicensePlateNavigation { get; set; }
}
