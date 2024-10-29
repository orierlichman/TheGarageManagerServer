using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TheGarageManagerServer.Models;

public partial class GaragePart
{
    [Key]
    [Column("PartID")]
    public int PartId { get; set; }

    [Column("GarageID")]
    public int? GarageId { get; set; }

    [StringLength(30)]
    public string? PartName { get; set; }

    public int? PartNumber { get; set; }

    public int? Cost { get; set; }

    [Column("ImageURL")]
    [StringLength(350)]
    public string? ImageUrl { get; set; }

    [ForeignKey("GarageId")]
    [InverseProperty("GarageParts")]
    public virtual Garage? Garage { get; set; }
}
