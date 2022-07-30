Following application has been created with .Net 6
It Contains 4 separate projects 
		RetailCustomerBonusCalculator - Main web project
		BusinessService - Contains the business logic
		DataAccesslayerService -  Interacts with SQLDB(UsersDB)
		RetailCustomerBonusCalculator.UnitTests  -- Contains unit tests

I have put  a DB backup file named UsersDb.bk under static data folder. Please restore it to and pass your loca lconnection string in AppSettings.Json file under RetailCustomerBonusCalculator 
Above mentioned DB contains 2 tables named as "Customers" & "Transactions"
I have put some random data of 1000 customers in first table and around 10k rows in transactions DB
Also, I am attaching the screenshot of DB structure for reference. Complete tables are shared in csv format under static data folder.
  

My API exposes 2 endpoints 
/apiCustomerBonus/GetAllCustomersTransactions
i.	takes numberOfMonths as input param 
ii.	returns Customer data with the corresponding Reward points for all the customer for the given last numberOfMonths

/apiCustomerBonus/GetACustomerTransaction/{mobileNumber}
i.	takes MobileNumber(unique to each customer) and numberOfMonths as input params
ii.	returns Customer data with the corresponding Reward points for the mentioned single customer for the given last numberOfMonths

Note: if numberOFmonths is <=0 , it returns the entire transactiondata.

I am using serilog as Logging tool and logging the data to "logs" directory in source project in "RetailCustomerBonusCalculator--.txt file" 
which gets rolled over every minute so that can have maximum number of logs for testing and also its logging level is above information.


Unit Test project is created as RetailCustomerBonusCalculator.UnitTests. I have written 4 testcases as a part of it.
To run the testcases please navigate to the testcase folder and run below command
-----dotnet test RetailCustomerBonusCalculator.UnitTests.csproj


PS: The Solution is Containersed. Pls clone the sourcecode and run docker-compose up from the RetailCustomerRewardPointsAssignmnet-Net6\RetailCustomerBonusCalculator folder
If this doesnt work. Please run with .net6 sdk in vs.

![]("images/AllCustomers-1month.png")
![]("images/AllCustomers-3months.png")
![]("images/singlecutomer-1month.png")
![]("images/singlecutomer-3months.png")
![]("images/customers-db-data.png")
![]("images/transactions-db-data.png")