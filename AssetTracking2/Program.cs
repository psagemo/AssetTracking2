
using AssetTracking2.Models;
using AssetTracking2.Data;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Diagnostics;
/*
Level 1
    - Create a console app that have the following classes and objects: 
        * Laptop Computers 
            - MacBook 
            - Asus 
            - Lenovo 
        * Mobile Phones 
            - Iphone 
            - Samsung  
            - Nokia  
    You will need to create the appropriate fields, constructors and properties for each object, like purchase date, price, model name etc.
    All assets needs to be stored in database using Entity Framework Core with Create and Read functionality. 

Level 2
    - Create a program to create a list of assets (inputs) where the final result is to write the following to the console:
        * Sorted list with Class as primary (computers first, then phones)
        * Then sorted by purchase date
        * Mark any item *RED* if purchase date is less than 3 months away from 3 years. 
    Your application should handle FULL CRUD.

Level 3
    - Add offices to the model: 
        * You should be able to place items in 3 different offices around the world which will use the appropriate currency for that country. 
        * You should be able to input values in dollars and convert them to each currency (based on todays currency charts) 
        * When you write the list to the console:
            - Sorted first by office
            - Then Purchase date
            - Items *RED* if date less than 3 months away from 3 years
            - Items *Yellow* if date less than 6 months away from 3 years
            - Each item should have currency according to country 
    Your application should handle FULL CRUD.
    Your application should have some reporting features.

Done:
    - Print-Table functionality
    - Create
    - Read
    - Update
        * EditAsset-method
    - Delete
    - MOVE PURCHASEDATE FROM LAPTOP/MOBILEPHONE TO ASSET
    - Fix bug when editing asset
    - Fix bug when creating new asset (on SaveChanges())

TODO-list:    
    - Final Testing
    
 */



// Greeting user
Console.WriteLine();
Console.ForegroundColor = ConsoleColor.DarkBlue;
Console.WriteLine("----------------------------------------------------------------------------------------------------");
Console.WriteLine("------------------------------------- Welcome to AssetTracker! -------------------------------------");
Console.WriteLine("----------------------------------------------------------------------------------------------------");
Console.ResetColor();
Console.WriteLine();

// Initializing context
using AssetTracker2Context context = new AssetTracker2Context();

// Option to populate db with generated assets
PopulateDbOption(context);

// Adding Offices to context
AddOffices(context);

// Launching program
Main();

void Main()
{
    Console.WriteLine("Select and option by typing the corresponding number:");
    Console.WriteLine("Enter 'E' or 'EXIT' in order to close the program.");
    Console.WriteLine("1. Create Asset");
    Console.WriteLine("2. Edit or Delete existing Asset");
    Console.WriteLine("3. Print Assets");

    string input = Console.ReadLine();
    Console.WriteLine();

    switch (input.Trim().ToLower())
    {
        case "e":
        case "exit":
            break;

        case "1":
            CreateAsset();
            break;

        case "2":
            EditAsset();
            break;

        case "3":
            PrintAssets();
            break;
    }   
}

// Method to add Offices
static void AddOffices(AssetTracker2Context context)
{  
    // Get offices from database using context
    var offices = context.Offices
        .Where(o => o != null);

    var officeCount = context.Offices.Count();

    //Console.WriteLine("OfficeCount: " + officeCount);

    // Add offices if they don't already exist
    if (officeCount == 0)
    {
        Office USA = new Office()
        {
            Location = "USA",
            Currency = "USD"
        };
        context.Offices.Add(USA);
        Office Spain = new Office()
        {
            Location = "Spain",
            Currency = "EUR"
        };
        context.Offices.Add(Spain);
        
        Office Sweden = new Office()
        {
            Location = "Sweden",
            Currency = "SEK"
        };
        context.Offices.Add(Sweden);

        // Save context or print error
        try
        {
            // Saving context
            context.SaveChanges();
        }
        catch (Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Something went wrong:");
            Console.WriteLine(e.ToString());
            Console.ResetColor();
        }
    }   
}

