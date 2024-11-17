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
    [StringLength(80)]
    public string LicensePlate { get; set; } = null!;

    [StringLength(80)]
    public string? Model { get; set; }

    public int? YearVehicle { get; set; }

    [StringLength(70)]
    public string? FuelType { get; set; }

    [StringLength(30)]
    public string? Color { get; set; }

    [StringLength(100)]
    public string? Manufacturer { get; set; }

    public int? CurrentMileage { get; set; }

    [InverseProperty("LicensePlateNavigation")]
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    [InverseProperty("LicensePlateNavigation")]
    public virtual ICollection<CarRepair> CarRepairs { get; set; } = new List<CarRepair>();
}
