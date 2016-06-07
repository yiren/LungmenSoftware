using System.Collections.Generic;
using LungmenSoftware.Models.NUMACFirmware;

namespace LungmenSoftware.MigrationNumac
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LungmenSoftware.Models.NUMACFirmware.NumacFirewareDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"MigrationNumac";
        }

        protected override void Seed(LungmenSoftware.Models.NUMACFirmware.NumacFirewareDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            if (context.Chassis.Any() || context.ChassisBoards.Any() || context.EPROMs.Any())
            {
                context.Chassis.RemoveRange(context.Chassis.ToList());
                context.ChassisBoards.RemoveRange(context.ChassisBoards.ToList());
                context.EPROMs.RemoveRange(context.EPROMs.ToList());
                context.SaveChanges();
            }
            

            //APRM A
            //Chassis
            Chassis APRM_A = new Chassis()
            {
                ChassisId = Guid.NewGuid(),
                ChassisName = "APRM A",
                ChasisSerialNumber = "98993756T1-12",
                Equipment = "NMS",
                Panel = "1H12-PL-1102"

            };


            //Chassis Board ASP A9
            ChassisBoard ASPModuleA9APRM = new ChassisBoard()
            {
                ChassisBoardId = Guid.NewGuid(),
                ChassBoardName = "ASP Module A9",
                Chassis = APRM_A,
                ChassisId = APRM_A.ChassisId,

            };

            //Chassis Board ASP A10
            ChassisBoard ASPModuleA10APRM = new ChassisBoard()
            {
                ChassisBoardId = Guid.NewGuid(),
                ChassBoardName = "ASP Module A10",
                Chassis = APRM_A,
                ChassisId = APRM_A.ChassisId,

            };

            //Chassis Board ASP A18
            ChassisBoard ASPModuleA18APRM = new ChassisBoard()
            {
                ChassisBoardId = Guid.NewGuid(),
                ChassBoardName = "ASP Module A18",
                Chassis = APRM_A,
                ChassisId = APRM_A.ChassisId,

            };

            //Computer Module A19
            ChassisBoard ComputerA19APRM = new ChassisBoard()
            {
                ChassisBoardId = Guid.NewGuid(),
                ChassBoardName = "Computer Module A19",
                Chassis = APRM_A,
                ChassisId = APRM_A.ChassisId,

            };

            //Digital Module A21
            ChassisBoard DigitalA21APRM = new ChassisBoard()
            {
                ChassisBoardId = Guid.NewGuid(),
                ChassBoardName = "Digital Control Module A21",
                Chassis = APRM_A,
                ChassisId = APRM_A.ChassisId,

            };


            //EPROMs APRM A9

            List<EPROM> ASPA9APRM_EPROMs = new List<EPROM>()
            {
                new EPROM()
                {
                    EPROMId = Guid.NewGuid(),
                    EPROMAssembly = "265A3025G003",
                    EPROMAssemblyRev = 0,
                    EPROMProgram = "265A3251P001",
                    EPROMProgramRev = 0,
                    PartsListRev = 3,
                    EPROMSerialNumber = "598993756-021",
                    SocketLocation = "U51",
                    ChassisBoardId = ASPModuleA9APRM.ChassisBoardId
                },
                new EPROM()
                {
                    EPROMId = Guid.NewGuid(),
                    EPROMAssembly = "265A3025G003",
                    EPROMAssemblyRev = 0,
                    EPROMProgram = "265A3252P001",
                    EPROMProgramRev = 0,
                    PartsListRev = 3,
                    EPROMSerialNumber = "598993756-021",
                    SocketLocation = "U52",
                    ChassisBoardId = ASPModuleA9APRM.ChassisBoardId
                },
                new EPROM()
                {
                    EPROMId = Guid.NewGuid(),
                    EPROMAssembly = "265A3027G003",
                    EPROMAssemblyRev = 0,
                    EPROMProgram = "265A3253P001",
                    EPROMProgramRev = 0,
                    PartsListRev = 3,
                    EPROMSerialNumber = "598993756-021",
                    SocketLocation = "U53",
                    ChassisBoardId = ASPModuleA9APRM.ChassisBoardId
                },
            };

            context.Chassis.Add(APRM_A);
            context.ChassisBoards.Add(ASPModuleA9APRM);
            context.EPROMs.AddRange(ASPA9APRM_EPROMs);

            context.SaveChanges();

            //EPROMs APRM A10

            List<EPROM> ASPA10APRM_EPROMs = new List<EPROM>()
            {
                new EPROM()
                {
                    EPROMId = Guid.NewGuid(),
                    EPROMAssembly = "265A3025G003",
                    EPROMAssemblyRev = 0,
                    EPROMProgram = "265A3251P001",
                    EPROMProgramRev = 0,
                    PartsListRev = 3,
                    EPROMSerialNumber = "5991019-016",
                    SocketLocation = "U51",
                    ChassisBoardId = ASPModuleA10APRM.ChassisBoardId
                },
                new EPROM()
                {
                    EPROMId = Guid.NewGuid(),
                    EPROMAssembly = "265A3026G003",
                    EPROMAssemblyRev = 0,
                    EPROMProgram = "265A3252P001",
                    EPROMProgramRev = 0,
                    PartsListRev = 3,
                    EPROMSerialNumber = "5991019-016",
                    SocketLocation = "U52",
                    ChassisBoardId = ASPModuleA10APRM.ChassisBoardId
                },
                new EPROM()
                {
                    EPROMId = Guid.NewGuid(),
                    EPROMAssembly = "265A3027G003",
                    EPROMAssemblyRev = 0,
                    EPROMProgram = "265A3253P001",
                    EPROMProgramRev = 0,
                    PartsListRev = 3,
                    EPROMSerialNumber = "5991019-016",
                    SocketLocation = "U53",
                    ChassisBoardId = ASPModuleA10APRM.ChassisBoardId
                },
            };

            context.ChassisBoards.Add(ASPModuleA10APRM);
            context.EPROMs.AddRange(ASPA10APRM_EPROMs);

            context.SaveChanges();

            //EPROMs APRM A18

            List<EPROM> ASPA18APRM_EPROMs = new List<EPROM>()
            {
                new EPROM()
                {
                    EPROMId = Guid.NewGuid(),
                    EPROMAssembly = "265A3237G006",
                    EPROMAssemblyRev = 0,
                    EPROMProgram = "343A19993P001",
                    EPROMProgramRev = 0,
                    PartsListRev = 6,
                    EPROMSerialNumber = "02028907A2-012",
                    SocketLocation = "U51",
                    ChassisBoardId = ASPModuleA18APRM.ChassisBoardId
                },
                new EPROM()
                {
                    EPROMId = Guid.NewGuid(),
                    EPROMAssembly = "265A3238G006",
                    EPROMAssemblyRev = 0,
                    EPROMProgram = "343A19994P001",
                    EPROMProgramRev = 0,
                    PartsListRev = 6,
                    EPROMSerialNumber = "02028907A2-012",
                    SocketLocation = "U51",
                    ChassisBoardId = ASPModuleA18APRM.ChassisBoardId
                },
                new EPROM()
                {
                    EPROMId = Guid.NewGuid(),
                    EPROMAssembly = "265A3239G006",
                    EPROMAssemblyRev = 0,
                    EPROMProgram = "343A19995P001",
                    EPROMProgramRev = 0,
                    PartsListRev = 6,
                    EPROMSerialNumber = "02028907A2-012",
                    SocketLocation = "U51",
                    ChassisBoardId = ASPModuleA18APRM.ChassisBoardId
                },
            };


            context.ChassisBoards.Add(ASPModuleA18APRM);
            context.EPROMs.AddRange(ASPA18APRM_EPROMs);

            context.SaveChanges();

            //EPROMs APRM A19

            List<EPROM> ComputerA19APRM_EPROMs = new List<EPROM>()
            {
                new EPROM()
                {
                    EPROMId = Guid.NewGuid(),
                    EPROMAssembly = "265A3088G029",
                    EPROMAssemblyRev = 2,
                    EPROMProgram = "343A2121P001",
                    EPROMProgramRev = 1,
                    PartsListRev = 29,
                    EPROMSerialNumber = "02028907A3-012",
                    SocketLocation = "U1",
                    ChassisBoardId = ComputerA19APRM.ChassisBoardId
                },
                new EPROM()
                {
                    EPROMId = Guid.NewGuid(),
                    EPROMAssembly = "265A3088G029",
                    EPROMAssemblyRev = 2,
                    EPROMProgram = "343A2122P001",
                    EPROMProgramRev = 1,
                    PartsListRev = 29,
                    EPROMSerialNumber = "02028907A3-012",
                    SocketLocation = "U9",
                    ChassisBoardId = ComputerA19APRM.ChassisBoardId
                },
            };

            context.ChassisBoards.Add(ComputerA19APRM);
            context.EPROMs.AddRange(ComputerA19APRM_EPROMs);

            context.SaveChanges();

            //EPROMs APRM A21

            List<EPROM> DigitalA21APRM_EPROMs = new List<EPROM>()
            {
                new EPROM()
                {
                    EPROMId = Guid.NewGuid(),
                    EPROMAssembly = "265A3343G009",
                    EPROMAssemblyRev = 0,
                    EPROMProgram = "343A1986P001",
                    EPROMProgramRev = 1,
                    PartsListRev = 12,
                    EPROMSerialNumber = "02028907A3-012",
                    SocketLocation = "U13",
                    ChassisBoardId = DigitalA21APRM.ChassisBoardId
                },
                new EPROM()
                {
                    EPROMId = Guid.NewGuid(),
                    EPROMAssembly = "265A3087G022",
                    EPROMAssemblyRev = 1,
                    EPROMProgram = "343A1987P001",
                    EPROMProgramRev = 0,
                    PartsListRev = 28,
                    EPROMSerialNumber = "02028907A3-012",
                    SocketLocation = "U14",
                    ChassisBoardId = DigitalA21APRM.ChassisBoardId
                },
            };

            context.ChassisBoards.Add(DigitalA21APRM);
            context.EPROMs.AddRange(DigitalA21APRM_EPROMs);

            context.SaveChanges();



            //context.Chassis.Add(APRM_A);
            //context.ChassisBoards.Add(ASPModuleA9APRM);
            //context.EPROMs.AddRange(EPROMsForAPRM_A);

            //context.SaveChanges();








        }
    }
}
