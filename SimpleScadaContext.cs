using System;
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
        public DbSet<Data> Data { get; set; }

        public DbSet<Variables> Variables { get; set; }

        public DbSet<AlarmList> AlarmList { get; set; }
        public DbSet<AlarmHistory> AlarmHistory { get; set; }
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

    public class Variables
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Source { get; set; }
        public string Type { get; set; }
        public string MeasuringUnit { get; set; }
        public string Comment { get; set; }
        public bool Alarm { get; set; }
        public string AlarmGroup { get; set; }
        public string AlarmText { get; set; }
        public double AlarmLimitMin { get; set; }
        public double AlarmLimitMax { get; set; }
    }

    public class AlarmList
    {
        public int Id { get; set; }
        public object Icon { get; set; }
        public string TimeReceived { get; set; }
        public string VariableName { get; set; }
        public double AlarmValue { get; set; }
        public string Text { get; set; }
        public bool Active { get; set; }
    }

    public class AlarmHistory
    {
        public int Id { get; set; }
        public string TimeReceived { get; set; }
        public string TimeAcknowledge { get; set; }
        public string VariableName { get; set; }
        public double AlarmValue { get; set; }
        public string Text { get; set; }
    }
}
