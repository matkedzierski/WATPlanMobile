﻿using System;
using System.Runtime.Serialization;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace WATPlanMobile.Models
{
    [Table("events")]
    [DataContract]
    [Serializable]
    public class EventModel
    {
        [DataMember][PrimaryKey] public string ID { get; set; } // Google ID

        [DataMember] public string Name { get; set; } // Nazwa przedmiotu

        [DataMember] public string Type { get; set; } // Wykład, Ćwiczenia itd..
        
        [DataMember] public string Number { get; set; } // Numer zajęć

        [DataMember] public string Lecturer { get; set; } // Wykładowca

        [DataMember] public string Room { get; set; } // Sala

        [DataMember] public string Groups { get; set; } // Grupy

        [DataMember] public string Info { get; set; } // Info dla studentów

        [DataMember] public string Color { get; set; } // hex

        [DataMember] public int Week { get; set; } // offset od 1.10

        [DataMember] public int DayOfWeek { get; set; } // 1-7

        [DataMember] public int BlockNumber { get; set; } // 1-7

        [DataMember] public int BlockSpan { get; set; } // 1-7

        [ForeignKey(typeof(WeekModel))] 
        public WeekModel WeekModel { get; set; }

        public override string ToString()
        {
            return "> Event: \n" +
                   $"\tID          {ID}\n" +
                   $"\tName        {Name}\n" +
                   $"\tType        {Type}\n" +
                   $"\tNumber      {Number}\n" +
                   $"\tLecturer    {Lecturer}\n" +
                   $"\tRoom        {Room}\n" +
                   $"\tGroups      {Groups}\n" +
                   $"\tInfo        {Info}\n" +
                   $"\tColor       {Color}\n" +
                   $"\tWeek        {Week}\n" +
                   $"\tDayOfWeek   {DayOfWeek}\n" +
                   $"\tBlockNumber {BlockNumber}\n" +
                   $"\tBlockSpan   {BlockSpan}\n"
                ;
        }
    }
}