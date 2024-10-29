using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TheGarageManagerServer.Models;

[Table("CarRepair")]
public partial class CarRepair
{
    [Key]
    [Column("RepairID")]
    public int RepairId { get; set; }

    public int? LicensePlate { get; set; }

    [Column("GarageID")]
    public int? GarageId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? RepairDate { get; set; }

    [Column(TypeName = "text")]
    public string? DescriptionCar { get; set; }

    public int? Cost { get; set; }

    [ForeignKey("GarageId")]
    [InverseProperty("CarRepairs")]
    public virtual Garage? Garage { get; set; }

    [ForeignKey("LicensePlate")]
    [InverseProperty("CarRepairs")]
    public virtual Vehicle? LicensePlateNavigation { get; set; }
}