// Method to add pre-generated assets
void PopulateDbOption(AssetTracker2Context context)
{
    // Get assets, mobile phones and laptops from database
    var assets = context.Assets
        .Where(a => a != null)
        .OrderBy(a => a.Type);

    var mobilePhones = context.MobilePhones
        .Where(m => m != null);

    var laptops = context.Laptops
        .Where(l => l != null);    

    var assetCount = context.Assets.Count();
    
    // Prompt user to add pre-generated assets to database
    Console.WriteLine("Would you like to populate the database with pre-generated assets? [Y/N]");
    string input = Console.ReadLine();
    Console.WriteLine();

    // Adding Mobilephones and assets if they don't already exist
    if (input.Trim().ToLower() == "y" || input.Trim().ToLower() == "yes")
    {                
        MobilePhone SamsungGalaxyS20 = new MobilePhone()
        {
            Brand = "Samsung",
            Model = "Galaxy S20",
        };
        if (mobilePhones.Any(m =>m.Brand == SamsungGalaxyS20.Brand && m.Model == SamsungGalaxyS20.Model)) { }
        else
        {
            context.MobilePhones.Add(SamsungGalaxyS20);
            context.SaveChanges();

            // Creating asset from mobile phone
            Asset SamsungGalaxyS20Asset = new Asset()
            {
                Type = "MobilePhone",
                MobilePhoneId = SamsungGalaxyS20.MobilePhoneId,
                Price = 4199,
                OfficeId = 3,
                PurchaseDate = Convert.ToDateTime("2020-03-30")
            };
            if (assets.Any(a => a.MobilePhoneId == SamsungGalaxyS20.MobilePhoneId)) { }
            else
            {
                context.Assets.Add(SamsungGalaxyS20Asset);
            }
        }       

        MobilePhone iPhone12 = new MobilePhone()
        {
            Brand = "iPhone",
            Model = "12",
        };
        if (mobilePhones.Any(m => m.Model == iPhone12.Model && m.Brand == iPhone12.Brand)) { }
        else
        {
            context.MobilePhones.Add(iPhone12);
            context.SaveChanges();

            Asset iPhone12Asset = new Asset()
            {
                Type = "MobilePhone",
                MobilePhoneId = iPhone12.MobilePhoneId,
                Price = 799,
                OfficeId = 1,
                PurchaseDate = Convert.ToDateTime("2022-05-12")
            };
            if (assets.Any(a => a.MobilePhoneId == iPhone12.MobilePhoneId)) { }
            else
            {
                context.Assets.Add(iPhone12Asset);
            }
        }        

        MobilePhone GooglePixel6a = new MobilePhone()
        {
            Brand = "Google",
            Model = "Pixel 6a",
        };
        if (mobilePhones.Any(m => m.Model == GooglePixel6a.Model && m.Brand == GooglePixel6a.Brand)) { }
        else
        {
            context.MobilePhones.Add(GooglePixel6a);
            context.SaveChanges();

            Asset GooglePixel6aAsset = new Asset()
            {
                Type = "MobilePhone",
                MobilePhoneId = GooglePixel6a.MobilePhoneId,
                Price = 499,
                OfficeId = 2,
                PurchaseDate = Convert.ToDateTime("2021-12-28")
            };
            if (assets.Any(a => a.MobilePhoneId == GooglePixel6a.MobilePhoneId)) { }
            else
            {
                context.Assets.Add(GooglePixel6aAsset);
            }
        }

        MobilePhone NokiaG21 = new MobilePhone()
        {
            Brand = "Nokia",
            Model = "G21",
        };
        if (mobilePhones.Any(m => m.Model == NokiaG21.Model && m.Brand == NokiaG21.Brand)) { }
        else
        {
            context.MobilePhones.Add(NokiaG21);
            context.SaveChanges();

            Asset NokiaG21Asset = new Asset()
            {
                Type = "MobilePhone",
                MobilePhoneId = NokiaG21.MobilePhoneId,
                Price = 199,
                OfficeId = 1,
                PurchaseDate = Convert.ToDateTime("2020-10-04")
            };
            if (assets.Any(a => a.MobilePhoneId == NokiaG21.MobilePhoneId)) { }
            else
            {
                context.Assets.Add(NokiaG21Asset);
            }
        }

        MobilePhone iPhone8 = new MobilePhone()
        {
            Brand = "iPhone",
            Model = "8",
        };
        if (mobilePhones.Any(m => m.Model == iPhone8.Model && m.Brand == iPhone8.Brand)) { }
        else
        {
            context.MobilePhones.Add(iPhone8);
            context.SaveChanges();

            Asset iPhone8Asset = new Asset()
            {
                Type = "MobilePhone",
                MobilePhoneId = iPhone8.MobilePhoneId,
                Price = 7499,
                OfficeId = 3,
                PurchaseDate = Convert.ToDateTime("2018-01-09")
            };
            if (assets.Any(a => a.MobilePhoneId == iPhone8.MobilePhoneId)) { }
            else
            {
                context.Assets.Add(iPhone8Asset);
            }
        }

        MobilePhone MotoroloMotoG60s = new MobilePhone()
        {
            Brand = "Motorola",
            Model = "Moto G60s",
        };
        if (mobilePhones.Any(m => m.Model == MotoroloMotoG60s.Model && m.Brand == MotoroloMotoG60s.Brand)) { }
        else
        {
            context.MobilePhones.Add(MotoroloMotoG60s);
            context.SaveChanges();

            Asset MotoroloMotoG60sAsset = new Asset()
            {
                Type = "MobilePhone",
                MobilePhoneId = MotoroloMotoG60s.MobilePhoneId,
                Price = 2499,
                OfficeId = 3,
                PurchaseDate = Convert.ToDateTime("2020-06-19")
            };
            if (assets.Any(a => a.MobilePhoneId == MotoroloMotoG60s.MobilePhoneId)) { }
            else
            {
                context.Assets.Add(MotoroloMotoG60sAsset);
            }
        }

        // Adding Laptops and assets if they don't already exist  
        Laptop Lenovo700 = new Laptop()
        {
            Brand = "Lenovo",
            Model = "700",
        }; 
        if (laptops.Any(l => l.Model == Lenovo700.Model && l.Brand == Lenovo700.Brand)) { }
        else
        {
            context.Laptops.Add(Lenovo700);
            context.SaveChanges();

            Asset Lenovo700Asset = new Asset()
            {
                Type = "Laptop",
                LaptopId = Lenovo700.LaptopId,
                Price = 599,
                OfficeId = 2,
                PurchaseDate = Convert.ToDateTime("2019-03-19")
            };
            if (assets.Any(a => a.LaptopId == Lenovo700.LaptopId)) { }
            else
            {
                context.Assets.Add(Lenovo700Asset);
                context.SaveChanges();
            }
        }

        Laptop MacBookAir = new Laptop()
        {            
            Brand = "MacBook",
            Model = "Air",
        };
        if (laptops.Any(l => l.Model == MacBookAir.Model && l.Brand == MacBookAir.Brand)) { }
        else
        {
            context.Laptops.Add(MacBookAir);
            context.SaveChanges();

            Asset MacBookAirAsset = new Asset()
            {
                Type = "Laptop",
                LaptopId = MacBookAir.LaptopId,
                Price = 1599,
                OfficeId = 1,
                PurchaseDate = Convert.ToDateTime("2022-04-11")
            };
            if (assets.Any(a => a.LaptopId == MacBookAir.LaptopId)) { }
            else
            {
                context.Assets.Add(MacBookAirAsset);
                context.SaveChanges();            }
        }

        Laptop HPPavilion15 = new Laptop()
        {
            Brand = "HP",
            Model = "Pavilion 15",
        };
        if (laptops.Any(l => l.Model == HPPavilion15.Model && l.Brand == HPPavilion15.Brand)) { }
        else
        {
            context.Laptops.Add(HPPavilion15);
            context.SaveChanges();

            Asset HPPavilion15Asset = new Asset()
            {
                Type = "Laptop",
                LaptopId = HPPavilion15.LaptopId,
                Price = 7490,
                OfficeId = 3,
                PurchaseDate = Convert.ToDateTime("2020-01-17")
            };
            if (assets.Any(a => a.LaptopId == HPPavilion15.LaptopId)) { }
            else
            {
                context.Assets.Add(HPPavilion15Asset);
                context.SaveChanges();
            }
        }

        Laptop AcerAspire2 = new Laptop()
        {
            Brand = "Acer",
            Model = "Aspire2",
        };
        if (laptops.Any(l => l.Model == AcerAspire2.Model && l.Brand == AcerAspire2.Brand)) { }
        else
        {
            context.Laptops.Add(AcerAspire2);
            context.SaveChanges();

            Asset AcerAspire2Asset = new Asset()
            {
                Type = "Laptop",
                LaptopId = AcerAspire2.LaptopId,
                Price = 499,
                OfficeId = 2,
                PurchaseDate = Convert.ToDateTime("2020-04-02")
            };
            if (assets.Any(a => a.LaptopId == AcerAspire2.LaptopId)) { }
            else
            {
                context.Assets.Add(AcerAspire2Asset);
                context.SaveChanges();
            }
        }

        Laptop AsusZenbookProDuo = new Laptop()
        {
            Brand = "Asus",
            Model = "Zenbook Pro Duo",
        };
        if (laptops.Any(l => l.Model == AsusZenbookProDuo.Model && l.Brand == AsusZenbookProDuo.Brand)) { }
        else
        {
            context.Laptops.Add(AsusZenbookProDuo);
            context.SaveChanges();

            Asset AsusZenbookProDuoAsset = new Asset()
            {
                Type = "Laptop",
                LaptopId = AsusZenbookProDuo.LaptopId,
                Price = 3199,
                OfficeId = 2,
                PurchaseDate = Convert.ToDateTime("2022-07-05")
            };
            if (assets.Any(a => a.LaptopId == AsusZenbookProDuo.LaptopId)) { }
            else
            {
                context.Assets.Add(AsusZenbookProDuoAsset);
                context.SaveChanges();
            }
        }
    }

    // Check for existing assets if user types 'n' or 'no'
    else if(input.Trim().ToLower() == "n" || input.Trim().ToLower() == "no") 
    {
        CheckForAssets();        
    }
    
    // Print error and restart PopulateDbOption-method
    else
    {
        Console.WriteLine("Wrong input, answer must 'Y' , 'Yes' , 'N' or 'No'. ");
        Console.WriteLine();
        PopulateDbOption(context);
    }
}

