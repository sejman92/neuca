// See https://aka.ms/new-console-template for more information

using AirlineBooking;
using AirlineBooking.Domain;
using AirlineBooking.Domain.Flights;
using AirlineBooking.Domain.Tickets;
using AirlineBooking.Persistence.Flights;
using AirlineBooking.Persistence.Tickets;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Hello, World!");

var serviceProvider = new ServiceCollection()
    .AddSingleton<IFlightsRepository, FlightsRepository>()
    .AddSingleton<IFlightManagementService, FlightManagementService>()
    .AddSingleton<ITicketsRepository, TicketsRepository>()
    .AddSingleton<IBookingService, BookingService>()
    .BuildServiceProvider();

var fms = serviceProvider.GetService<IFlightManagementService>();

fms.AddFlight("abc", 123, "XXX", "Poland", "Italy", TimeSpan.FromHours(11), [DayOfWeek.Friday, DayOfWeek.Saturday], 30m);

var f = fms.GetByFlightId(FlightId.Create("abc", 123, "XXX"));

Console.WriteLine($"{f.FlightId.FullId}");

var b = serviceProvider.GetService<IBookingService>();
var customer = new Customer(DateTime.UtcNow.AddYears(-35), "John");

b.BookFlight(f, customer, DateTime.UtcNow, Tenant.A);

