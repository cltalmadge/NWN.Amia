using System;
using JetBrains.Annotations;
using NWN.Amia.Main.Core.Types;
using NWN.Core;
using NWN.Core.NWNX;

namespace NWN.Amia.Main.Managed.Encounters.Invasions
{
    
    public class TriggerSpawnCreatures 
    {
        
        public void InvasionGeneric(var area, var size, var random, string creaturetype1, string creaturetype2, string creaturetype3, string creaturetype4, string creaturetype5, string lieutentant, string boss, string message)
        {
            string areaName = NWScript.GetName(area);
            int totalPLC = 150; 
            int totalMobs = Convert.ToInt32(random.Next(random) + size);
            int totalLieutentants = Convert.ToInt32(random.Next(random/2) + size*0.75);

            SpawnPLC(area, totalPLC);

            NWScript.DelayCommand(15.0,SpawnMobs(area, totalMobs,creaturetype1,creaturetype2,creaturetype3,creaturetype4,creaturetype5));
            NWScript.DelayCommand(30.0,SpawnLieutentants(area, totalLieutentants, lieutentant));
            NWScript.DelayCommand(45.0,SpawnLieutentants(area, boss));
            NWScript.DelayCommand(60.0,MassMessage(area, message));
        }

        public void InvasionBeasts(var area, var size, var random)
        {
            string areaName = NWScript.GetName(area);
            string creature1 = "beasthero";
            string creature2 = "elitebeastarcher";
            string creature3 = "beastshaman";
            string creature4 = "beastmanchampion";
            string creature5 = "beastmonk";
            string lieutentant = "beastguard";
            string boss = "invasionbeastbs";
            string message = "News quickly spreads of an amassing army of Beastmen in " + areaName + ". They must be stopped before it is too late!";

            int totalPLC = 150; 
            int totalMobs = Convert.ToInt32(random.Next(random) + size);
            int totalLieutentants = Convert.ToInt32(random.Next(random/2) + size*0.75);

            SpawnPLC(area, totalPLC);

            NWScript.DelayCommand(15.0,SpawnMobs(area, totalMobs,creaturetype1,creaturetype2,creaturetype3,creaturetype4,creaturetype5));
            NWScript.DelayCommand(30.0,SpawnLieutentants(area, totalLieutentants, lieutentant));
            NWScript.DelayCommand(45.0,SpawnLieutentants(area, boss));
            NWScript.DelayCommand(60.0,MassMessage(area, message));

        }

        
        public void InvasionGoblins(var area, var size, var random)
        {
            string areaName = NWScript.GetName(area);
            string creature1 = "ds_yellowfang_5";
            string creature2 = "ds_yellowfang_1";
            string creature3 = "ds_yellowfang_2";
            string creature4 = "ds_yellowfang_1";
            string creature5 = "ds_yellowfang_2";
            string lieutentant = "ds_yellowfang_4";
            string boss = "ds_yellowfang_6";
            string message = "News quickly spreads of an amassing army of Goblins in " + areaName + ". They must be stopped before it is too late!";

            int totalPLC = 150; 
            int totalMobs = Convert.ToInt32(random.Next(random) + size);
            int totalLieutentants = Convert.ToInt32(random.Next(random/2) + size*0.75);

            SpawnPLC(area, totalPLC);

            NWScript.DelayCommand(15.0,SpawnMobs(area, totalMobs,creaturetype1,creaturetype2,creaturetype3,creaturetype4,creaturetype5));
            NWScript.DelayCommand(30.0,SpawnLieutentants(area, totalLieutentants, lieutentant));
            NWScript.DelayCommand(45.0,SpawnLieutentants(area, boss));
            NWScript.DelayCommand(60.0,MassMessage(area, message));
        }

        
        public void InvasionTrolls(var area, var size, var random)
        {
            string areaName = NWScript.GetName(area);
            string creature1 = "mountainguard";
            string creature2 = "mountainguard";
            string creature3 = "mounttroll";
            string creature4 = "mounttroll";
            string creature5 = "mounttroll";
            string lieutentant = "bigmounttroll";
            string boss = "invasiontrollbs";
            string message = "News quickly spreads of an amassing army of Trolls in " + areaName + ". They must be stopped before it is too late!";

            int totalPLC = 150; 
            int totalMobs = Convert.ToInt32(random.Next(random) + size);
            int totalLieutentants = Convert.ToInt32(random.Next(random/2) + size*0.75);

            SpawnPLC(area, totalPLC);

            NWScript.DelayCommand(15.0,SpawnMobs(area, totalMobs,creaturetype1,creaturetype2,creaturetype3,creaturetype4,creaturetype5));
            NWScript.DelayCommand(30.0,SpawnLieutentants(area, totalLieutentants, lieutentant));
            NWScript.DelayCommand(45.0,SpawnLieutentants(area, boss));
            NWScript.DelayCommand(60.0,MassMessage(area, message));
        }

        
        public void InvasionOrcs(var area, var size, var random)
        {
            string areaName = NWScript.GetName(area);
            string creature1 = "af_ds_ork";
            string creature2 = "arelithorc001";
            string creature3 = "arelithorc";
            string creature4 = "arelithorc";
            string creature5 = "orcbasher";
            string lieutentant = "orcboss001";
            string boss = "chosenofkilma002";
            string message = "News quickly spreads of an amassing army of Orcs in " + areaName + ". They must be stopped before it is too late!";

            int totalPLC = 150; 
            int totalMobs = Convert.ToInt32(random.Next(random) + size);
            int totalLieutentants = Convert.ToInt32(random.Next(random/2) + size*0.75);

            SpawnPLC(area, totalPLC);

            NWScript.DelayCommand(15.0,SpawnMobs(area, totalMobs,creaturetype1,creaturetype2,creaturetype3,creaturetype4,creaturetype5));
            NWScript.DelayCommand(30.0,SpawnLieutentants(area, totalLieutentants, lieutentant));
            NWScript.DelayCommand(45.0,SpawnLieutentants(area, boss));
            NWScript.DelayCommand(60.0,MassMessage(area, message));
        }

