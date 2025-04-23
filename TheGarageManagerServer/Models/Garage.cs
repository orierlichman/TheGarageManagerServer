using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TheGarageManagerServer.Models;

[Table("Garage")]
public partial class Garage
{
    [Key]
    [Column("GarageID")]
    public int GarageId { get; set; }

    public int? MosahNumber { get; set; }

    [StringLength(150)]
    public string? GarageName { get; set; }

    public int? TypeCode { get; set; }

    [StringLength(100)]
    public string? GarageType { get; set; }

    [StringLength(100)]
    public string? GarageAddress { get; set; }

    [StringLength(70)]
    public string? City { get; set; }

    [StringLength(15)]
    public string? Phone { get; set; }

    [StringLength(10)]
    public string? ZipCode { get; set; }

    public int? SpecializationCode { get; set; }

    [StringLength(150)]
    public string? Specialization { get; set; }

    [StringLength(100)]
    public string? ManagerSpecialization { get; set; }

    public int? License { get; set; }

    [InverseProperty("Garage")]
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    [InverseProperty("Garage")]
    public virtual ICollection<AvailableOption> AvailableOptions { get; set; } = new List<AvailableOption>();

    [InverseProperty("Garage")]
    public virtual ICollection<CarRepair> CarRepairs { get; set; } = new List<CarRepair>();

    [InverseProperty("Garage")]
    public virtual ICollection<GaragePart> GarageParts { get; set; } = new List<GaragePart>();

    [InverseProperty("UserGarage")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
