﻿namespace AirlineBooking.Domain.Flights;

public class Flight
{
    public Guid Id { get; private set; }
    public FlightId FlightId { get; private set; }
    public Route Route { get; private set; }
    public FlightSchedule Schedule { get; private set; }

    public Flight(FlightId flightId, Route route, FlightSchedule schedule)
    {
        Id = Guid.NewGuid();
        FlightId = flightId;
        Route = route;
        Schedule = schedule;
    }
}