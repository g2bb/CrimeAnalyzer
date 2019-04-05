using System;

namespace CrimeAnalyzer {
    public class CrimeStats {
        public int Year { get; set; }
        public int Population { get; set; }
        public int ViolentCrime { get; set; }
        public int Murder { get; set; }
        public int Rape { get; set; }
        public int Robbery { get; set; }
        public int AggravatedAssault { get; set; }
        public int PropertyCrime { get; set; }
        public int Burglary { get; set; }
        public int Theft { get; set; }
        public int MotorVehicleTheft { get; set; }

        public CrimeStats() {

        }

        public CrimeStats(int year, int population, int violent_crime, int murder, int rape, int robbery, int aggravated_assault, int property_crime, int burglary, int theft, int motor_vehicle_theft) {
            Year = year;
            Population = population;
            ViolentCrime = violent_crime;
            Murder = murder;
            Rape = rape;
            Robbery = robbery;
            AggravatedAssault = aggravated_assault;
            PropertyCrime = property_crime;
            Burglary = burglary;
            Theft = theft;
            MotorVehicleTheft = motor_vehicle_theft;
        }
    }
}