// Method to check for existing assets
void CheckForAssets()
{
    var assets = context.Assets
        .Where(a => a != null)
        .OrderBy(a => a.Type);

    var mobilePhones = context.MobilePhones
        .Where(m => m != null);

    var laptops = context.Laptops
        .Where(l => l != null);

    // Check if database contains assets
    if (assets.Count() != 0)
    {
        // Prompt user to keep or delete existing assets
        Console.WriteLine(assets.Count() + " assets were found in database");
        Console.WriteLine("If you would like to KEEP existing assets, type 'Y' or 'Yes'");
        Console.WriteLine("If you would like to DELETE existing assets, type 'Delete'");
        string input = Console.ReadLine();
        Console.WriteLine();

        // Do nothing/exit method if user types 'Y' or 'Yes'
        if (input.Trim().ToLower() == "y" || input.Trim().ToLower() == "yes") { }

        // Delete existing assets from database if user types delete
        else if (input.Trim().ToLower() == "delete")
        {            
            foreach (Asset a in assets)
            {
                context.Remove(a);
            }
            foreach (MobilePhone m in mobilePhones)
            {
                context.Remove(m);
            }
            foreach (Laptop l in laptops)
            {
                context.Remove(l);
            }
            context.SaveChanges();
        }

        // Print error + re-launch CheckForAssets on wrong input
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Wrong input, try again");
            Console.ResetColor();
            CheckForAssets();
        }
    }
}

