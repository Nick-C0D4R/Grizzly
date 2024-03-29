﻿using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    //DTO - Data Transfer Object
    public class RoleDTO : IDTOEntity
    {
        public int Id { get; set; }
        public string RoleName { get; set; }

        public override string ToString()
        {
            return RoleName;
        }
    }
}
