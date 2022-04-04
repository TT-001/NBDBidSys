using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NBD_TractionFive.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NBD_TractionFive.Data
{
    public static class NBDSeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {

            using (var context = new NBDContext(
                serviceProvider.GetRequiredService<DbContextOptions<NBDContext>>()))
            {
                Random random = new Random();
                if (!context.Clients.Any())
                {
                    context.Clients.AddRange(
                        new Client
                        {
                            cliName = "SM Mega Mall",
                            Street = "420 Mad dog lane",
                            cliCity = "Niagara Falls",
                            Province = Province.ON,
                            ZipCode = "L2G 3G9",
                            cliContactName = "Clarise Rouche",
                            clicontactType = "Assistant manager of sales",
                            cliPhonenumber = 28900156741

                        },

                        new Client
                        {
                            cliName = "Light Residence Hotel",
                            Street = "325 whereevr I go",
                            cliCity = "Halifax",
                            ZipCode = "L2T 3G9",
                            Province = Province.NS,
                            cliContactName = "Marko Kaiser",
                            clicontactType = "Personal assistant",
                            cliPhonenumber = 9058831255

                        },

                         new Client
                         {
                             cliName = "Anna Events Corporation",
                             Street = "525 Jump Street",
                             cliCity = "Bathurst",
                             Province = Province.NB,
                             ZipCode = "L2P 3T9",
                             cliContactName = "Anna Marie Spritz",
                             clicontactType = "Spokesperson",
                             cliPhonenumber = 9051108123

                         },

                         new Client
                         {
                             cliName = "Tee Coffee Shop",
                             Street = "582 Jump Street 2",
                             cliCity = "Welland",
                             Province = Province.ON,
                             ZipCode = "L2P 3T9",
                             cliContactName = "Arnold Ling",
                             clicontactType = "HR",
                             cliPhonenumber = 2895451782

                         },

                         new Client
                         {
                             cliName = "Pentagon Residence Hotel",
                             Street = "420 Mad dog lane",
                             cliCity = "Hamilton",
                             Province = Province.ON,
                             ZipCode = "L2G 3G9",
                             cliContactName = "Clara Eigenman",
                             clicontactType = "HR",
                             cliPhonenumber = 9052771113

                         }
                    );
                    context.SaveChanges();
                }
                if (!context.Projects.Any())
                {
                    context.Projects.AddRange(

                    new Project
                    {
                        projBeginDate = DateTime.Parse("03-05-2020"),
                        projCompletionDate = DateTime.Today.AddDays(random.Next(60)),
                        projSite = "Main Entrance Middle Wing",
                        projCity = "Toronto",
                        Province = Province.ON,
                        ZipCode = "L2P 3T9",
                        cliID = context.Clients.FirstOrDefault(c => c.cliName == "SM Mega Mall").cliID
                    },

                    new Project
                    {
                        projBeginDate = DateTime.Parse("04-03-2020"),
                        projCompletionDate = DateTime.Parse("04-29-2020"),
                        projSite = "Level 2 Garder Area",
                        projCity = "Charlottetown",
                        Province = Province.PE,
                        ZipCode = "L2P 3C8",
                        cliID = context.Clients.FirstOrDefault(c => c.cliName == "SM Mega Mall").cliID
                    },

                    new Project
                    {
                        projBeginDate = DateTime.Parse("10-10-2020"),
                        projCompletionDate = DateTime.Parse("10-31-2020"),
                        projSite = "Front Desk/Main Entrance",
                        projCity = "Stratford",
                        Province = Province.PE,
                        ZipCode = "L2P 3C9",
                        cliID = context.Clients.FirstOrDefault(c => c.cliName == "Light Residence Hotel").cliID
                    },

                    new Project
                    {
                        projBeginDate = DateTime.Parse("10-10-2020"),
                        projCompletionDate = DateTime.Parse("10-31-2020"),
                        projSite = "Balcony Area in Luxury Room 355",
                        projCity = "Halifax",
                        Province = Province.NS,
                        ZipCode = "L2P 3C8",
                        cliID = context.Clients.FirstOrDefault(c => c.cliName == "Light Residence Hotel").cliID
                    },

                    new Project
                    {
                        projBeginDate = DateTime.Parse("08-05-2020"),
                        projCompletionDate = DateTime.Parse("08-25-2020"),
                        projSite = "65 Golden St. Niagara Falls/Pool Area",
                        projCity = "Sydney",
                        Province = Province.PE,
                        ZipCode = "L2P 5P6",
                        cliID = context.Clients.FirstOrDefault(c => c.cliName == "Anna Events Corporation").cliID
                    },

                    new Project
                    {
                        projBeginDate = DateTime.Parse("09-10-2020"),
                        projCompletionDate = DateTime.Parse("09-30-2020"),
                        projSite = "101 Pine St. West High Grimsby/Auditorium",
                        projCity = "Cambridge",
                        Province = Province.ON,
                        ZipCode = "L2P 3C8",
                        cliID = context.Clients.FirstOrDefault(c => c.cliName == "Anna Events Corporation").cliID
                    },

                     new Project
                     {
                         projBeginDate = DateTime.Parse("02-05-2020"),
                         projCompletionDate = DateTime.Parse("02-20-2020"),
                         projSite = "Patio Area Front/Back",
                         projCity = "Paradise",
                         Province = Province.NL,
                         ZipCode = "L2P 3C8",
                         cliID = context.Clients.FirstOrDefault(c => c.cliName == "Tee Coffee Shop").cliID
                     },

                     new Project
                     {
                         projBeginDate = DateTime.Parse("02-05-2020"),
                         projCompletionDate = DateTime.Parse("02-20-2020"),
                         projSite = "Main Area/Ground Level",
                         projCity = "Mount Pearl",
                         Province = Province.PE,
                         ZipCode = "L2P 3C8",
                         cliID = context.Clients.FirstOrDefault(c => c.cliName == "Tee Coffee Shop").cliID
                     },

                     new Project
                     {
                         projBeginDate = DateTime.Parse("12-15-2020"),
                         projCompletionDate = DateTime.Parse("12-30-2020"),
                         projSite = "Main Entrance",
                         projCity = "Oshawa",
                         Province = Province.ON,
                         ZipCode = "L2P 3C8",
                         cliID = context.Clients.FirstOrDefault(c => c.cliName == "Pentagon Residence Hotel").cliID
                     },

                     new Project
                     {
                         projBeginDate = DateTime.Parse("11-05-2020"),
                         projCompletionDate = DateTime.Parse("11-25-2020"),
                         projSite = "Room 112/Room 113/Room 114",
                         projCity = "Montreal",
                         Province = Province.QC,
                         ZipCode = "L2P 3C8",
                         cliID = context.Clients.FirstOrDefault(c => c.cliName == "Pentagon Residence Hotel").cliID
                     });
                    context.SaveChanges();
                }
                if (!context.Employees.Any())
                {
                    context.Employees.AddRange(

                        new Employee
                        {
                            empFirst = "Aaron",
                            empLast = "Bailey",
                            empPhoneNumber = 9053275568,
                            empType = "Designer",
                            eMail = "aBailey@outlook.com"
                        },
                        new Employee
                        {
                            empFirst = "Monique",
                            empLast = "Leies",
                            empPhoneNumber = 9053271457,
                            empType = "Designer",
                            eMail = "mLeies@outlook.com"
                        },
                        new Employee
                        {
                            empFirst = "Johnathon",
                            empLast = "Kruis",
                            empPhoneNumber = 9053279983,
                            empType = "Designer",
                            eMail = "joKruis@outlook.com"
                        },
                        new Employee
                        {
                            empFirst = "Jeremy",
                            empLast = "DeMarsh",
                            empPhoneNumber = 9053273355,
                            empType = "Designer",
                            eMail = "jDeM@outlook.com"
                        },
                        new Employee
                        {
                            empFirst = "Gaurav",
                            empLast = "Bassi",
                            empPhoneNumber = 9053274456,
                            empType = "Designer",
                            eMail = "gBassi@outlook.com"
                        },
                        new Employee
                        {
                            empFirst = "Tamara",
                            empLast = "Bakken",
                            empPhoneNumber = 4087753645,
                            empType = "Designer",
                            eMail = "tBakken@outlook.com"
                        },
                        new Employee
                        {
                            empFirst = "Jeff",
                            empLast = "Martinson",
                            empPhoneNumber = 9053274412,
                            empType = "Sales Associate",
                            eMail = "jMartinson@outlook.com"
                        },
                        new Employee
                        {
                            empFirst = "Julianna",
                            empLast = "Walters",
                            empPhoneNumber = 9053278562,
                            empType = "Sales Associate",
                            eMail = "jWalters@outlook.com"
                        },
                        new Employee
                        {
                            empFirst = "Bob",
                            empLast = "Reinhardt",
                            empPhoneNumber = 4087753652,
                            empType = "Sales Associate",
                            eMail = "bReinhardt@outlook.com"
                        },
                        new Employee
                        {
                            empFirst = "Stan",
                            empLast = "Lee",
                            empPhoneNumber = 9053568790,
                            empType = "Management",
                            eMail = "SLee@outlook.com"
                        }
                    );
                    context.SaveChanges();
                }
                //if (!context.BidStates.Any())
                //{
                //    context.BidStates.AddRange(
                //        new BidState
                //        {
                //            status = "Pending"
                //        },
                //        new BidState
                //        {
                //            status = "Denied"
                //        },
                //        new BidState
                //        {
                //            status = "Approved"
                //        }
                //    );
                //    context.SaveChanges();
                //}
                if (!context.Labours.Any())
                {
                    context.Labours.AddRange(

                        new Labour
                        {
                            labDescription = "Production workers",
                            labPrice = 30.00
                        },
                        new Labour
                        {
                            labDescription = "Design consultant",
                            labPrice = 65.00
                        },
                        new Labour
                        {
                            labDescription = "Heavy equipment operator",
                            labPrice = 65.00
                        },
                        new Labour
                        {
                            labDescription = "Excavator",
                            labPrice = 100.00
                        }
                    );

                    context.SaveChanges();
                }

                if (!context.InvTypes.Any())
                {
                    context.InvTypes.AddRange(

                        new InvType
                        {
                            itType = "Plants"
                        },
                        new InvType
                        {
                            itType = "Pottery"
                        },
                        new InvType
                        {
                            itType = "Materials"
                        }
                    );

                    context.SaveChanges();
                }

                if (!context.Inventories.Any())
                {
                    context.Inventories.AddRange(

                        new Inventory
                        {
                            invName = "Laccospadix Australasica Palm",
                            invSize = "15 gal",
                            invPrice = 450.00,
                            itID = context.InvTypes.FirstOrDefault(i => i.itType == "Plants").itID
                        },
                        new Inventory
                        {
                            invName = "Caryota Mitis",
                            invSize = "7 gal",
                            invPrice = 233.00,
                            itID = context.InvTypes.FirstOrDefault(i => i.itType == "Plants").itID
                        },
                        new Inventory
                        {
                            invName = "Marginata",
                            invSize = "2 gal",
                            invPrice = 45.00,
                            itID = context.InvTypes.FirstOrDefault(i => i.itType == "Plants").itID
                        },
                        new Inventory
                        {
                            invName = "Ceratozamia Molonggo",
                            invSize = "1 gal",
                            invPrice = 400.00,
                            itID = context.InvTypes.FirstOrDefault(i => i.itType == "Plants").itID
                        },
                        new Inventory
                        {
                            invName = "Ficus",
                            invSize = "2 gal",
                            invPrice = 90.00,
                            itID = context.InvTypes.FirstOrDefault(i => i.itType == "Plants").itID
                        },
                        new Inventory
                        {
                            invName = "Arecastrum Coco",
                            invSize = "2 gal",
                            invPrice = 458.00,
                            itID = context.InvTypes.FirstOrDefault(i => i.itType == "Plants").itID
                        },
                        new Inventory
                        {
                            invName = "Green Ti",
                            invSize = "2 gal",
                            invPrice = 92.00,
                            itID = context.InvTypes.FirstOrDefault(i => i.itType == "Plants").itID
                        },
                         new Inventory
                         {
                             invName = "Arringa Pinnata",
                             invSize = "2 gal",
                             invPrice = 310.00,
                             itID = context.InvTypes.FirstOrDefault(i => i.itType == "Plants").itID
                         },


                        new Inventory
                        {
                            invName = "Granite Fountain (GFN48)",
                            invSize = "48 in",
                            invPrice = 750.00,
                            itID = context.InvTypes.FirstOrDefault(i => i.itType == "Pottery").itID
                        },
                        new Inventory
                        {
                            invName = "Granite Pots (GP50)",
                            invSize = "50 gal",
                            invPrice = 195.00,
                            itID = context.InvTypes.FirstOrDefault(i => i.itType == "Pottery").itID
                        },
                        new Inventory
                        {
                            invName = "Marble Tiles (MTL09)",
                            invSize = "1 sq ft",
                            invPrice = 10.00,
                            itID = context.InvTypes.FirstOrDefault(i => i.itType == "Pottery").itID
                        },
                        new Inventory
                        {
                            invName = "T/C Figurine Swan(Glazed)",
                            invSize = "1 sq ft",
                            invPrice = 25.00,
                            itID = context.InvTypes.FirstOrDefault(i => i.itType == "Pottery").itID

                        },
                        new Inventory
                        {
                            invName = "T/C Pot(Hand Painted)",
                            invSize = "1 sq ft",
                            invPrice = 53.00,
                            itID = context.InvTypes.FirstOrDefault(i => i.itType == "Pottery").itID
                        },


                        new Inventory
                        {
                            invName = "Decorative Cedar Bark (CBRK5)",
                            invSize = "5 cu ft",
                            invPrice = 7.50,
                            itID = context.InvTypes.FirstOrDefault(i => i.itType == "Materials").itID
                        },
                        new Inventory
                        {
                            invName = "Crushed Granite",
                            invSize = "yard",
                            invPrice = 7.50,
                            itID = context.InvTypes.FirstOrDefault(i => i.itType == "Materials").itID
                        },
                        new Inventory
                        {
                            invName = "Pea Gravel",
                            invSize = "1 sq ft",
                            invPrice = 8.00,
                            itID = context.InvTypes.FirstOrDefault(i => i.itType == "Materials").itID
                        },
                        new Inventory
                        {
                            invName = "1 inch Gravel",
                            invSize = "1 sq ft",
                            invPrice = 5.90,
                            itID = context.InvTypes.FirstOrDefault(i => i.itType == "Materials").itID
                        },
                        new Inventory
                        {
                            invName = "Patio Block",
                            invSize = "1 sq ft",
                            invPrice = .56,
                            itID = context.InvTypes.FirstOrDefault(i => i.itType == "Materials").itID
                        }
                    );
                }
                if (!context.inventoryVMs.Any())
                {
                    context.inventoryVMs.AddRange(

               new inventoryVM
                        {
                            quantityOnHands = 350,
                            name = "Locca Australasica",
                            price = 450m,
                            type = "Plant"
                        },
               new inventoryVM
                        {
                            quantityOnHands = 350,
                            name = "Arringa Pinnata",
                            price = 310m,
                            type = "Plant"
                        },
               new inventoryVM
                        {
                            quantityOnHands = 400,
                            name = "Ceratozamia Molonggo",
                            price = 400m,
                            type = "Plant"
                        },
               new inventoryVM
               {
                   quantityOnHands = 510,
                   name = "Arecastrum Coco",
                   price = 458m,
                   type = "Plant"
               },
               new inventoryVM
               {
                   quantityOnHands = 510,
                   name = "Cariota Mitis",
                   price = 233m,
                   type = "Plant"
               },
               new inventoryVM
               {
                   quantityOnHands = 600,
                   name = "Green Ti",
                   price = 92m,
                   type = "Plant"
               },
               new inventoryVM
               {
                   quantityOnHands = 600,
                   name = "Ficus Green",
                   price = 240m,
                   type = "Plant"
               },
               new inventoryVM
               {
                   quantityOnHands = 300,
                   name = "Marginata",
                   price = 45m,
                   type = "Plant"
               },
               new inventoryVM
               {
                   quantityOnHands = 180,
                   name = "T/C Pot(Hand Painted)",
                   price = 53m,
                   type = "Pot"
               },
               new inventoryVM
               {
                   quantityOnHands = 150,
                   name = "Granite Pot(Oblate)",
                   price = 110m,
                   type = "Pot"
               },
               new inventoryVM
               {
                   quantityOnHands = 200,
                   name = "T/C Figurine Swan(Glazed)",
                   price = 25m,
                   type = "Pot"
               },
               new inventoryVM
               {
                   quantityOnHands = 120,
                   name = "Marble Bird(Doric Based)",
                   price = 128m,
                   type = "Pot"
               },
               new inventoryVM
               {
                   quantityOnHands = 200,
                   name = "Granite Fountain(Fluted Basin)",
                   price = 457m,
                   type = "Pot"
               },
               new inventoryVM
               {
                   quantityOnHands = 300,
                   name = "Decorative Cedar Bark",
                   price = 7.50m,
                   type = "Decor Material"
               },
               new inventoryVM
               {
                   quantityOnHands = 310,
                   name = "Crushed Granite",
                   price = 7.50m,
                   type = "Decor Material"
               },
               new inventoryVM
               {
                   quantityOnHands = 310,
                   name = "Pea Gravel",
                   price = 8.00m,
                   type = "Decor Material"
               },
               new inventoryVM
               {
                   quantityOnHands = 310,
                   name = "1inch Gravel",
                   price = 5.90m,
                   type = "Decor Material"
               },
               new inventoryVM
               {
                   quantityOnHands = 310,
                   name = "Top Soil",
                   price = 12.50m,
                   type = "Decor Material"
               },
               new inventoryVM
               {
                   quantityOnHands = 310,
                   name = "Patio Block",
                   price = .56m,
                   type = "Decor Material"
               });
                    context.SaveChanges();
            }

                if (!context.Bids.Any())
                {
                    context.Bids.AddRange(

                    new Bid
                    {
                        bidDate = DateTime.Parse("03-10-2020"),
                        bidAmount = 6996.00m,
                        projID = context.Projects.FirstOrDefault(p => p.projSite == "Main Entrance Middle Wing").projID,
                        SalesID = context.Employees.FirstOrDefault(e => e.empFirst == "Jeff").empID,
                        DesignerID = context.Employees.FirstOrDefault(e => e.empFirst == "Aaron").empID,
                        BidState = BidState.Designing
                    },
                    new Bid
                    {
                        bidDate = DateTime.Parse("04-08-2020"),
                        bidAmount = 3950.00m,
                        projID = context.Projects.FirstOrDefault(p => p.projSite == "Level 2 Garder Area").projID,
                        SalesID = context.Employees.FirstOrDefault(e => e.empFirst == "Julianna").empID,
                        DesignerID = context.Employees.FirstOrDefault(e => e.empFirst == "Monique").empID,
                        BidState = BidState.Designing
                    },
                    new Bid
                    {
                        bidDate = DateTime.Parse("10-18-2020"),
                        bidAmount = 2475.00m,
                        projID = context.Projects.FirstOrDefault(p => p.projSite == "Front Desk/Main Entrance").projID,
                        SalesID = context.Employees.FirstOrDefault(e => e.empFirst == "Julianna").empID,
                        DesignerID = context.Employees.FirstOrDefault(e => e.empFirst == "Johnathon").empID,
                        BidState = BidState.Designing
                    },
                    new Bid
                    {
                        bidDate = DateTime.Parse("10-11-2020"),
                        bidAmount = 4100.00m,
                        projID = context.Projects.FirstOrDefault(p => p.projSite == "Balcony Area in Luxury Room 355").projID,
                        SalesID = context.Employees.FirstOrDefault(e => e.empFirst == "Jeff").empID,
                        DesignerID = context.Employees.FirstOrDefault(e => e.empFirst == "Johnathon").empID,
                        BidState = BidState.Designing
                    },
                    new Bid
                    {
                        bidDate = DateTime.Parse("08-13-2020"),
                        bidAmount = 12240.00m,
                        projID = context.Projects.FirstOrDefault(p => p.projSite == "65 Golden St. Niagara Falls/Pool Area").projID,
                        SalesID = context.Employees.FirstOrDefault(e => e.empFirst == "Julianna").empID,
                        DesignerID = context.Employees.FirstOrDefault(e => e.empFirst == "Monique").empID,
                        BidState = BidState.Designing
                    },
                    new Bid
                    {
                        bidDate = DateTime.Parse("09-22-2020"),
                        bidAmount = 2800.00m,
                        projID = context.Projects.FirstOrDefault(p => p.projSite == "101 Pine St. West High Grimsby/Auditorium").projID,
                        SalesID = context.Employees.FirstOrDefault(e => e.empFirst == "Bob").empID,
                        DesignerID = context.Employees.FirstOrDefault(e => e.empFirst == "Jeremy").empID,
                        BidState = BidState.Designing
                    },
                    new Bid
                    {
                        bidDate = DateTime.Parse("02-13-2020"),
                        bidAmount = 6700.00m,
                        projID = context.Projects.FirstOrDefault(p => p.projSite == "Patio Area Front/Back").projID,
                        SalesID = context.Employees.FirstOrDefault(e => e.empFirst == "Bob").empID,
                        DesignerID = context.Employees.FirstOrDefault(e => e.empFirst == "Johnathon").empID,
                        BidState = BidState.Designing
                    },
                    new Bid
                    {
                        bidDate = DateTime.Parse("12-18-2020"),
                        bidAmount = 4419.00m,
                        projID = context.Projects.FirstOrDefault(p => p.projSite == "Main Entrance").projID,
                        SalesID = context.Employees.FirstOrDefault(e => e.empFirst == "Bob").empID,
                        DesignerID = context.Employees.FirstOrDefault(e => e.empFirst == "Aaron").empID,
                        BidState = BidState.Designing
                    },
                    new Bid
                    {
                        bidDate = DateTime.Parse("11-18-2020"),
                        bidAmount = 4310.00m,
                        projID = context.Projects.FirstOrDefault(p => p.projSite == "Room 112/Room 113/Room 114").projID,
                        SalesID = context.Employees.FirstOrDefault(e => e.empFirst == "Bob").empID,
                        DesignerID = context.Employees.FirstOrDefault(e => e.empFirst == "Monique").empID,
                        BidState = BidState.Designing
                    },
                    new Bid
                    {
                        bidDate = DateTime.Parse("02-06-2020"),
                        bidAmount = 9730.00m,
                        projID = context.Projects.FirstOrDefault(p => p.projSite == "Main Area/Ground Level").projID,
                        SalesID = context.Employees.FirstOrDefault(e => e.empFirst == "Julianna").empID,
                        DesignerID = context.Employees.FirstOrDefault(e => e.empFirst == "Aaron").empID,
                        BidState = BidState.Designing
                    });
                    context.SaveChanges();
                }

                if (!context.BidLabours.Any())
                {
                    context.BidLabours.AddRange(

                        new BidLabour
                        {
                            blHours = 30,
                            blCost = 900.00,
                            labID = context.Labours.FirstOrDefault(l => l.labID == 1).labID,
                            bidID = context.Bids.FirstOrDefault(b => b.bidID == 1).bidID
                        },
                        new BidLabour
                        {
                            blHours = 10,
                            blCost = 650.00,
                            labID = context.Labours.FirstOrDefault(l => l.labID == 2).labID,
                            bidID = context.Bids.FirstOrDefault(b => b.bidID == 1).bidID
                        },
                        new BidLabour
                        {
                            blHours = 10,
                            blCost = 650.00,
                            labID = context.Labours.FirstOrDefault(l => l.labID == 3).labID,
                            bidID = context.Bids.FirstOrDefault(b => b.bidID == 1).bidID
                        },
                        new BidLabour
                        {
                            blHours = 20,
                            blCost = 600.00,
                            labID = context.Labours.FirstOrDefault(l => l.labID == 1).labID,
                            bidID = context.Bids.FirstOrDefault(b => b.bidID == 2).bidID
                        },
                        new BidLabour
                        {
                            blHours = 10,
                            blCost = 650.00,
                            labID = context.Labours.FirstOrDefault(l => l.labID == 2).labID,
                            bidID = context.Bids.FirstOrDefault(b => b.bidID == 2).bidID
                        },
                        new BidLabour
                        {
                            blHours = 10,
                            blCost = 300.00,
                            labID = context.Labours.FirstOrDefault(l => l.labID == 1).labID,
                            bidID = context.Bids.FirstOrDefault(b => b.bidID == 3).bidID
                        },
                        new BidLabour
                        {
                            blHours = 10,
                            blCost = 650.00,
                            labID = context.Labours.FirstOrDefault(l => l.labID == 2).labID,
                            bidID = context.Bids.FirstOrDefault(b => b.bidID == 3).bidID
                        },
                        new BidLabour
                        {
                            blHours = 10,
                            blCost = 300.00,
                            labID = context.Labours.FirstOrDefault(l => l.labID == 1).labID,
                            bidID = context.Bids.FirstOrDefault(b => b.bidID == 4).bidID
                        },
                        new BidLabour
                        {
                            blHours = 10,
                            blCost = 650.00,
                            labID = context.Labours.FirstOrDefault(l => l.labID == 2).labID,
                            bidID = context.Bids.FirstOrDefault(b => b.bidID == 4).bidID
                        },
                        new BidLabour
                        {
                            blHours = 40,
                            blCost = 1200.00,
                            labID = context.Labours.FirstOrDefault(l => l.labID == 1).labID,
                            bidID = context.Bids.FirstOrDefault(b => b.bidID == 5).bidID
                        },
                        new BidLabour
                        {
                            blHours = 10,
                            blCost = 650.00,
                            labID = context.Labours.FirstOrDefault(l => l.labID == 2).labID,
                            bidID = context.Bids.FirstOrDefault(b => b.bidID == 5).bidID
                        },
                        new BidLabour
                        {
                            blHours = 10,
                            blCost = 650.00,
                            labID = context.Labours.FirstOrDefault(l => l.labID == 3).labID,
                            bidID = context.Bids.FirstOrDefault(b => b.bidID == 5).bidID
                        },
                        new BidLabour
                        {
                            blHours = 5,
                            blCost = 500.00,
                            labID = context.Labours.FirstOrDefault(l => l.labID == 4).labID,
                            bidID = context.Bids.FirstOrDefault(b => b.bidID == 5).bidID
                        },
                        new BidLabour
                        {
                            blHours = 20,
                            blCost = 600.00,
                            labID = context.Labours.FirstOrDefault(l => l.labID == 1).labID,
                            bidID = context.Bids.FirstOrDefault(b => b.bidID == 6).bidID
                        },
                        new BidLabour
                        {
                            blHours = 10,
                            blCost = 650.00,
                            labID = context.Labours.FirstOrDefault(l => l.labID == 2).labID,
                            bidID = context.Bids.FirstOrDefault(b => b.bidID == 6).bidID
                        },
                        new BidLabour
                        {
                            blHours = 10,
                            blCost = 650.00,
                            labID = context.Labours.FirstOrDefault(l => l.labID == 3).labID,
                            bidID = context.Bids.FirstOrDefault(b => b.bidID == 6).bidID
                        },
                        new BidLabour
                        {
                            blHours = 20,
                            blCost = 600.00,
                            labID = context.Labours.FirstOrDefault(l => l.labID == 1).labID,
                            bidID = context.Bids.FirstOrDefault(b => b.bidID == 7).bidID
                        },
                        new BidLabour
                        {
                            blHours = 10,
                            blCost = 650.00,
                            labID = context.Labours.FirstOrDefault(l => l.labID == 2).labID,
                            bidID = context.Bids.FirstOrDefault(b => b.bidID == 7).bidID
                        },
                        new BidLabour
                        {
                            blHours = 10,
                            blCost = 650.00,
                            labID = context.Labours.FirstOrDefault(l => l.labID == 3).labID,
                            bidID = context.Bids.FirstOrDefault(b => b.bidID == 7).bidID
                        },
                        new BidLabour
                        {
                            blHours = 30,
                            blCost = 900.00,
                            labID = context.Labours.FirstOrDefault(l => l.labID == 1).labID,
                            bidID = context.Bids.FirstOrDefault(b => b.bidID == 8).bidID
                        },
                        new BidLabour
                        {
                            blHours = 10,
                            blCost = 650.00,
                            labID = context.Labours.FirstOrDefault(l => l.labID == 2).labID,
                            bidID = context.Bids.FirstOrDefault(b => b.bidID == 8).bidID
                        },
                        new BidLabour
                        {
                            blHours = 10,
                            blCost = 650.00,
                            labID = context.Labours.FirstOrDefault(l => l.labID == 3).labID,
                            bidID = context.Bids.FirstOrDefault(b => b.bidID == 8).bidID
                        },
                        new BidLabour
                        {
                            blHours = 10,
                            blCost = 1000.00,
                            labID = context.Labours.FirstOrDefault(l => l.labID == 4).labID,
                            bidID = context.Bids.FirstOrDefault(b => b.bidID == 8).bidID
                        },
                        new BidLabour
                        {
                            blHours = 15,
                            blCost = 450.00,
                            labID = context.Labours.FirstOrDefault(l => l.labID == 1).labID,
                            bidID = context.Bids.FirstOrDefault(b => b.bidID == 9).bidID
                        },
                        new BidLabour
                        {
                            blHours = 10,
                            blCost = 650.00,
                            labID = context.Labours.FirstOrDefault(l => l.labID == 2).labID,
                            bidID = context.Bids.FirstOrDefault(b => b.bidID == 9).bidID
                        },
                        new BidLabour
                        {
                            blHours = 50,
                            blCost = 1500.00,
                            labID = context.Labours.FirstOrDefault(l => l.labID == 1).labID,
                            bidID = context.Bids.FirstOrDefault(b => b.bidID == 10).bidID
                        },
                        new BidLabour
                        {
                            blHours = 10,
                            blCost = 650.00,
                            labID = context.Labours.FirstOrDefault(l => l.labID == 2).labID,
                            bidID = context.Bids.FirstOrDefault(b => b.bidID == 10).bidID
                        },
                        new BidLabour
                        {
                            blHours = 20,
                            blCost = 1300.00,
                            labID = context.Labours.FirstOrDefault(l => l.labID == 3).labID,
                            bidID = context.Bids.FirstOrDefault(b => b.bidID == 10).bidID
                        }
                    );
                    context.SaveChanges();
                }
                if (!context.BidMaterials.Any())
                {
                    context.BidMaterials.AddRange(

                        new BidMaterial
                        {
                            bmQuantity = 4,
                            bmCost = 2996.00,
                            invID = context.Inventories.FirstOrDefault(i => i.invID == 1).invID,
                            bidID = context.Bids.FirstOrDefault(b => b.bidID == 1).bidID
                        },
                        new BidMaterial
                        {
                            bmQuantity = 20,
                            bmCost = 900.00,
                            invID = context.Inventories.FirstOrDefault(i => i.invID == 2).invID,
                            bidID = context.Bids.FirstOrDefault(b => b.bidID == 1).bidID
                        },
                        new BidMaterial
                        {
                            bmQuantity = 1,
                            bmCost = 750.00,
                            invID = context.Inventories.FirstOrDefault(i => i.invID == 6).invID,
                            bidID = context.Bids.FirstOrDefault(b => b.bidID == 1).bidID
                        },
                        new BidMaterial
                        {
                            bmQuantity = 100,
                            bmCost = 150.00,
                            invID = context.Inventories.FirstOrDefault(i => i.invID == 11).invID,
                            bidID = context.Bids.FirstOrDefault(b => b.bidID == 1).bidID
                        },
                        new BidMaterial
                        {
                            bmQuantity = 10,
                            bmCost = 750.00,
                            invID = context.Inventories.FirstOrDefault(i => i.invID == 3).invID,
                            bidID = context.Bids.FirstOrDefault(b => b.bidID == 2).bidID
                        },
                        new BidMaterial
                        {
                            bmQuantity = 10,
                            bmCost = 1950.00,
                            invID = context.Inventories.FirstOrDefault(i => i.invID == 7).invID,
                            bidID = context.Bids.FirstOrDefault(b => b.bidID == 2).bidID
                        },
                        new BidMaterial
                        {
                            bmQuantity = 5,
                            bmCost = 1165.00,
                            invID = context.Inventories.FirstOrDefault(i => i.invID == 2).invID,
                            bidID = context.Bids.FirstOrDefault(b => b.bidID == 3).bidID
                        },
                        new BidMaterial
                        {
                            bmQuantity = 4,
                            bmCost = 360.00,
                            invID = context.Inventories.FirstOrDefault(i => i.invID == 5).invID,
                            bidID = context.Bids.FirstOrDefault(b => b.bidID == 3).bidID
                        },
                        new BidMaterial
                        {
                            bmQuantity = 5,
                            bmCost = 450.00,
                            invID = context.Inventories.FirstOrDefault(i => i.invID == 5).invID,
                            bidID = context.Bids.FirstOrDefault(b => b.bidID == 4).bidID
                        },
                        new BidMaterial
                        {
                            bmQuantity = 270,
                            bmCost = 2700.00,
                            invID = context.Inventories.FirstOrDefault(i => i.invID == 8).invID,
                            bidID = context.Bids.FirstOrDefault(b => b.bidID == 4).bidID
                        },
                        new BidMaterial
                        {
                            bmQuantity = 10,
                            bmCost = 7490.00,
                            invID = context.Inventories.FirstOrDefault(i => i.invID == 1).invID,
                            bidID = context.Bids.FirstOrDefault(b => b.bidID == 5).bidID
                        },
                        new BidMaterial
                        {
                            bmQuantity = 1,
                            bmCost = 750.00,
                            invID = context.Inventories.FirstOrDefault(i => i.invID == 6).invID,
                            bidID = context.Bids.FirstOrDefault(b => b.bidID == 5).bidID
                        },
                        new BidMaterial
                        {
                            bmQuantity = 50,
                            bmCost = 1000.00,
                            invID = context.Inventories.FirstOrDefault(i => i.invID == 10).invID,
                            bidID = context.Bids.FirstOrDefault(b => b.bidID == 5).bidID
                        },
                        new BidMaterial
                        {
                            bmQuantity = 10,
                            bmCost = 900.00,
                            invID = context.Inventories.FirstOrDefault(i => i.invID == 5).invID,
                            bidID = context.Bids.FirstOrDefault(b => b.bidID == 6).bidID
                        },
                        new BidMaterial
                        {
                            bmQuantity = 20,
                            bmCost = 900.00,
                            invID = context.Inventories.FirstOrDefault(i => i.invID == 4).invID,
                            bidID = context.Bids.FirstOrDefault(b => b.bidID == 7).bidID
                        },
                        new BidMaterial
                        {
                            bmQuantity = 20,
                            bmCost = 3900.00,
                            invID = context.Inventories.FirstOrDefault(i => i.invID == 7).invID,
                            bidID = context.Bids.FirstOrDefault(b => b.bidID == 7).bidID
                        },
                        new BidMaterial
                        {
                            bmQuantity = 10,
                            bmCost = 750.00,
                            invID = context.Inventories.FirstOrDefault(i => i.invID == 3).invID,
                            bidID = context.Bids.FirstOrDefault(b => b.bidID == 8).bidID
                        },
                        new BidMaterial
                        {
                            bmQuantity = 20,
                            bmCost = 319.00,
                            invID = context.Inventories.FirstOrDefault(i => i.invID == 9).invID,
                            bidID = context.Bids.FirstOrDefault(b => b.bidID == 8).bidID
                        },
                        new BidMaterial
                        {
                            bmQuantity = 100,
                            bmCost = 150.00,
                            invID = context.Inventories.FirstOrDefault(i => i.invID == 11).invID,
                            bidID = context.Bids.FirstOrDefault(b => b.bidID == 8).bidID
                        },
                        new BidMaterial
                        {
                            bmQuantity = 6,
                            bmCost = 540.00,
                            invID = context.Inventories.FirstOrDefault(i => i.invID == 5).invID,
                            bidID = context.Bids.FirstOrDefault(b => b.bidID == 9).bidID
                        },
                        new BidMaterial
                        {
                            bmQuantity = 6,
                            bmCost = 1170.00,
                            invID = context.Inventories.FirstOrDefault(i => i.invID == 7).invID,
                            bidID = context.Bids.FirstOrDefault(b => b.bidID == 9).bidID
                        },
                        new BidMaterial
                        {
                            bmQuantity = 150,
                            bmCost = 1500.00,
                            invID = context.Inventories.FirstOrDefault(i => i.invID == 8).invID,
                            bidID = context.Bids.FirstOrDefault(b => b.bidID == 9).bidID
                        },
                        new BidMaterial
                        {
                            bmQuantity = 10,
                            bmCost = 2330.00,
                            invID = context.Inventories.FirstOrDefault(i => i.invID == 2).invID,
                            bidID = context.Bids.FirstOrDefault(b => b.bidID == 10).bidID
                        },
                        new BidMaterial
                        {
                            bmQuantity = 4,
                            bmCost = 3000.00,
                            invID = context.Inventories.FirstOrDefault(i => i.invID == 6).invID,
                            bidID = context.Bids.FirstOrDefault(b => b.bidID == 10).bidID
                        },
                        new BidMaterial
                        {
                            bmQuantity = 50,
                            bmCost = 1000.00,
                            invID = context.Inventories.FirstOrDefault(i => i.invID == 10).invID,
                            bidID = context.Bids.FirstOrDefault(b => b.bidID == 10).bidID
                        }
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}
