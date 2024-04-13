﻿namespace EventManagement.ViewModels
{
    public class MyPurchasedEventViewModel
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public DateTime Date { get; set; }
        public bool IsEventFinished { get; set; }
        public int NumberOfTickets { get; set; }
        public double TotalPrice { get; set; }
        public string ContactInformation { get; set; }
        public string HallName { get; set; }
        public string Venue { get; set; }
        public string Location { get; set; }
        public string Duration { get; set; }

        public int TicketId { get; set; }
    }

}
