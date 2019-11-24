using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TT3.Models
{
    public class Professor
    {
        public string id { get; set; }
        public string name { get; set; }
        public string Department { get; set; }
        public int MaxSuperVision { get; set; }
        public bool AllowedReliver { get; set; }
        public bool AllowedStandBy { get; set; }
    }
    public class Rooms
    {
        public string id { get; set; }
        public string RoomName { get; set; }
    }
    public class Time
    {
        public string T { get; set; }
        public List<string> Rooms { get; set; }
    }

    public class Date
    {
        public string SingleDate { get; set; }
        public List<Time> Time { get; set; }
    }

    public class ExamList
    {
        public int id { get; set; }
        public string ExamName { get; set; }
        public List<Date> Dates { get; set; }
    }

    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string UserGroup { get; set; }
        public string HeaderTemplate { get; set; }
    }
}