        public var GenerateRandomLocation(var area)
        {
            int widthInTiles;
            int heightinTiles;
            int widthInMeters;
            int heightInMeters;
            var xPosition; 
            var yPosition;
            var zPosition = 0.0; 
            var randomPositon;
            var facing = 0.0;
            var randomLocation;

            if(NWScript.GetIsObjectValid(area))
            {

                // Determining the width and height of the area in tiles
                widthInTiles = NWScript.GetAreaSize(NWScript.AREA_WIDTH, area);
                heightinTiles = NWScript.GetAreaSize(NWScript.AREA_HEIGHT, area);
                widthInMeters = widthInTiles*10; 
                heightInMeters = heightInTiles*10; 

                //Generate a random position in the area
                xPosition = NWScript.Random(widthInMeters*10) / 10.0;
                yPosition = NWScript.Random(heightInMeters*10) / 10.0; 
                randomPositon = NWScript.Vector(xPosition,yPosition,zPosition);
                randomLocation = NWScript.Location(area, randomPositon, facing);
                return randomLocation;

            }

        }

        private void SpawnPLC(var area, int totalPLC)
        { 
            int countPLC; 
            var objectPLC; 
            var randomLocation;

            while(countPLC < totalPLC)
            {            
              randomLocation = GenerateRandomLocation(area);
              objectPLC = CreatePLC(randomLocation);

              if(NWScript.GetIsObjectValid(objectPLC))
              {
                  countPLC++;
              }
            }

        }

        private void CreatePLC(var objectLocation)
        {
            var random = random.Next(11);
            switch (random)
            {
                case 0: NWScript.CreateObject(NWScript.OBJECT_TYPE_PLACEABLE,"invasioncorpse1",objectLocation, false); break;
                case 1: NWScript.CreateObject(NWScript.OBJECT_TYPE_PLACEABLE,"invasionmisc3",objectLocation, false); break;
                case 2: NWScript.CreateObject(NWScript.OBJECT_TYPE_PLACEABLE,"invasioncorpse3",objectLocation, false); break;
                case 3: NWScript.CreateObject(NWScript.OBJECT_TYPE_PLACEABLE,"invasioncorpse5",objectLocation, false); break;
                case 4: NWScript.CreateObject(NWScript.OBJECT_TYPE_PLACEABLE,"invasionmisc5",objectLocation, false); break;
                case 5: NWScript.CreateObject(NWScript.OBJECT_TYPE_PLACEABLE,"invasiondebris1",objectLocation, false); break;
                case 6: NWScript.CreateObject(NWScript.OBJECT_TYPE_PLACEABLE,"invasioncamp",objectLocation, false); break;
                case 7: NWScript.CreateObject(NWScript.OBJECT_TYPE_PLACEABLE,"invasiondebris1",objectLocation, false); break;
                case 8: NWScript.CreateObject(NWScript.OBJECT_TYPE_PLACEABLE,"invasioncamp",objectLocation, false); break;
                case 9: NWScript.CreateObject(NWScript.OBJECT_TYPE_PLACEABLE,"invasionmisc3",objectLocation, false); break;
                case 10: NWScript.CreateObject(NWScript.OBJECT_TYPE_PLACEABLE,"invasionmisc3",objectLocation, false); break;
            }
        }

