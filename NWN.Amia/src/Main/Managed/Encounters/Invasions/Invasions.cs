using System;
using JetBrains.Annotations;
using NWN.Amia.Main.Core.Types;
using NWN.Core;
using NWN.Core.NWNX;

namespace NWN.Amia.Main.Managed.Encounters.Invasions
{
    
    public class TriggerSpawnCreatures 
    {
        
        public void InvasionGeneric(uint area, int size, int random, string creaturetype1, string creaturetype2, string creaturetype3, string creaturetype4, string creaturetype5, string lieutentant, string boss, string message)
        {
            string areaName = NWScript.GetName(area);
            int totalPLC = 150; 
            Random rnd = new Random();
            int totalMobs = Convert.ToInt32(rnd.Next(random) + size);
            int totalLieutentants = Convert.ToInt32(rnd.Next(random/2) + size*0.75);

            SpawnPLC(area, totalPLC);

            NWScript.DelayCommand(15.0f,() => SpawnMobs(area, totalMobs,creaturetype1,creaturetype2,creaturetype3,creaturetype4,creaturetype5));
            NWScript.DelayCommand(30.0f,() => SpawnLieutentants(area, totalLieutentants, lieutentant));
            NWScript.DelayCommand(45.0f,() => SpawnBoss(area, boss));
            NWScript.DelayCommand(60.0f,() => MassMessage(area, message));
        }

        public void InvasionBeasts(uint area, int size, int random)
        {
            string areaName = NWScript.GetName(area);
            string creaturetype1 = "beasthero";
            string creaturetype2 = "elitebeastarcher";
            string creaturetype3 = "beastshaman";
            string creaturetype4 = "beastmanchampion";
            string creaturetype5 = "beastmonk";
            string lieutentant = "beastguard";
            string boss = "invasionbeastbs";
            string message = "News quickly spreads of an amassing army of Beastmen in " + areaName + ". They must be stopped before it is too late!";

            InvasionGeneric(area, size, random, creaturetype1, creaturetype2, creaturetype3, creaturetype4, creaturetype5, lieutentant, boss, message);

        }

        
        public void InvasionGoblins(uint area, int size, int random)
        {
            string areaName = NWScript.GetName(area);
            string creaturetype1 = "ds_yellowfang_5";
            string creaturetype2 = "ds_yellowfang_1";
            string creaturetype3 = "ds_yellowfang_2";
            string creaturetype4 = "ds_yellowfang_1";
            string creaturetype5 = "ds_yellowfang_2";
            string lieutentant = "ds_yellowfang_4";
            string boss = "ds_yellowfang_6";
            string message = "News quickly spreads of an amassing army of Goblins in " + areaName + ". They must be stopped before it is too late!";

            InvasionGeneric(area, size, random, creaturetype1, creaturetype2, creaturetype3, creaturetype4, creaturetype5, lieutentant, boss, message);
        }

        
        public void InvasionTrolls(uint area, int size, int random)
        {
            string areaName = NWScript.GetName(area);
            string creaturetype1 = "mountainguard";
            string creaturetype2 = "mountainguard";
            string creaturetype3 = "mounttroll";
            string creaturetype4 = "mounttroll";
            string creaturetype5 = "mounttroll";
            string lieutentant = "bigmounttroll";
            string boss = "invasiontrollbs";
            string message = "News quickly spreads of an amassing army of Trolls in " + areaName + ". They must be stopped before it is too late!";

            InvasionGeneric(area, size, random, creaturetype1, creaturetype2, creaturetype3, creaturetype4, creaturetype5, lieutentant, boss, message);
        }

        
        public void InvasionOrcs(uint area, int size, int random)
        {
            string areaName = NWScript.GetName(area);
            string creaturetype1 = "af_ds_ork";
            string creaturetype2 = "arelithorc001";
            string creaturetype3 = "arelithorc";
            string creaturetype4 = "arelithorc";
            string creaturetype5 = "orcbasher";
            string lieutentant = "orcboss001";
            string boss = "chosenofkilma002";
            string message = "News quickly spreads of an amassing army of Orcs in " + areaName + ". They must be stopped before it is too late!";

            InvasionGeneric(area, size, random, creaturetype1, creaturetype2, creaturetype3, creaturetype4, creaturetype5, lieutentant, boss, message);
        }