// Method to create new assets
void CreateAsset(Asset? edit = null)
{
    while (true)
    {
        // Initiating variables
        string type = "";
        string brand = "";
        string model = "";
        int office = 0;
        DateTime purchaseDate = new();
        int price = 0;
        int priceInUSD = 0;
        string currency = "";
        double USDtoEUR = (priceInUSD * 1.03);
        double USDtoSEK = (priceInUSD * 11.34);

        string previousType = "";
        string previousBrand = "";
        string previousModel = "";
        int previousOffice = 0;
        DateTime previousPurchaseDate = new();
        int previousPrice = 0;

        // On edit: Set values from selected asset to edit
        if (edit != null)
        {
            type = edit.Type;
            office = edit.OfficeId;
            purchaseDate = edit.PurchaseDate;
            currency = edit.Office.Currency;
            price = edit.Price;
            if (edit.Type == "MobilePhone") 
            { 
                brand = edit.MobilePhone.Brand;
                model = edit.MobilePhone.Model;
            }
            else if (edit.Type == "Laptop")
            {
                brand = edit.Laptop.Brand;
                model = edit.Laptop.Model;
            }

            // Select what should be edited 
            if (type != "" && brand != "" && office != 0 && purchaseDate != DateTime.MinValue && price != 0 && currency != "")
            {
                Console.WriteLine("Select what to edit by entering the corresponding number:");
                Exit();
                Console.WriteLine("1. The asset's Type");
                Console.WriteLine("2. The asset's Brand");
                Console.WriteLine("3. The asset's Model");
                Console.WriteLine("4. The asset's Office");
                Console.WriteLine("5. The asset's Purchase Date");
                Console.WriteLine("6. The asset's Price");

                string input = Console.ReadLine();
                Console.WriteLine();

                // Reset values to edit and set previous values 
                switch (input.Trim().ToLower())
                {
                    case "e":
                    case "exit":
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine("Edits saved!");
                        Console.ResetColor();
                        Main();
                        break;

                    case "1":
                        previousType = type;
                        type = "";
                        break;

                    case "2":
                        previousBrand = brand;
                        brand = "";
                        break;

                    case "3":
                        previousModel = model;
                        model = "";
                        break;

                    case "4":
                        previousOffice = office;
                        office = 0;
                        break;

                    case "5":
                        previousPurchaseDate = purchaseDate;
                        purchaseDate = new();
                        break;

                    case "6":
                        previousPrice = price;
                        price = 0;
                        break;
                }
            }
        }

        // Check if type is set
        if (type.Trim() == "")
        {
            Console.WriteLine("Select what type of asset you would like to add:");
            Console.WriteLine("1. Phone");
            Console.WriteLine("2. Laptop");
            Console.WriteLine();

            // Print previous type from edit
            if (edit != null)
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;                
                Console.WriteLine("Previous type: " + previousType);
                Console.ResetColor();
            }
            
            Exit(); // Method to print exit instructions

            string input = Console.ReadLine();
            Console.WriteLine();

            // Exit functionallity
            if (input.ToLower().Trim() == "exit" || input.ToLower().Trim() == "e")
            {
                break;
            }

            // Set type from user input
            else if (input.ToLower().Trim() == "phone" || input.ToLower().Trim() == "p" || input.ToLower().Trim() == "1")
            {
                type = "Phone";
            }
            else if (input.ToLower().Trim() == "laptop" || input.ToLower().Trim() == "l" || input.ToLower().Trim() == "2")
            {
                type = "Laptop";
            }
            // Error handling
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wrong input");
                Console.ResetColor();
                Console.WriteLine();
            }
        }

        // Check if brand is set
        if (brand.Trim() == "" && type != "")
        {
            // Add new phone
            if (type == "Phone")
            {
                Console.WriteLine("Enter the number corresponding to the brand of the phone you would like to add. Or press enter to write the name manually");
                Console.WriteLine("1. iPhone");
                Console.WriteLine("2. Motorola");
                Console.WriteLine("3. Samsung");
                Console.WriteLine("4. Nokia");
                Console.WriteLine("5. Google");
                
                // Print previous brand from edit
                if (edit != null)
                {
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;                    
                    Console.WriteLine("Previous brand: " + previousBrand);
                    Console.ResetColor();
                }

                Exit(); // Method to print exit instructions

                string input = Console.ReadLine();
                Console.WriteLine();

                switch (input.ToLower().Trim())
                {
                    case "e":
                    case "exit":
                        break;
                    case "1":
                        brand = "iPhone";
                        break;
                    case "2":
                        brand = "Motorola";
                        break;
                    case "3":
                        brand = "Samsung";
                        break;
                    case "4":
                        brand = "Nokia";
                        break;
                    case "5":
                        brand = "Google";
                        break;

                    // Prompt user for manual input
                    default:
                        Console.WriteLine("Please enter the name of the brand:");
                        input = Console.ReadLine();
                        Console.WriteLine();

                        // Error handling
                        if (input.Trim() == "")
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("You need to enter a brand.");
                            Console.ResetColor();
                            Console.WriteLine();
                            break;
                        }

                        // Set brand from input
                        else
                        {
                            brand = input;
                            break;
                        }
                }
            }

            // Add new Laptop
            else if (type == "Laptop")
            {
                Console.WriteLine("Enter the number corresponding to the brand of the Laptop you would like to add. Or press enter to write the name manually");
                Console.WriteLine("1. MacBook");
                Console.WriteLine("2. Asus");
                Console.WriteLine("3. Lenovo");
                Console.WriteLine("4. HP");
                Console.WriteLine("5. Acer");

                // Set and print previous brand from edit
                if (edit != null)
                {
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    
                    Console.WriteLine("Previous brand: " + brand);
                    Console.ResetColor();
                }

                Exit(); // Method to print exit instructions
                string input = Console.ReadLine();
                Console.WriteLine();

                // Set brand from options or manually
                switch (input.ToLower().Trim())
                {
                    // Exit functionallity
                    case "e":
                    case "exit":
                        break;
                    case "1":
                        brand = "MacBook";
                        break;
                    case "2":
                        brand = "Asus";
                        break;
                    case "3":
                        brand = "Lenovo";
                        break;
                    case "4":
                        brand = "HP";
                        break;
                    case "5":
                        brand = "Acer";
                        break;

                    // Prompt user for manual input
                    default:
                        Console.WriteLine("Please enter the name of the brand:");
                        input = Console.ReadLine();
                        Console.WriteLine();

                        // Error handling
                        if (input.Trim() == "")
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("You need to enter a brand.");
                            Console.ResetColor();
                            Console.WriteLine();
                            break;
                        }

                        // Set brand from input
                        else
                        {
                            brand = input;
                            break;
                        }
                }
            }
        }

        // Check if model, type and brand is set
        if (model.Trim() == "" && type != "" && brand != "")
        {
            Console.WriteLine("Enter the " + brand + "'s model:");

            // Print previous model from edit
            if (edit != null)
            {                                
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("Previous model: " + previousModel);
                Console.ResetColor();
            }

            Exit(); // Method to print exit instructions

            string input = Console.ReadLine();
            Console.WriteLine();

            // Exit functionallity
            if (input.ToLower().Trim() == "exit" || input.ToLower().Trim() == "e")
            {
                break;
            }

            // Error handling
            else if (input.Trim() == "")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You need to enter a model.");
                Console.ResetColor();
                Console.WriteLine();
            }

            // Set model from input
            else
            {
                model = input;
            }
        }

        // Check if office, type, brand and model is set
        if (office == 0 && type != "" && brand != "" && model != "")
        {
            Console.WriteLine("Enter the number corresponding to the office the asset belongs to:");
            Console.WriteLine("1. USA");
            Console.WriteLine("2. Spain");
            Console.WriteLine("3. Sweden");

            // Print previous office from edit
            if (edit != null)
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;                
                if (previousOffice == 1)
                {
                    Console.WriteLine("Previous office: USA");
                }
                else if (previousOffice == 2)
                {
                    Console.WriteLine("Previous office: Spain");
                }
                else if (previousOffice == 3)
                {
                    Console.WriteLine("Previous office: Sweden");
                }
                Console.ResetColor();
            }

            Exit(); // Method to print exit instructions
            string input = Console.ReadLine();
            Console.WriteLine();

            // Set office and currency from user input
            switch (input.ToLower().Trim())
            {
                // Exit functionallity
                case "e":
                case "exit":
                    break;
                case "1":
                    office = 1;
                    currency = "USD";
                    break;
                case "2":
                    office = 2;
                    currency = "EUR";
                    break;
                case "3":
                    office = 3;
                    currency = "SEK";
                    break;

                // Error handling
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Wrong input, you must enter a number between 1-3 corresponding to the correct office.");
                    Console.ResetColor();
                    Console.WriteLine();
                    break;
            }
        }

        // Check if purchase date, type, model and office is set
        if (purchaseDate == DateTime.MinValue && type != "" && model != "" && office != 0)
        {
            int year = 0;
            int month = 0;
            int day = 0;

            // Print previous office from edit
            if (edit != null)
            {                
                // Set and print previous purchase date                
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("Previous purchase date: " + previousPurchaseDate.ToString());
                Console.ResetColor();
            }
            // Set year from user input + error handling
            while (year == 0)
            {
                Console.WriteLine("Enter the year the asset was purchased:"); ;
                Exit(); // Method to print exit instructions
                string input = Console.ReadLine();
                Console.WriteLine();

                // Exit functionallity
                if (input.ToLower().Trim() == "exit" || input.ToLower().Trim() == "e")
                {
                    break;
                }
                else if (input.Trim().Length == 4)
                {
                    if (int.TryParse(input, out year)) { }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Wrong input, year must be written with numbers");
                        Console.ResetColor();
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Wrong input, year must be written with 4 single-digit numbers");
                    Console.ResetColor();
                    Console.WriteLine();
                }
            }

            // Set month from user input + error handling
            while (month == 0)
            {
                Console.WriteLine("Enter the month the asset was purchased:");
                Exit(); // Method to print exit instructions

                string input = Console.ReadLine();
                Console.WriteLine();

                // Exit functionallity
                if (input.ToLower().Trim() == "exit" || input.ToLower().Trim() == "e")
                {
                    break;
                }
                else if (input.Trim().Length == 2)
                {
                    if (int.TryParse(input.Trim(), out month)) { }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Wrong input, month must be written with numbers");
                        Console.ResetColor();
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Wrong input, month must be written with two single-digit numbers");
                    Console.ResetColor();
                    Console.WriteLine();
                }
            }

            // Set day from user input + error handling
            while (day == 0)
            {
                Console.WriteLine("Enter the day the asset was purchased:");
                Exit(); // Method to print exit instructions
                string input = Console.ReadLine();
                Console.WriteLine();

                // Exit functionallity
                if (input.ToLower().Trim() == "exit" || input.ToLower().Trim() == "e")
                {
                    break;
                }
                else if (input.Trim().Length == 2)
                {
                    if (int.TryParse(input.Trim(), out day)) { }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Wrong input, day must be written with numbers");
                        Console.ResetColor();
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Wrong input, day must be written with two single-digit numbers");
                    Console.ResetColor();
                    Console.WriteLine();
                }
            }

            // Create datetime from user day, month & year + error handling
            try
            {
                string purchaseD = year.ToString() + "-" + month.ToString() + "-" + day.ToString();
                purchaseDate = Convert.ToDateTime(purchaseD);
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wrong input, the purchase date is not valid.");
                Console.ResetColor();
                Console.WriteLine();
            }

        }

        // Check if price is set
        if (price == 0 && type != "" && brand != "" && model != "" && office != 0)
        {
            Console.WriteLine("Enter the assets purchase price in USD");

            // Print previous price from edit
            if (edit != null)
            {                
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("Previous price: " + previousPrice);
                Console.ResetColor();
            }            

            Exit(); // Method to print exit instructions

            string input = Console.ReadLine();
            Console.WriteLine();

            // Exit functionallity
            if (input.ToLower().Trim() == "exit" || input.ToLower().Trim() == "e")
            {
                break;
            }

            // Set price based on local currency or print error
            else if (int.TryParse(input.Trim(), out priceInUSD))
            {
                if (office == 1)
                {
                    price = priceInUSD;
                }
                else if (office == 2)
                {
                    price = (int)USDtoEUR;
                }
                else if (office == 3)
                {
                    price = (int)USDtoSEK;
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wrong input, price must be written with numbers");
                Console.ResetColor();
                Console.WriteLine();
            }
        }

        // Save changes to the edited asset
        if (edit != null)
        {
            edit.Type = type;
            edit.OfficeId = office;
            edit.PurchaseDate = purchaseDate;
            edit.Price = price;
            if (edit.Type == "MobilePhone")
            {
                edit.MobilePhone.Brand = brand;
                edit.MobilePhone.Model = model;
            }
            else if (edit.Type == "Laptop")
            {
                edit.Laptop.Brand = brand;
                edit.Laptop.Model = model;
            }
            context.SaveChanges();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine("Asset edited!");
            Console.WriteLine();
            Console.ResetColor();
            Console.WriteLine("If you are finished editing type 'E' or 'Exit' to Save Changes and Exit");
        }

        // Create asset if all values are set
        else
        {            
            if (type != "" && brand != "" && office != 0 && purchaseDate != DateTime.MinValue && price != 0 && currency != "")
            {
                // Create new mobile phone
                if (type == "Phone")
                {
                    MobilePhone mobilePhone = new MobilePhone()
                    {
                        Brand = brand,
                        Model = model
                    };
                    context.MobilePhones.Add(mobilePhone);
                    context.SaveChanges();

                    // Create asset from mobile phone
                    Asset asset = new Asset()
                    {
                        Type = "MobilePhone",
                        MobilePhoneId = mobilePhone.MobilePhoneId,
                        Price = price,
                        OfficeId = office,
                        PurchaseDate = purchaseDate
                    };
                    context.Assets.Add(asset);
                    context.SaveChanges();
                }

                // Create new laptop
                else if (type == "Laptop")
                {
                    Laptop laptop = new Laptop()
                    {
                        Brand = brand,
                        Model = model
                    };
                    context.Laptops.Add(laptop);
                    context.SaveChanges();

                    // Create asset from laptop
                    Asset asset = new Asset()
                    {
                        Type = "Laptop",
                        LaptopId = laptop.LaptopId,
                        Price = price,
                        OfficeId = office,
                        PurchaseDate = purchaseDate
                    };
                    context.Assets.Add(asset);
                    context.SaveChanges();
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine();
                Console.WriteLine("Asset created!");
                Console.WriteLine();
                Console.ResetColor();
            }
        }        
    }
    Main();
}

