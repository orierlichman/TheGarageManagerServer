using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TheGarageManagerServer.Models;

[Table("Vehicle")]
public partial class Vehicle
{
    [Key]
    public int LicensePlate { get; set; }

    [StringLength(80)]
    [Unicode(false)]
    public string? Model { get; set; }

    public int? YearVehicle { get; set; }

    [StringLength(70)]
    [Unicode(false)]
    public string? FuelType { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string? Color { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Manufacturer { get; set; }

    public int? CurrentMileage { get; set; }

    [InverseProperty("LicensePlateNavigation")]
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    [InverseProperty("LicensePlateNavigation")]
    public virtual ICollection<CarRepair> CarRepairs { get; set; } = new List<CarRepair>();
}