        public IntPtr GenerateRandomLocation(uint area)
        {
            var zPosition = 0.0f; 
            var facing = 0.0f;
            int widthInTiles;
            int heightInTiles;
            int widthInMeters;
            int heightInMeters;


            // Determining the width and height of the area in tiles
            widthInTiles = NWScript.GetAreaSize(NWScript.AREA_WIDTH, area);
            heightInTiles = NWScript.GetAreaSize(NWScript.AREA_HEIGHT, area);
            widthInMeters = widthInTiles*10; 
            heightInMeters = heightInTiles*10; 

            //Generate a random position in the area
            var xPosition = Convert.ToSingle(NWScript.Random(widthInMeters*10) / 10.0);
            var yPosition = Convert.ToSingle(NWScript.Random(heightInMeters*10) / 10.0); 
            var randomPositon = NWScript.Vector(xPosition,yPosition,zPosition);
            var randomLocation = NWScript.Location(area, randomPositon, facing);
            return randomLocation;

            

        }

        private void SpawnPLC(uint area, int totalPLC)
        { 
            int countPLC = 0; 
            while(countPLC < totalPLC)
            {            
              var randomLocation = GenerateRandomLocation(area);
              var objectPLC = CreatePLC(randomLocation);

              if(NWScript.GetIsObjectValid(objectPLC) == NWScript.TRUE)
              {
                  countPLC++;
              }
            }

        }

        private uint CreatePLC(IntPtr objectLocation)
        {
            Random rnd = new Random();
            var random = rnd.Next(11);
            uint PLC = 0; 

            switch (random)
            {
                case 0: PLC = NWScript.CreateObject(NWScript.OBJECT_TYPE_PLACEABLE,"invasioncorpse1",objectLocation); break;
                case 1: PLC = NWScript.CreateObject(NWScript.OBJECT_TYPE_PLACEABLE,"invasionmisc3",objectLocation); break;
                case 2: PLC = NWScript.CreateObject(NWScript.OBJECT_TYPE_PLACEABLE,"invasioncorpse3",objectLocation); break;
                case 3: PLC = NWScript.CreateObject(NWScript.OBJECT_TYPE_PLACEABLE,"invasioncorpse5",objectLocation); break;
                case 4: PLC = NWScript.CreateObject(NWScript.OBJECT_TYPE_PLACEABLE,"invasionmisc5",objectLocation); break;
                case 5: PLC = NWScript.CreateObject(NWScript.OBJECT_TYPE_PLACEABLE,"invasiondebris1",objectLocation); break;
                case 6: PLC = NWScript.CreateObject(NWScript.OBJECT_TYPE_PLACEABLE,"invasioncamp",objectLocation); break;
                case 7: PLC = NWScript.CreateObject(NWScript.OBJECT_TYPE_PLACEABLE,"invasiondebris1",objectLocation); break;
                case 8: PLC = NWScript.CreateObject(NWScript.OBJECT_TYPE_PLACEABLE,"invasioncamp",objectLocation); break;
                case 9: PLC = NWScript.CreateObject(NWScript.OBJECT_TYPE_PLACEABLE,"invasionmisc3",objectLocation); break;
                case 10: PLC = NWScript.CreateObject(NWScript.OBJECT_TYPE_PLACEABLE,"invasionmisc3",objectLocation); break;
            }

            return PLC;
        }