// Method to edit or delete assets
void EditAsset(Boolean? printed = false, string? passedAction = null)
{
    string action = "";
    if (passedAction != null)
    {
        action = passedAction;
    }
    string input;

    if (action == "")
    {
        Console.WriteLine("Select and option by typing the corresponding number:");
        Exit();
        Console.WriteLine("1. Edit existing asset");
        Console.WriteLine("2. Delete existing asset");
        input = Console.ReadLine();
        Console.WriteLine();

        // Edit functionality
        if (input.Trim().ToLower() == "1" || input.Trim().ToLower() == "edit")
        {
            action = "EDIT";

            // Print assets if not already done so
            if (printed == false)
            {
                PrintAssets(true);
            }
            
        }

        // Delete functionality
        else if (input.Trim().ToLower() == "2" || input.Trim().ToLower() == "delete")
        {
            action = "DELETE";

            // Print assets if not already done so
            if (printed == false)
            {
                PrintAssets(false);
            }
        }

        // Exit to main menu if user types 'E' or 'Exit'
        else if (input.ToLower().Trim() == "exit" || input.ToLower().Trim() == "e")
        {
            Main();
        }

        // Print error and restart EditAsset
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Wrong input");
            Console.ResetColor();
            EditAsset(false, null);
        }
    }

    
    
    
    Console.WriteLine("Type the ID of the asset you would like to " + action);
    input = Console.ReadLine();
    Console.WriteLine();

    int inputId;

    // Match input to asset id
    if (int.TryParse(input.Trim(), out inputId))
    {
        Asset? asset = null;       
        MobilePhone? mobile = null;
        Laptop? laptop = null;

        asset = context.Assets
            .Where(a => a.Id == inputId)
            .FirstOrDefault();

        // Check if selected asset exists, print asset and call CreateAsset to edit
        if (asset != null)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("ID:".PadRight(10) + "Type:".PadRight(15) + "Brand:".PadRight(15) + "Model:".PadRight(20) + "Location:".PadRight(10) + "PurchaseDate:".PadRight(15) + "Price:".ToString().PadRight(15) + "Currency:");
            Console.ResetColor();

            if (asset.Type == "MobilePhone")
            {
                // Get mobile phone from db
                mobile = context.MobilePhones
                    .Where(m => m.MobilePhoneId == asset.MobilePhoneId)
                    .FirstOrDefault();

                // Print mobile phone data
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(asset.Id.ToString().PadRight(10) + asset.Type.PadRight(15) + asset.MobilePhone.Brand.PadRight(15) + asset.MobilePhone.Model.PadRight(20) + asset.Office.Location.PadRight(10) + asset.PurchaseDate.ToString("MM/dd/yyyy").PadRight(15) + asset.Price.ToString().PadRight(15) + asset.Office.Currency.PadRight(9));
                Console.ResetColor();
                Console.WriteLine();
            }
            else if (asset.Type == "Laptop")
            {
                // Get laptop from db
                laptop = context.Laptops
                    .Where(l => l.LaptopId == asset.LaptopId)
                    .FirstOrDefault();

                // Print laptop data
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(asset.Id.ToString().PadRight(10) + asset.Type.PadRight(15) + asset.Laptop.Brand.PadRight(20) + asset.Laptop.Model.PadRight(20) + asset.Office.Location.PadRight(10) + asset.PurchaseDate.ToString("MM/dd/yyyy").PadRight(15) + asset.Price.ToString().PadRight(15) + asset.Office.Currency.PadRight(9));
                Console.ResetColor();
                Console.WriteLine();
            }
            if (action == "EDIT")
            {
                Console.WriteLine("To EDIT the selected asset type 'Y' or 'Yes'");
                Console.WriteLine("To go back and choose another asset to edit or delete type 'N' or 'No'");
                Exit();
                input = Console.ReadLine();

                if (input.Trim().ToLower() == "y" || input.Trim().ToLower() == "yes")
                {
                    CreateAsset(asset);
                }
                else if (input.Trim().ToLower() == "n" || input.Trim().ToLower() == "no")
                {
                    EditAsset();
                }
                else if (input.Trim().ToLower() == "e" || input.Trim().ToLower() == "exit")
                {
                    Main();
                }
            }
            else if (action == "DELETE")
            {
                Console.WriteLine("To DELETE the selected asset type 'Y' or 'Yes'");
                Console.WriteLine("To go back and choose another asset to edit or delete type 'N' or 'No'");
                Exit();
                input = Console.ReadLine();

                if (input.Trim().ToLower() == "y" || input.Trim().ToLower() == "yes")
                {
                    // Remove asset and mobile/latop
                    if (asset.Type == "MobilePhone")
                    {
                        context.Remove(mobile);
                    }
                    else if( asset.Type == "Laptop")
                    {
                        context.Remove(laptop);
                    }
                    context.Remove(asset);
                    
                    // Save changes
                    context.SaveChanges();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine();
                    Console.WriteLine("Asset deleted!");
                    Console.WriteLine();
                    Console.ResetColor();
                    EditAsset(true, null);
                }
                else if (input.Trim().ToLower() == "n" || input.Trim().ToLower() == "no")
                {
                    EditAsset();
                }
                else if (input.Trim().ToLower() == "e" || input.Trim().ToLower() == "exit")
                {
                    Main();
                }
            }            
        }

        // Re-launch method on wrong input
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("No asset was found with the selected ID, try again");
            Console.ResetColor();
            Console.WriteLine();
            EditAsset();
        }
    }    

    // Re-launch method on wrong input
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Wrong input");
        Console.ResetColor();
        Console.WriteLine();
        EditAsset();
    }
}

