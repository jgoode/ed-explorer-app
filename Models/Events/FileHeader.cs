using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ED_Explorer.Models.Events {
   internal class EventBase {
      public string TimeStamp { get; set; }
      public string Event { get; set; }
   }

   class FileHeader : EventBase {
      public int Part { get; set; }
      public string Language { get; set; }
      public string GameVersion { get; set; }
      public string Build { get; set; }
   }

   class LoadGame : EventBase {
      public string Commander { get; set; }
      public string Ship { get; set; }
      public int ShipID { get; set; }
      public string GameMode { get; set; }
      public int Credits { get; set; }
      public int Loan { get; set; }
   }

   class ProgressRank : EventBase {
      public int Combat { get; set; }
      public int Trade { get; set; }
      public int Explore { get; set; }
      public int Empire { get; set; }
      public int Federation { get; set; }
      public int CQC { get; set; }
   }

   class Location : EventBase {
      public bool Docked { get; set; }
      public string StarSystem { get; set; }
      public List<double> StarPos { get; set; }
      public string Allegiance { get; set; }
      public string Economy { get; set; }
      public string Economy_Localised { get; set; }
      public string Government { get; set; }
      public string Government_Localised { get; set; }
      public string Security { get; set; }
      public string Security_Localised { get; set; }
      public string Body { get; set; }
      public string BodyType { get; set; }
   }

   class FSDJump : EventBase {
      public string StarSystem { get; set; }
      public List<double> StarPos { get; set; }
      public string Allegiance { get; set; }
      public string Economy { get; set; }
      public string Economy_Localised { get; set; }
      public string Government { get; set; }
      public string Government_Localised { get; set; }
      public string Security { get; set; }
      public string Security_Localised { get; set; }
      public double JumpDist { get; set; }
      public double FuelUsed { get; set; }
      public double FuelLevel { get; set; }
   }

   class Scan : EventBase {
      public string BodyName { get; set; }
      public double DistanceFromArrivalLS { get; set; }
      public bool TidalLock { get; set; }
      public string TerraformState { get; set; }
      public string PlanetClass { get; set; }
      public string Atmosphere { get; set; }
      public string Volcanism { get; set; }
      public double MassEM { get; set; }
      public double Radius { get; set; }
      public double SurfaceGravity { get; set; }
      public double SurfaceTemperature { get; set; }
      public double SurfacePressure { get; set; }
      public bool Landable { get; set; }
      public double SemiMajorAxis { get; set; }
      public double Eccentricity { get; set; }
      public double OrbitalInclination { get; set; }
      public double Periapsis { get; set; }
      public double OrbitalPeriod { get; set; }
      public double RotationPeriod { get; set; }
   }

   class FuelScoop : EventBase {
      public double Scooped { get; set; }
      public double Total { get; set; }
   }

   class Docked: EventBase {
      public string StationName { get; set; }
      public string StarSystem { get; set; }
      public string StationFaction { get; set; }
      public string FactionState { get; set; }
      public string StationGovernment { get; set; }
      public string StationGovernment_Localised { get; set; }
      public string StationEconomy { get; set; }
      public string StationEconomy_Localised { get; set; }
   }

   class Undocked : EventBase {
      public string StationName { get; set; }
   }

   class ReceiveText : EventBase {
      public string From { get; set; }
      public string Message { get; set; }
      public string Message_Localised { get; set; }
      public string Channel { get; set; }
   }

   class Screenshot: EventBase {
      public string Filename { get; set; }
      public int Width { get; set; }
      public int Height { get; set; }
      public string System { get; set; }
      public string Body { get; set; }
   }

   class SupercruiseExit: EventBase {
      public string StarSystem { get; set; }
      public string Body { get; set; }
      public string BodyType { get; set; }
   }

   class DockingDenied: EventBase {
      public string Reason { get; set; }
      public string StationName { get; set; }
   }

   class DockingRequested: EventBase {
      public string StationName { get; set; }
   }

   class DockingGranted: EventBase {
      public int LandingPad { get; set; }
      public string StationName { get; set; }
   }

   class SellExplorationData : EventBase {
      public List<string> Systems { get; set; }
      public List<string> Discovered { get; set; }
      public int BaseValue { get; set; }
      public int Bonus { get; set; }
   }
}
