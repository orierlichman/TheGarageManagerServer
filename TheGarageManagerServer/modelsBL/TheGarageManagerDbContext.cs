using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;

namespace TheGarageManagerServer.Models;

public partial class TheGarageManagerDbContext : DbContext
{
    public User? GetUser(string email)
    {
        return this.Users.Where(u => u.Email == email)
                                        .FirstOrDefault();


    }


    public ObservableCollection<CarRepair> GetRepairs(string licensePlate)
    {
        ObservableCollection<CarRepair> result = new ObservableCollection<CarRepair>();
        foreach (CarRepair v in this.CarRepairs)
        {
            if (v.LicensePlate == licensePlate)
            {
                result.Add(v);
            }
        }
        return result;
    }

    public string GetGarageName(int? garageID)
    {
        return this.Garages.Where(u => u.GarageId == garageID).FirstOrDefault().GarageName;
    }



}