        private void SpawnMobs(var area, int totalMobs, string creaturetype1, string creaturetype2, string creaturetype3, string creaturetype4, string creaturetype5)
        {
            int countMobs; 
            int widthInTiles;
            int heightinTiles;
            int widthInMeters;
            int heightInMeters;
            var xPosition; 
            var yPosition;
            var zPosition = 0.0; 
            var randomPositon;
            var facing = 0.0;
            var randomLocation;
            var randomPosN;
            var randomPosS;
            var randomPosE;
            var randomPosW;
            var randomPosNE;
            var randomPosNW;
            var randomPosSE;
            var randomPosSW;

            var creature1;
            var creature2;
            var creature3;
            var creature4;
            var creature5;
            var creature6;
            var creature7;
            var creature8;
            var creature9;

            while(countMobs < totalMobs)
            {
                

              if(NWScript.GetIsObjectValid(area))
              {

                  // Determining the width and height of the area in tiles
                  widthInTiles = NWScript.GetAreaSize(NWScript.AREA_WIDTH, area);
                  heightinTiles = NWScript.GetAreaSize(NWScript.AREA_HEIGHT, area);
                  widthInMeters = widthInTiles*10; 
                  heightInMeters = heightInTiles*10; 

                  //Generate a random position in the area
                  xPosition = NWScript.Random(widthInMeters*10) / 10.0;
                  yPosition = NWScript.Random(heightInMeters*10) / 10.0; 
                  randomPositon = NWScript.Vector(xPosition,yPosition,zPosition);
                  randomLocation = NWScript.Location(area, randomPositon, facing);
                  return randomLocation;

              }

              creature1 = NWScript.CreateObject(NWScript.OBJECT_TYPE_CREATURE,creaturetype1,randomLocation,FALSE);

              if(NWScript.GetIsObjectValid(creature1))
              {       
                  randomPosN = NWScript.Vector(xPosition,yPosition + 1.0,zPosition);
                  randomPosS = NWScript.Vector(xPosition,yPosition - 1.0,zPosition);
                  randomPosE = NWScript.Vector(xPosition + 1.0,yPosition,zPosition);
                  randomPosW = NWScript.Vector(xPosition - 1.0,yPosition,zPosition);
                  randomPosNE = NWScript.Vector(xPosition + 1.0,yPosition + 1.0,zPosition);
                  randomPosNW = NWScript.Vector(xPosition - 1.0,yPosition + 1.0,zPosition);
                  randomPosSE = NWScript.Vector(xPosition + 1.0,yPosition - 1.0,zPosition);
                  randomPosSW = NWScript.Vector(xPosition - 1.0,yPosition - 1.0,zPosition);

                  creature2 = NWScript.CreateObject(NWScript.OBJECT_TYPE_CREATURE,creaturetype2,NWScript.Location(area, randomPosN, facing),FALSE);
                  creature3 = NWScript.CreateObject(NWScript.OBJECT_TYPE_CREATURE,creaturetype2,NWScript.Location(area, randomPosS, facing),FALSE);
                  creature4 = NWScript.CreateObject(NWScript.OBJECT_TYPE_CREATURE,creaturetype3,NWScript.Location(area, randomPosE, facing),FALSE);
                  creature5 = NWScript.CreateObject(NWScript.OBJECT_TYPE_CREATURE,creaturetype3,NWScript.Location(area, randomPosW, facing),FALSE);
                  creature6 = NWScript.CreateObject(NWScript.OBJECT_TYPE_CREATURE,creaturetype4,NWScript.Location(area, randomPosNE, facing),FALSE);
                  creature7 = NWScript.CreateObject(NWScript.OBJECT_TYPE_CREATURE,creaturetype4,NWScript.Location(area, randomPosNW, facing),FALSE);
                  creature8 = NWScript.CreateObject(NWScript.OBJECT_TYPE_CREATURE,creaturetype5,NWScript.Location(area, randomPosSE, facing),FALSE);
                  creature9 = NWScript.CreateObject(NWScript.OBJECT_TYPE_CREATURE,creaturetype5,NWScript.Location(area, randomPosSW, facing),FALSE);
                  countMobs++;
              }

            }


        }

        private void SpawnLieutentants(var area, int totalLieutentants, string lieutentant)
        {
            int countLieutentant; 
            var randomLocation;
            var objectCreature;

            while(countLieutentant < totalLieutentants)
            {
              randomLocation = GenerateRandomLocation(area);
              objectCreature = NWScript.CreateObject(NWScript.OBJECT_TYPE_CREATURE,lieutentant,randomLocation,FALSE);

              if(NWScript.GetIsObjectValid(objectCreature))
              {
                  countLieutentant++;
              }

            }

        }

        private void SpawnBoss(var area, string boss)
        {
            var countBoss;
            var randomLocation;
            var objectCreature;

            while(countBoss < 1)
            {
              randomLocation = GenerateRandomLocation(area);
              objectCreature = NWScript.CreateObject(NWScript.OBJECT_TYPE_CREATURE,boss,randomLocation,FALSE);

              if(NWScript.GetIsObjectValid(objectCreature))
              {
                  countBoss++;
              }      

        }

        void MassMessage(var area, string message)
        {
            var objectCreature = NWScript.GetFirstPC();

            while(NWScript.GetIsObjectValid(objectCreature))
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


