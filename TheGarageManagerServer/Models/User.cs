using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TheGarageManagerServer.Models;

public partial class User
{
    [Key]
    [Column("UserID")]
    public int UserId { get; set; }

    [StringLength(70)]
    public string? Email { get; set; }

    [StringLength(50)]
    public string? UserPassword { get; set; }

    [StringLength(50)]
    public string? UserFirstName { get; set; }

    [StringLength(50)]
    public string? UserLastName { get; set; }

    [Column("UserGarageID")]
    public int UserGarageId { get; set; }

    [Column("UserStatusID")]
    public int? UserStatusId { get; set; }

    [ForeignKey("UserGarageId")]
    [InverseProperty("Users")]
    public virtual Garage UserGarage { get; set; } = null!;

    [ForeignKey("UserStatusId")]
    [InverseProperty("Users")]
    public virtual UserStatus? UserStatus { get; set; }
}
