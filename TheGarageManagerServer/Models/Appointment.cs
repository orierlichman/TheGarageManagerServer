﻿using System;
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

    [StringLength(80)]
    public string? LicensePlate { get; set; }

    public int? AppointmentStatusId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ConfirmDate { get; set; }

    [ForeignKey("AppointmentStatusId")]
    [InverseProperty("Appointments")]
    public virtual AppointmentStatus? AppointmentStatus { get; set; }

    [ForeignKey("GarageId")]
    [InverseProperty("Appointments")]
    public virtual Garage? Garage { get; set; }

    [ForeignKey("LicensePlate")]
    [InverseProperty("Appointments")]
    public virtual Vehicle? LicensePlateNavigation { get; set; }
}