// Method to print assets
void PrintAssets(Boolean? edit = null)
{
    // Get assets from database
    var assets = context.Assets.AsQueryable()
        .Where(a => a != null)
        .OrderBy(a => a.Office)
        .ThenBy(a => a.PurchaseDate); 

    var mobilePhones = context.MobilePhones.AsQueryable()
        .Where(m => m != null);

    var laptops = context.Laptops.AsQueryable()
        .Where(l => l != null);

    var offices = context.Offices.AsQueryable()
        .Where(l => l != null);      

    // Print if assets exist
    if (assets.Count() > 0)
    {
        // Print each asset
        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine("ID:".PadRight(10) + "Type:".PadRight(15) + "Brand:".PadRight(15) + "Model:".PadRight(20) + "Location:".PadRight(10) + "PurchaseDate:".PadRight(15) + "Price:".ToString().PadRight(15) + "Currency:");
        Console.ResetColor();

        foreach (Asset a in assets)
        {
            int Id = a.Id;
            string Type = a.Type;
            int Price = a.Price;
            int OfficeId = a.OfficeId;
            DateTime PurchaseDate = a.PurchaseDate;
            List<Office> officeList = offices.Where(o => o.OfficeId == OfficeId).ToList();
            Office Office = officeList.First();
            string Location = Office.Location;
            string Currency = Office.Currency;

            if (a.Type == "MobilePhone")
            {
                // Get data for mobile phone
                int mobilePhoneId = (int)a.MobilePhoneId;
                List<MobilePhone> mobilePhoneList = mobilePhones.Where(m => m.MobilePhoneId == mobilePhoneId).ToList();
                if (mobilePhoneList.Count == 1)
                {
                    MobilePhone mobilePhone = mobilePhoneList.First();
                    
                    string Brand = mobilePhone.Brand;
                    string Model = mobilePhone.Model;

                    // Print product in red if purchase date is within 3 months from being 3 years old
                    if (PurchaseDate.AddMonths(-3) < DateTime.Now.AddYears(-3))
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine(Id.ToString().PadRight(10) + Type.PadRight(15) + Brand.PadRight(15) + Model.PadRight(20) + Location.PadRight(10) + PurchaseDate.ToString("MM/dd/yyyy").PadRight(15) + Price.ToString().PadRight(15) + Currency.PadRight(9));
                        Console.ResetColor();
                    }

                    // Print product in yellow if purchase date is within 6 months from being 3 years old
                    else if (PurchaseDate.AddMonths(-6) < DateTime.Now.AddYears(-3))
                    {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine(Id.ToString().PadRight(10) + Type.PadRight(15) + Brand.PadRight(15) + Model.PadRight(20) + Location.PadRight(10) + PurchaseDate.ToString("MM/dd/yyyy").PadRight(15) + Price.ToString().PadRight(15) + Currency.PadRight(9));
                        Console.ResetColor();
                    }

                    // Print product
                    else
                    {
                        Console.WriteLine(Id.ToString().PadRight(10) + Type.PadRight(15) + Brand.PadRight(15) + Model.PadRight(20) + Location.PadRight(10) + PurchaseDate.ToString("MM/dd/yyyy").PadRight(15) + Price.ToString().PadRight(15) + Currency);
                    }
                }
                else
                {
                    Console.WriteLine("Something went wrong. Count = " + mobilePhoneList.Count());
                }
            }
            else if (a.Type == "Laptop")
            {
                // Get data for mobile phone
                int laptopId = (int)a.LaptopId;
                List<Laptop> laptopList = laptops.Where(m => m.LaptopId == laptopId).ToList();
                if (laptopList.Count == 1)
                {
                    Laptop laptop = laptopList.First();
                    string Brand = laptop.Brand;
                    string Model = laptop.Model;

                    // Print product in red if purchase date is within 3 months from being 3 years old
                    if (PurchaseDate.AddMonths(-3) < DateTime.Now.AddYears(-3))
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine(Id.ToString().PadRight(10) + Type.PadRight(15) + Brand.PadRight(15) + Model.PadRight(20) + Location.PadRight(10) + PurchaseDate.ToString("MM/dd/yyyy").PadRight(15) + Price.ToString().PadRight(15) + Currency.PadRight(9));
                        Console.ResetColor();
                    }

                    // Print product in yellow if purchase date is within 6 months from being 3 years old
                    else if (PurchaseDate.AddMonths(-6) < DateTime.Now.AddYears(-3))
                    {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine(Id.ToString().PadRight(10) + Type.PadRight(15) + Brand.PadRight(15) + Model.PadRight(20) + Location.PadRight(10) + PurchaseDate.ToString("MM/dd/yyyy").PadRight(15) + Price.ToString().PadRight(15) + Currency.PadRight(9));
                        Console.ResetColor();
                    }

                    // Print product
                    else
                    {
                        Console.WriteLine(Id.ToString().PadRight(10) + Type.PadRight(15) + Brand.PadRight(15) + Model.PadRight(20) + Location.PadRight(10) + PurchaseDate.ToString("MM/dd/yyyy").PadRight(15) + Price.ToString().PadRight(15) + Currency);
                    }
                }
                else
                {
                    Console.WriteLine("Something went wrong. Count = " + laptopList.Count());
                }
            }
            else
            {
                Console.WriteLine("No assets were found");
            }
        }
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine();
    }
    else
    {
        Console.WriteLine();
        Console.WriteLine("No assets found, try adding some!");
        Console.WriteLine();
    }

    // Return to EditAsset with edit-action
    if (edit == true)
    {
        EditAsset(true, "EDIT");
    }

    // Return to EditAsset with delete-action
    else if (edit == false)
    {
        EditAsset(true, "DELETE");
    }

    // Go back to main menu
    else
    {        
        Main();
    }

    
}

// Print exit instructions to user
void Exit()
{
    Console.ForegroundColor = ConsoleColor.DarkGray;
    Console.WriteLine("Enter 'E' or 'EXIT' in order to exit to main menu.");
    Console.ResetColor();
}
