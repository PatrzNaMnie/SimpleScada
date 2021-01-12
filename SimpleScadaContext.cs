﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleScada
{
    public class SimpleScadaContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
        DbSet<Data> Data { get; set; }
    }

    public class Users
    {
        public int Id { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public int Level { get; set; }
    }

    public class Data
    {
        public int Id { get; set; }
        public string Time { get; set; }
        public string LI1 { get; set; }
    }
}
