using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FuncCore.Buildings;

namespace FuncCore.Persons;

using System.Collections.Generic;

public class LandLord : Resident
{
    
    public double Income { get; set; }


}