﻿using BongOliver.DTOs.Service;
using BongOliver.DTOs.User;
using BongOliver.Models;
using System.ComponentModel.DataAnnotations;

namespace BongOliver.DTOs.Booking
{
    public class BookingDTO
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public int Status { get; set; }
        public DateTime Time { get; set; }
        public int CustomerId { get; set; }
        public UserDTO Customer { get; set; }
        public int StylistId { get; set; }
        public UserDTO Stylist { get; set; }
        public ICollection<ServiceDTO> Services { get; set; }
        public BookingDTO(Models.Booking booking)
        {
            Id = booking.Id;
            Comment = booking.Comment;
            Status = booking.Status;
            Time = booking.Time;
            CustomerId = booking.CustomerId;
            Customer = new UserDTO(booking.Customer);
            StylistId = booking.StylistId;
            Stylist = new UserDTO(booking.Stylist);
            Services = booking.Services.Select(s => new ServiceDTO(s)).ToList();
        }
    }
}