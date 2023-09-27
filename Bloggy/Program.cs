using Bloggy;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;



//__________________________________________________________________________________________________________________________________// Connectionstring
string ConnectionString = "Server=(localdb)\\mssqllocaldb;Database=Bloggy;Trusted_Connection=True";
SqlConnection Connection = new SqlConnection(ConnectionString);

//__________________________________________________________________________________________________________________________________// Creating Tables and choosing what the values they hold Cateogry and BlogPost works but has to be commented out for the code to run once the databases are created.



SqlCommand SqlCat = new SqlCommand(
  "CREATE TABLE Category(" + "CatID INT PRIMARY KEY," + "Category VARCHAR(50))" + 
   "USE Bloggy",
    Connection);

SqlCommand SqlBlogPost = new SqlCommand(
"CREATE TABLE BlogPost(" + "BlogID INT PRIMARY KEY," + "Title VARCHAR(50)," + "Text VARCHAR(500))" +
    "USE Bloggy",
    Connection);

SqlCommand SqlBlogPostCategory = new SqlCommand(
 "CREATE TABLE BlogPostCategory("+ "BlogPostID INT FOREIGN KEY REFERENCES BlogPost(BlogID)," + "CategoryID INT FOREIGN KEY REFERENCES Category(CatID))" +
    "USE Bloggy",
     Connection);






//__________________________________________________________________________________________________________________________________// Opening and closing connections to the tables

Connection.Open(); // Opening connection to database
SqlCat.ExecuteNonQuery(); //Creating Category table
SqlBlogPost.ExecuteNonQuery(); //Creating BlogPost table
SqlBlogPostCategory.ExecuteNonQuery(); //Creating BlogPostCategory table
Connection.Close(); // Closing connection to database

