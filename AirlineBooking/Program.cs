// See https://aka.ms/new-console-template for more information

using AirlineBooking;
using AirlineBooking.Domain.Flights;
using AirlineBooking.Persistence.Flight;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Hello, World!");

var serviceProvider = new ServiceCollection()
    .AddSingleton<IFlightsRepository, FlightsRepository>()
    .AddSingleton<IFlightManagementService, FlightManagementService>()
    .BuildServiceProvider();

var s = serviceProvider.GetService<IFlightManagementService>();

s.AddFlight("abc", 123, "XXX", "Poland", "Italy", TimeSpan.FromHours(11), [DayOfWeek.Friday, DayOfWeek.Saturday]);

var f = s.GetByFlightId(FlightId.Create("abc", 123, "XXX"));

Console.WriteLine($"{f.FlightId.FullId}");