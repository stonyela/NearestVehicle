namespace VehicleModels;

public class Vehicle
{
    public int VehicleId { get; set; }
    public string VehicleRegistration { get; set; }
    public float Latitude { get; set; }
    public float Longitude { get; set; }
    public float Distance { get; set; }
    public ulong RecordedTimeUtc { get; set; }

    public Vehicle(int vehicleId, string vehicleRegistration, float latitude, float longitude, ulong recordedTimeUtc)
    {
        VehicleId = vehicleId;
        VehicleRegistration = vehicleRegistration;
        Latitude = latitude;
        Longitude = longitude;
        RecordedTimeUtc = recordedTimeUtc;
    }

    public void CalculateDistanceBetweenTwoCoordinates(float lat, float lon)
    {
        Distance = (float)(Math.Pow(Latitude - lat, 2) + Math.Pow(Longitude - lon, 2));
    }
}