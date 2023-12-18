using Domain.Primitives.Results;

namespace Domain.Entities.Errors;

public static partial class DomainErrors
{
    public static class Airport
    {
        public static readonly Error Exists = new(
            "Airport.Exists", 
            "Destination already exists!");

        public static readonly Error NotFound = new(
            "Airport.NotFound", 
            "Destination does not exist!");

        public static readonly Error TerminalExists = new(
            "Terminal.Exists", 
            "Aiport terminal already exists!");
    }

    public static class City
    {
        public static readonly Error Exists = new(
            "City.Exists", 
            "Destination already exists!");
            
        public static readonly Error NotFound = new(
            "City.NotFound", 
            "Destination does not exist!");
    }
}