bool KeepRunning = true; // creating a bool for the while loop
while (KeepRunning) // while keeprunning = true this will run.
{


    //__________________________________________________________________________________________________________________________________// Welcome screen and input of choices


    Console.WriteLine("-----------------------------");
    Console.WriteLine("");
    Console.WriteLine("Welcome to Bloggy!");
    Console.WriteLine("");
Mainmenu:                                                                                // Makes it possible to return to the main menue for new choices.
    Console.WriteLine("-----------------------------");
    Console.WriteLine("");
    Console.WriteLine("Choose one of the following options");
    Console.WriteLine("");
    Console.WriteLine("-----------------------------");
    Console.WriteLine("1. Display all posts");
    Console.WriteLine("2. Display the names of all the categories");
    Console.WriteLine("3. Add a new blog post");
    Console.WriteLine("4. Add a new category");
    Console.WriteLine("5. Display all the blog title from a category of your choice");
    Console.WriteLine("6. Add an existing blog post to an existing category");
    Console.WriteLine("7. Exit the application");
    Console.WriteLine("-----------------------------");
    Console.WriteLine("");
    Console.Write("Your choice: ");

    string Userchoice = Console.ReadLine();


    //__________________________________________________________________________________________________________________________________// Choice 1

    if (Userchoice == "1")
    {
        Console.Clear();
        Console.WriteLine("-----------------------------");
        Console.WriteLine(); Console.WriteLine("Displaying Id, Title and Text");
        Console.WriteLine("");
        string Sql = "SELECT * FROM BlogPost";
        SqlCommand Command = new SqlCommand(Sql, Connection);
        Connection.Close();
        Connection.Open();
        SqlDataReader Result = Command.ExecuteReader();
        while (Result.Read())
        {

            Console.WriteLine("-----------------------------");
            string UserId1 = Result.GetValue(0).ToString();
            string Title1 = Result.GetValue(1).ToString();
            string Text1 = Result.GetValue(2).ToString();
            Console.WriteLine("ID: " + UserId1);
            Console.WriteLine("Title: " + Title1);
            Console.WriteLine("Text: " + Text1);

        }
        goto Mainmenu; //uses goto to move back to the main menu after the Userchoice '1' has ran.
    }


    //__________________________________________________________________________________________________________________________________// Choice 2


    else if (Userchoice == "2")
    {
        Console.Clear();
        Console.WriteLine("-----------------------------");
        Console.WriteLine(); Console.WriteLine("Displaying Id and Category");
        Console.WriteLine("");
        string Sql = "SELECT * FROM Category";
        SqlCommand Command = new SqlCommand(Sql, Connection);
        Connection.Close();
        Connection.Open();
        SqlDataReader Result = Command.ExecuteReader();
        while (Result.Read())
        {

            Console.WriteLine("-----------------------------");
            string UserId1 = Result.GetValue(0).ToString();
            string Category1 = Result.GetValue(1).ToString();
            Console.WriteLine("ID: " + UserId1);
            Console.WriteLine("Category: " + Category1);


        }
        goto Mainmenu;
    }
    //__________________________________________________________________________________________________________________________________// Choice 3


    else if (Userchoice == "3")
    {


        Console.Clear();
        Console.WriteLine("Add a new blog post");
        bool runloop = true;
        while (runloop == true)
        {

            Console.WriteLine("Enter 'q' to return to menu");
            Console.WriteLine("Enter Id: ");


            string UserId = Console.ReadLine();
            if (UserId.ToLower().Trim() == "q")
            {

                goto Mainmenu;
                //break; Kills the program, i choose to go back to main menue to allow user to have another action, or press 7 in order to exit the program completly.
            }


            Console.WriteLine("Enter 'q' to return to menu");
            Console.WriteLine("Enter Title: ");
            string Title = Console.ReadLine();
            if (Title.ToLower().Trim() == "q")
            {

                goto Mainmenu;
                //break;
                
            }

            Console.WriteLine("Enter 'q' to return to menu");
            Console.WriteLine("Enter Text: ");
            string Text = Console.ReadLine();
            if (Text.ToLower().Trim() == "q")
            {

                goto Mainmenu;
                //break;                      
            }
            //__________________________________________________________________________________________________________________________________// Storing Id, Title and Text and showing user what was written

            int BlogId = Convert.ToInt32(UserId);
            string Sql = "INSERT INTO BlogPost VALUES(" + BlogId + ", '" + Title + "', '" + Text + "')"; //Insert the values from the strings/int into BlogPost cateogories 1,2,3
            Console.WriteLine("-----------------------------");
            Console.WriteLine("");
            Console.Write("Du skrev in följande:\n ");
            Console.WriteLine("");
            Console.WriteLine("-----------------------------");
            Console.WriteLine("\n-----Id-----\n: " + UserId + ". \n-----Category-----\n: " + Title + ". \n-----Text-----\n: " + Text); //Shows what was written so the users can see what they inputed


            SqlCommand Command = new SqlCommand(Sql, Connection);
             
            Connection.Close();
            Connection.Open();
            Command.ExecuteNonQuery();
            Connection.Close();

            Console.WriteLine("-----------------------------");
            Console.WriteLine("");
            Console.WriteLine("The data has been stored inside the Bloggy database.");
            Console.WriteLine("");
            Console.WriteLine("-----------------------------");
            goto Mainmenu;
            Console.ReadLine();
        }
    }


    //__________________________________________________________________________________________________________________________________// Choice 4

    else if (Userchoice == "4")
    {
        Console.Clear();
        Console.WriteLine("Add a new category");

       
        while (KeepRunning == true)
        {

            Console.WriteLine("Enter 'q' to return to menu");
            Console.WriteLine("Enter Id: ");


            string UserId = Console.ReadLine();
            if (UserId.ToLower().Trim() == "q")
            {
                goto Mainmenu;
                //break;
            }

            Console.WriteLine("Enter 'q' to return to menu");
            Console.WriteLine("Enter Category: ");


            string Category = Console.ReadLine();
            if (Category.ToLower().Trim() == "q")
            {
                goto Mainmenu;
                //break;
            }
            string sqlCheck = "SELECT COUNT(*) FROM Category WHERE Category = @Category";
            SqlCommand commandCheck = new SqlCommand(sqlCheck, Connection);
            commandCheck.Parameters.AddWithValue("@Category", Category);
            Connection.Close();
            Connection.Open();
            int count = (int)commandCheck.ExecuteScalar();

            if (count > 0)
            {
                Console.WriteLine("Category already exists!");
                continue;
            }

            //__________________________________________________________________________________________________________________________________// Storing Id and Category and showing user what was written
            int CatId = Convert.ToInt32(UserId);
           string Sql = "INSERT INTO Category VALUES(" + CatId + ", '" + Category + "')";

            Console.WriteLine("-----------------------------");
            Console.WriteLine("");
            Console.Write("Du skrev in följande:\n ");
            Console.WriteLine("");
            Console.WriteLine("-----------------------------");

            Console.WriteLine("\n-----Id-----\n: " + UserId + ". \n-----Category-----\n: " + Category);

            SqlCommand Command = new SqlCommand(Sql, Connection);

            Connection.Close();
            Connection.Open();
            Command.ExecuteNonQuery();
            Connection.Close();

            Console.WriteLine("-----------------------------");
            Console.WriteLine("");
            Console.WriteLine("The data has been stored inside the Bloggy database.");
            Console.WriteLine("");
            Console.WriteLine("-----------------------------");



        }
        
    }

    //__________________________________________________________________________________________________________________________________// Choice 5

    if (Userchoice == "5")
    {
        Console.Clear();
        Console.WriteLine(); Console.WriteLine("Displaying Id and Category");
        Console.WriteLine("");

        string Sql = "SELECT * FROM Category";
        SqlCommand Command = new SqlCommand(Sql, Connection);
        Connection.Close();
        Connection.Open();
        SqlDataReader Result = Command.ExecuteReader();
        while (Result.Read())
        {

            Console.WriteLine("-----------------------------");
            string UserId1 = Result.GetValue(0).ToString();
            string Category1 = Result.GetValue(1).ToString();
            Console.WriteLine("ID: " + UserId1);
            Console.WriteLine("Category: " + Category1);


        }

        Console.WriteLine("Choose the ID of a category: ");
        string ReadId = Console.ReadLine();
        Convert.ToInt32(ReadId);

        string FindBlogId = $"SELECT BlogPostID FROM BlogPostCategory WHERE CategoryID = '{ReadId}'";


        SqlCommand Command1 = new SqlCommand(FindBlogId, Connection);
        Connection.Close();
        Connection.Open();
        SqlDataReader Result1 = Command1.ExecuteReader();



        List<string> list = new List<string>(); 
        string WhatUserId = "";
        while (Result1.Read())
        {



            WhatUserId = Result1.GetValue(0).ToString(); //Grabbing value and adding it to the list.
            list.Add(WhatUserId);


        }
        bool TitleFound = false; // bool that is set to false, only activates when true, if true i would have to set everything else to false, so easier to make it false to begin with.
        Connection.Close();
        foreach (string Id in list)
        {

            string DisplayTitles = $"SELECT Title FROM BlogPost WHERE BlogID = '{Id}'";
            SqlCommand Command2 = new SqlCommand(DisplayTitles, Connection);
            Connection.Close();
            Connection.Open();
            SqlDataReader Result2 = Command2.ExecuteReader();

            Console.WriteLine();
            Console.WriteLine("Displaying title: ");
            Console.WriteLine("----------------------------");
            Console.WriteLine();
            while (Result2.Read())
            {
                string DisplayTitle = Result2.GetValue(0).ToString();
                Console.WriteLine(DisplayTitle);
                TitleFound = true; // if bool is true it will print out the titles. 


            }
            Connection.Close();
        }
        if (TitleFound == false) // if bool is false it will print out that there are no titles with that cateogry in the database
        {
            
            Console.WriteLine("----------------------------");
            Console.WriteLine();
            Console.WriteLine("There are no titles with that category in the database:" + ReadId);
            Console.WriteLine();
        }
        Console.WriteLine("----------------------------");
        Console.WriteLine();
        Console.WriteLine("Returning to main menu: ");
        goto Mainmenu;
    }

    //__________________________________________________________________________________________________________________________________// Choice 6

    else if (Userchoice == "6")
    {
        Console.WriteLine("Add an existing blog to an existing category");
        string Sql = "SELECT * FROM Category";

        using (SqlConnection Connection2 = new SqlConnection(ConnectionString))
        {
            Connection2.Open();
            SqlCommand Command = new SqlCommand(Sql, Connection2);
            SqlDataReader Result = Command.ExecuteReader();
            Console.WriteLine("Id: " + "CategoryName: ");
            while (Result.Read())
            {
                string Id = Result.GetValue(0).ToString();
                string CategoryName = Result.GetValue(1).ToString();
                Console.WriteLine(Id.PadRight(5) + CategoryName.PadRight(15)); // padding to make the displayed opitons easier to see for the user
            }
            Console.WriteLine();
            Result.Close();
        }

        try //try catch in order to grab any errors.
        {
            Console.Write("Choose a category Id: ");
            string CategoryIdstring = Console.ReadLine();
            int CategoryId = Convert.ToInt32(CategoryIdstring);

            
            Console.WriteLine(); Console.WriteLine("Displaying Id, Title and Text");
            Console.WriteLine("");
            Sql = "SELECT * FROM BlogPost";
            SqlCommand Command = new SqlCommand(Sql, Connection);
            Connection.Close();
            Connection.Open();
            SqlDataReader Result = Command.ExecuteReader();
            while (Result.Read())
            {

                Console.WriteLine("-----------------------------"); // printing out the values for the user to see
                string UserId1 = Result.GetValue(0).ToString();
                string Title1 = Result.GetValue(1).ToString();
                string Text1 = Result.GetValue(2).ToString();
                Console.WriteLine("ID: " + UserId1);
                Console.WriteLine("Title: " + Title1);
                Console.WriteLine("Text: " + Text1);

            }
           
            Console.Write("Choose a blog post Id: ");
            string IdString = Console.ReadLine();
            int BlogId = Convert.ToInt32(IdString);

            string InsertQuery = $"INSERT INTO BlogPostCategory VALUES('{BlogId}',  '{CategoryId}')"; // Insert the values into BlogPostCategory linking the two together through Foreign keys.
            SqlCommand InsertQueryCommand = new SqlCommand(InsertQuery, Connection);
            Connection.Close();
            Connection.Open() ;
            InsertQueryCommand.ExecuteNonQuery();
            Connection.Close();
        }
        catch (Exception ex) // Catching errors and printing them out through message function
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        
        
        }
    }
    //__________________________________________________________________________________________________________________________________// Choice 7 Exit the program.

    else if (Userchoice == "7") //exits the program with a button instead of Q making Q return to menue seems more user-friendly. Prints out the BlogPosts
    {

        Console.Clear();    


        KeepRunning = false;
        string Sql = "SELECT * FROM BlogPost";
        SqlCommand Command = new SqlCommand(Sql, Connection);
        Connection.Open();
        SqlDataReader Result = Command.ExecuteReader();

        Console.WriteLine("-----------------------------");
        Console.WriteLine("");
        Console.WriteLine("Displaying all posts");
        Console.WriteLine("");
        Console.WriteLine("-------------------------------");
        while (Result.Read())
        {
            string Id = Result.GetValue(0).ToString();
            string BlogTitle = Result.GetValue(1).ToString();
            string BlogText = Result.GetValue(2).ToString();
            
            
            Console.WriteLine("ID: " + Id);
            Console.WriteLine("Title: " + BlogTitle);
            Console.WriteLine("Text: " + BlogText);
            



        }
        Connection.Close();

        Console.WriteLine("-----------------------------");
        Console.WriteLine("");
        Console.WriteLine("Exiting program");
        Console.WriteLine("");
        Console.WriteLine("-----------------------------");
        Console.WriteLine("");
        Console.WriteLine("Thank you for using Bloggy!");
        Console.WriteLine("");
        Console.WriteLine("-------------------------------");


    }

    else // else tells user they made the wrong input and will tell them to choose between 1-7.
    {

        Console.WriteLine("");
        Console.Clear();
        Console.WriteLine("Wrong input");
        Console.WriteLine("Please enter a number between 1-7 as displayed below.");
        goto Mainmenu;


    }



 

    





}
