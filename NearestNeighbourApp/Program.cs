using CalculationsLibrary;
using System.Diagnostics;

var stopWatch = new Stopwatch();
stopWatch.Start();

var vehicles = VehicleFileReader.ReadVehicles("VehiclePositions.dat");

var kDimensionalTree = new KDimensionalTree(vehicles);

// Search for the nearest vehicles
float[,] positions = {
    {34.544909f, -102.100843f},
    {32.345544f, -99.123124f},
    {33.234235f, -100.214124f},
    {35.195739f, -95.348899f},
    {31.895839f, -97.789573f},
    {32.895839f, -101.789573f},
    {34.115839f, -100.225732f},
    {32.335839f, -99.992232f},
    {33.535339f, -94.792232f},
    {32.234235f, -100.222222f}
};

for (var i = 0; i < positions.GetLength(0); i++)
{
    var nearest = kDimensionalTree.Nearest(positions[i, 0], positions[i, 1]);
    Console.WriteLine($"Closest vehicle to position {positions[i, 0]},{positions[i, 1]} is {nearest.VehicleId} with coordinates: {nearest.Latitude}, {nearest.Longitude}.");
}

stopWatch.Stop();
Console.WriteLine($"{stopWatch.Elapsed.TotalSeconds}"); // takes on average 23 seconds

