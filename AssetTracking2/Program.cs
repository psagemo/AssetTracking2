
using AssetTracking2.Models;
using AssetTracking2.Data;
using System.Globalization;
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


TODO-list:
    X Start TODO-list
    - Add-to-db functionallity
    - Read-from-db functionallity 
    - Print-Table functionality
    - Set price
    - Create if statments to check if offices already exists and if db is already populated
    - Finish TODO-list
 
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

// Saving context
context.SaveChanges();



// Launching program
Main();

void Main()
{
    while (true)
    {
        // Initiating variables
        string type = "";
        string brand = "";
        string model = "";
        string office = "";
        DateTime purchaseDate = new();
        int USD = 0;
        string currency = "";
        double localPriceToday = 0;
        double EUR = 0.99;
        double SEK = 10.46;


        // Check if type is set
        if (type.Trim() == "")
        {
            Console.WriteLine("Select what type of asset you would like to add:");
            Console.WriteLine("1. Phone");
            Console.WriteLine("2. Computer");
            Console.WriteLine();
            Exit(); // Method to print exit instructions

            string input = Console.ReadLine();

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
            else if (input.ToLower().Trim() == "computer" || input.ToLower().Trim() == "c" || input.ToLower().Trim() == "2")
            {
                type = "Computer";
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
            // Samsung iPhone Google Nokia Motorola
            if (type == "Phone")
            {              
                Console.WriteLine("Enter the number corresponding to the brand of the phone you would like to add. Or press enter to write the name manually");
                Console.WriteLine("1. iPhone");
                Console.WriteLine("2. Motorola");
                Console.WriteLine("3. Samsung");
                Console.WriteLine("4. Nokia");
                Console.WriteLine("5. Google");
                Exit(); // Method to print exit instructions

                string input = Console.ReadLine();

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

            // Add new computer
            else if (type == "Computer")
            {
                Console.WriteLine("Enter the number corresponding to the brand of the computer you would like to add. Or press enter to write the name manually");
                Console.WriteLine("1. MacBook");
                Console.WriteLine("2. Asus");
                Console.WriteLine("3. Lenovo");
                Console.WriteLine("4. HP");
                Console.WriteLine("5. Acer");
                Exit(); // Method to print exit instructions
                string input = Console.ReadLine();                             

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
            Exit(); // Method to print exit instructions

            string input = Console.ReadLine();

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
        if (office == "" && type != "" && brand != "" && model != "")
        {
            Console.WriteLine("Enter the number corresponding to the office the asset belongs to:");
            Console.WriteLine("1. USA");
            Console.WriteLine("2. Spain");
            Console.WriteLine("3. Sweden");
            Exit(); // Method to print exit instructions
            string input = Console.ReadLine();

            // Set office and currency from user input
            switch (input.ToLower().Trim())
            {
                // Exit functionallity
                case "e":
                case "exit":
                    break;
                case "1":
                    office = "USA";
                    currency = "USD";
                    break;
                case "2":
                    office = "Spain";
                    currency = "EUR";
                    break;
                case "3":
                    office = "Sweden";
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
        if (purchaseDate == DateTime.MinValue && type != "" && model != "" && office != "")
        {
            int year = 0;
            int month = 0;
            int day = 0;

            // Set year from user input + error handling
            while (year == 0)
            {
                Console.WriteLine("Enter the year the asset was purchased:"); ;
                Exit(); // Method to print exit instructions
                string input = Console.ReadLine();

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

        /*------------------------------------------------------------------------*
         *------------------------------------------------------------------------*
         *----------------------------------TODO----------------------------------*
         *------------------------------------------------------------------------*
         *------------------------------------------------------------------------*/


        // Create asset if all values are set

        /*------------------------------------------------------------------------*
         *------------------------------------------------------------------------*
         *----------------------------------TODO----------------------------------*
         *------------------------------------------------------------------------*
         *------------------------------------------------------------------------*/
    }
    // Print assets
    var assets = from 


}

// Method to add Offices to context
static void AddOffices(AssetTracker2Context context)
{
    /*----------- TODO -----------
        if statement to check if offices are already set
     */
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
}

static void PopulateDbOption(AssetTracker2Context context)
{
    /*----------- TODO -----------     
        if statement to check if database is already populated
     */
    Console.WriteLine("Would you like to populate the database with pre-generated assets? [Y/N]");
    string input = Console.ReadLine();
    if(input.Trim().ToLower() == "y" || input.Trim().ToLower() == "yes")
    {
        // Adding Mobilephones        
        MobilePhone SamsungGalaxyS20 = new MobilePhone()
        {
            Brand = "Samsung",
            Model = "Galaxy S20",
            Price = 4199,
            OfficeId = 3,
            PurchaseDate = Convert.ToDateTime("2020-03-30")
        };
        MobilePhone iPhone12 = new MobilePhone()
        {
            Brand = "iPhone",
            Model = "12",
            Price = 799,
            OfficeId = 1,
            PurchaseDate = Convert.ToDateTime("2022-05-12")
        };
        MobilePhone GooglePixel6a = new MobilePhone()
        {
            Brand = "Google",
            Model = "Pixel 6a",
            Price = 499,
            OfficeId = 2,
            PurchaseDate = Convert.ToDateTime("2021-12-28")
        };
        MobilePhone NokiaG21 = new MobilePhone()
        {
            Brand = "Nokia",
            Model = "G21",
            Price = 199,
            OfficeId = 1,
            PurchaseDate = Convert.ToDateTime("2020-10-04")
        };
        MobilePhone iPhone8 = new MobilePhone()
        {
            Brand = "iPhone",
            Model = "8",
            Price = 7499,
            OfficeId = 3,
            PurchaseDate = Convert.ToDateTime("2018-01-09")
        };
        MobilePhone MotoroloMotoG60s = new MobilePhone()
        {
            Brand = "Motorola",
            Model = "Moto G60s",
            Price = 2499,
            OfficeId = 3,
            PurchaseDate = Convert.ToDateTime("2020-06-19")
        };
        
        // Adding Laptops
        Laptop Lenovo700 = new Laptop()
        {
            Brand = "Lenovo",
            Model = "700",
            Price = 599,
            OfficeId = 2,
            PurchaseDate = Convert.ToDateTime("2019-03-19")
        };
        Laptop MacBookAir = new Laptop()
        {
            Brand = "MacBook",
            Model = "Air",
            Price = 1599,
            OfficeId = 1,
            PurchaseDate = Convert.ToDateTime("2022-04-11")
        };
        Laptop HPPavilion15 = new Laptop()
        {
            Brand = "HP",
            Model = "Pavilion 15",
            Price = 7490,
            OfficeId = 3,
            PurchaseDate = Convert.ToDateTime("2020-01-17")
        };
        Laptop AcerAspire2 = new Laptop()
        {
            Brand = "Acer",
            Model = "Aspire2",
            Price = 499,
            OfficeId = 2,
            PurchaseDate = Convert.ToDateTime("2020-04-02")
        };
        Laptop AsusZenbookProDuo = new Laptop()
        {
            Brand = "Asus",
            Model = "Zenbook Pro Duo",
            Price = 3199,
            OfficeId = 2,
            PurchaseDate = Convert.ToDateTime("2022-07-05")
        };
    }

    // Do nothing/exit method if user types 'n' or 'no'
    else if(input.Trim().ToLower() == "n" || input.Trim().ToLower() == "no") { }
    
    // Print error and restart PopulateDbOption-method
    else
    {
        Console.WriteLine("Wrong input, answer must 'Y' , 'Yes' , 'N' or 'No'. ");
        PopulateDbOption(context);
    }
}

void PrintProduct() // assets
{
    // Print product in red if purchase date is within 3 months from being 3 years old
    //if (PurchaseDate.AddMonths(-3) < DateTime.Now.AddYears(-3))
    //{
    //    Console.ForegroundColor = ConsoleColor.Red;
    //    Console.WriteLine(Type.PadRight(20) + Brand.PadRight(20) + Model.PadRight(20) + Office.PadRight(20) + PurchaseDate.ToString("MM/dd/yyyy").PadRight(20) + USD.ToString().PadRight(20) + Currency.PadRight(20) + LocalPriceToday + " " + "(" + Currency + ")");
    //    Console.ResetColor();
    //}

    // Print product in yellow if purchase date is within 6 months from being 3 years old
    //else if (PurchaseDate.AddMonths(-6) < DateTime.Now.AddYears(-3))
    //{
    //    Console.ForegroundColor = ConsoleColor.Yellow;
    //    Console.WriteLine(Type.PadRight(20) + Brand.PadRight(20) + Model.PadRight(20) + Office.PadRight(20) + PurchaseDate.ToString("MM/dd/yyyy").PadRight(20) + USD.ToString().PadRight(20) + Currency.PadRight(20) + LocalPriceToday + " " + "(" + Currency + ")");
    //    Console.ResetColor();
    //}

    // Print product
    //else
    //{
    //    Console.WriteLine(Type.PadRight(20) + Brand.PadRight(20) + Model.PadRight(20) + Office.PadRight(20) + PurchaseDate.ToString("MM/dd/yyyy").PadRight(20) + USD.ToString().PadRight(20) + Currency.PadRight(20) + LocalPriceToday + " " + "(" + Currency + ")");
    //}
}

// Print exit instructions to user
void Exit()
{
    Console.ForegroundColor = ConsoleColor.DarkGray;
    Console.WriteLine("Enter 'E' or 'EXIT' in order to close the program.");
    Console.ResetColor();
}
