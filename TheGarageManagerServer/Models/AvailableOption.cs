using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TheGarageManagerServer.Models;

public partial class AvailableOption
{
    [Key]
    [Column("OptionID")]
    public int OptionId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? OptionDate { get; set; }

    [Column("GarageID")]
    public int? GarageId { get; set; }

    [ForeignKey("GarageId")]
    [InverseProperty("AvailableOptions")]
    public virtual Garage? Garage { get; set; }
}