        private void SpawnMobs(uint area, int totalMobs, string creaturetype1, string creaturetype2, string creaturetype3, string creaturetype4, string creaturetype5)
        {
            int countMobs = 0;              
            var zPosition = 0.0f; 
            var facing = 0.0f;
            int widthInTiles;
            int heightInTiles;
            int widthInMeters;
            int heightInMeters;

            while(countMobs < totalMobs)
            {
                

              
              // Determining the width and height of the area in tiles
              widthInTiles =  NWScript.GetAreaSize(NWScript.AREA_WIDTH, area);             
              heightInTiles = NWScript.GetAreaSize(NWScript.AREA_HEIGHT, area);
              widthInMeters =  widthInTiles*10;                
              heightInMeters =  heightInTiles*10; 

              //Generate a random position in the area
              var xPosition =  Convert.ToSingle(NWScript.Random(widthInMeters*10) / 10.0);                 
              var yPosition =  Convert.ToSingle(NWScript.Random(heightInMeters*10) / 10.0);                 
              var randomPositon =  NWScript.Vector(xPosition,yPosition,zPosition);                 
              var randomLocation =  NWScript.Location(area, randomPositon, facing);

              

              var creature1 = NWScript.CreateObject(NWScript.OBJECT_TYPE_CREATURE,creaturetype1,randomLocation);

              if(NWScript.GetIsObjectValid(creature1) == NWScript.TRUE)
              {       
                  var randomPosN = NWScript.Vector(xPosition,yPosition + 1.0f,zPosition);
                  var randomPosS = NWScript.Vector(xPosition,yPosition - 1.0f,zPosition);
                  var randomPosE = NWScript.Vector(xPosition + 1.0f,yPosition,zPosition);
                  var randomPosW = NWScript.Vector(xPosition - 1.0f,yPosition,zPosition);
                  var randomPosNE = NWScript.Vector(xPosition + 1.0f,yPosition + 1.0f,zPosition);
                  var randomPosNW = NWScript.Vector(xPosition - 1.0f,yPosition + 1.0f,zPosition);
                  var randomPosSE = NWScript.Vector(xPosition + 1.0f,yPosition - 1.0f,zPosition);
                  var randomPosSW = NWScript.Vector(xPosition - 1.0f,yPosition - 1.0f,zPosition);

                  var creature2 = NWScript.CreateObject(NWScript.OBJECT_TYPE_CREATURE,creaturetype2,NWScript.Location(area, randomPosN, facing));
                  var creature3 = NWScript.CreateObject(NWScript.OBJECT_TYPE_CREATURE,creaturetype2,NWScript.Location(area, randomPosS, facing));
                  var creature4 = NWScript.CreateObject(NWScript.OBJECT_TYPE_CREATURE,creaturetype3,NWScript.Location(area, randomPosE, facing));
                  var creature5 = NWScript.CreateObject(NWScript.OBJECT_TYPE_CREATURE,creaturetype3,NWScript.Location(area, randomPosW, facing));
                  var creature6 = NWScript.CreateObject(NWScript.OBJECT_TYPE_CREATURE,creaturetype4,NWScript.Location(area, randomPosNE, facing));
                  var creature7 = NWScript.CreateObject(NWScript.OBJECT_TYPE_CREATURE,creaturetype4,NWScript.Location(area, randomPosNW, facing));
                  var creature8 = NWScript.CreateObject(NWScript.OBJECT_TYPE_CREATURE,creaturetype5,NWScript.Location(area, randomPosSE, facing));
                  var creature9 = NWScript.CreateObject(NWScript.OBJECT_TYPE_CREATURE,creaturetype5,NWScript.Location(area, randomPosSW, facing));
                  countMobs++;
              }

            }


        }

        private void SpawnLieutentants(uint area, int totalLieutentants, string lieutentant)
        {
            int countLieutentant = 0; 
            IntPtr randomLocation;
            uint objectCreature;

            while(countLieutentant < totalLieutentants)
            {
              randomLocation = GenerateRandomLocation(area);
              objectCreature = NWScript.CreateObject(NWScript.OBJECT_TYPE_CREATURE,lieutentant,randomLocation);

              if(NWScript.GetIsObjectValid(objectCreature) == NWScript.TRUE)
              {
                  countLieutentant++;
              }

            }

        }

        private void SpawnBoss(uint area, string boss)
        {
            int countBoss = 0;
            IntPtr randomLocation;
            uint objectCreature;

            while(countBoss < 1)
            {
              randomLocation = GenerateRandomLocation(area);
              objectCreature = NWScript.CreateObject(NWScript.OBJECT_TYPE_CREATURE,boss,randomLocation);

              if(NWScript.GetIsObjectValid(objectCreature) == NWScript.TRUE)
              {
                  countBoss++;
              }      
 
            }

        }

        void MassMessage(uint area, string message)
        {
            var objectCreature = NWScript.GetFirstPC();

            while(NWScript.GetIsObjectValid(objectCreature) == NWScript.TRUE)
            {

                NWScript.SendMessageToPC(objectCreature,"-----");
                NWScript.SendMessageToPC(objectCreature,"-----");
                NWScript.SendMessageToPC(objectCreature,message);
                NWScript.SendMessageToPC(objectCreature,"-----");
                NWScript.SendMessageToPC(objectCreature,"-----");
                objectCreature = NWScript.GetNextPC();

            }

        }
}
}



