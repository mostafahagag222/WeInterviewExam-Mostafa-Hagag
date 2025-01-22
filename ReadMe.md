# Project Setup Instructions

## Steps to Run the Project

### Step 1: Backup the SQL Server Database
1. Locate the database backup file at:
   ```
   ./WeInterviewExam/Executables-MostafaHagag/Database/electricity-outages.trn
   ```
2. Ensure you are using **SQL Server version 16** to restore the backup.
3. If you encounter issues accessing the file, copy it to the default SQL Server backup directory:
   ```
   C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\Backup\
   ```
4. Restore the backup on the default SQL Server instance (local server `.`) to ensure connection strings work correctly.

### Step 2: Start the APIs
1. Navigate to the APIs executable:
   ```
   .\WeInterviewExam\Executables-MostafaHagag\Api\Apis.Presentation.exe
   ```
2. Launch the executable. It will host the APIs on the local server at **port 5000**.
3. Ensure **port 5000** is not occupied by any other application.
4. Note:
   - The APIs were created using **.NET 8.0**.
   - Verify that all dependencies for .NET 8.0 are installed if the APIs fail to start.

### Step 3: Start the Console Application
1. Navigate to the console executable:
   ```
   .\We\WeInterviewExam\Executables-MostafaHagag\Console\ConsoleApplication.Presentation.exe
   ```
2. Launch the executable. The console app will:
   - Run indefinitely.
   - Generate test data.
   - Call the hosted APIs and populate data for `cuttings A` and `cuttings B`.
   - Execute the stored procedures `SP_Create` and `SP_Close`.

### Step 4: Start the MVC Web Portal
1. Navigate to the MVC (Web Portal) executable:
   ```
   .\We\WeInterviewExam\Executables-MostafaHagag\WebPortal\MVC.Presentation.exe
   ```
2. Log in using the following credentials (case-sensitive):
   - **Username:** Admin
   - **Password:** Admin
3. Launch the executable. It will host the Web Portal at:
   ```
   http://localhost:5001
   ```
4. Open a browser and navigate to the URL above.

---

## Additional Notes
- Ensure **SQL Server** and the required **.NET runtime for .NET 8.0** are properly installed and configured.

---

## Project Overview
This project was custom-developed for **We Telecom Company** as part of an interview task. It implements robust solutions leveraging the latest technologies and design patterns to handle performance optimization, big data processing, and security considerations.

---

## Key Features and Architecture

### Database
- The database schema includes scalar functions, stored procedures, and foreign key relationships but starts with no data.
- Names for entities (e.g., `flat`, `individual subscriptions`, and `corporate subscriptions`) ensure accurate data transfer by enabling joins on names during data migration.
- Data transfer is performed from staging network elements tables to fact tables, ensuring correct relationships and foreign key mappings.

### Console Application
- **Architecture:** Three-tier architecture:
  - **Presentation Layer:** Handles interaction and API calls.
  - **Business Logic Layer:** Implements core logic and service operations.
  - **Data Access Layer:** Manages database interaction using repositories and the Unit of Work pattern.
- **Technologies:**
  - Dependency injection using the Autofac library.
  - Entity Framework Core for secure database access and prevention of SQL injection.
- **Features:**
  - Generates consistent, relationally accurate data, ensuring valid foreign keys and entity relationships.
  - Optimized for performance using:
    - Multithreading for concurrent task processing.
    - Semaphore for efficient API concurrency.
  - Decompression mechanism for compressed API responses.
  - Secure user creation process with hashed and salted passwords.

### API
- **Architecture:** Three-tier architecture adhering to modern design principles:
  - Dependency Injection via .NET's official DI container.
  - Entity Framework Core for data access with repositories and Unit of Work pattern.
- **Features:**
  - **Rate Limiting Middleware:** Restricts concurrent requests per client.
  - **Response Compression Middleware:** Reduces response sizes for better performance.
- **Endpoints:**
  - `SP_Create API`: Transfers data from staging tables (`Cutting Down A` and `Cutting Down B`) to fact tables (`Cutting Header`, `Cutting Details`, and `Cutting Ignored`).
  - `SP_Close API`: Completes the transfer process and finalizes data processing.

### Web Portal
- **Architecture:** Built using C# MVC Framework with three-tier architecture.
- **Frontend:**
  - JavaScript (DOM manipulation) for responsive UI interactions.
- **Features:**
  - **Authentication Middleware:** Uses JWT tokens stored in cookies for secure authentication.
  - **Custom Routing:** Implements tailored routing solutions.
  - **Pages:**
    - **Login Page:** Handles user authentication.
    - **Search Page:** Enables searching for cuttings using multiple criteria or null search parameters (returns all cuttings).
    - **Ignored Cuttings Page:** Displays ignored cuttings with dynamic deletion capabilities.
    - **Add Cuttings Page:**
      - Includes a form to add new cuttings.
      - Features a hierarchical search section for finding cuttings within the network element hierarchy.
      - Allows dynamic deletion of specific cuttings.
  - **Backend:** Uses stored procedures and LINQ queries for dynamic data population.

---

## Technical Highlights

### Security
- Secure password storage with hashing and salting.
- SQL injection prevention through parameterized queries and Entity Framework Core.

### Performance Optimization
- Multithreading for task concurrency.
- API concurrency managed with semaphores.
- Compression mechanisms for smaller response sizes.

### Big Data Handling
- Efficient transfer of large datasets between staging and fact tables.
- Optimized queries to handle relational data and ensure consistency.

---

## Technologies Used

- **Backend:** C#, ASP.NET Core, Entity Framework Core.
- **Frontend:** JavaScript, MVC Framework, DOM manipulation.
- **Database:** SQL Server with stored procedures and scalar functions.
- **Libraries and Tools:** Autofac, LINQ, .NET Dependency Injection.

---

## Final Notes
This project was developed to meet the specific requirements of **We Telecom Company** during an interview task, showcasing:

- The use of modern technologies.
- Scalability and performance considerations.
- Robust handling of large datasets and complex relationships.

### Developer Contact Information
- **Name:** Mostafa Ahmed Hagag
- **Phone:** +201069424082
- **Email:** moustafahagag222@gmail.com
