﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TheGarageManagerServer.Models;

public partial class TheGarageManagerDbContext : DbContext
{
    public User? GetUser(string email)
    {
        return this.Users.Where(u => u.Email == email)
                                        .FirstOrDefault();


    }

}

