namespace PRG_2_ASG
{
    internal class Location
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public Location (string lat,string lon)
        {
            Latitude = lat;
            Longitude = lon;
        }
    }
}