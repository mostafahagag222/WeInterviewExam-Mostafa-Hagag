Steps to Run the Project
Step 1: Backup the SQL Server Database
Locate the database backup file at:
./WeInterviewExam/Executables-MostafaHagag/Database/electricity-outages.trn

Note:

The backup was created using SQL Server version 16.
Ensure you are using the correct SQL Server version to restore the backup.
If you encounter any issues accessing the file, copy it to the default backup directory for SQL Server: 
  C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\Backup\
Restore the backup on the default SQL Server instance (local server .) to ensure the connection strings work correctly.

Step 2: Start the APIs
Navigate to the APIs executable:
.\WeInterviewExam\Executables-MostafaHagag\Api\Apis.Presentation.exe
Launch the executable. It will host the APIs on the local server at port 5000.
Ensure port 5000 is not occupied by any other application.
Note:
The APIs were created using .NET 8.0.
If the APIs fail to start, verify that all dependencies for .NET 8.0 are installed.

Step 3: Start the Console Application
Navigate to the console executable:
.\We\WeInterviewExam\Executables-MostafaHagag\Console\ConsoleApplication.Presentation.exe
Launch the executable. The console app will: Run indefinitely - Generate test data - Call the hosted APIs and populate data for
 cuttings A and cuttings B - Execute the stored procedures SP_Create and SP_Close.

Step 4: Start the MVC Web Portal
-Navigate to the MVC (Web Portal) executable:
  .\We\WeInterviewExam\Executables-MostafaHagag\WebPortal\MVC.Presentation.exe
-Log in using the following credentials (case-sensitive):
Username: Admin
Password: Admin
-Launch the executable. It will host the Web Portal at:
  http://localhost:5001
Open a browser and navigate to the URL above.

Additional Notes
Ensure SQL Server and the required .NET runtime for .NET 8.0 are properly installed and configured.