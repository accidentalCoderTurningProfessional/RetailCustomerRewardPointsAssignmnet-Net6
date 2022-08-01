## Project Structure
Following application has been created with .Net 6.

It Contains 4 separate projects

		````* RetailCustomerBonusCalculator - Main web project
		
		    * RetailCustomerBonusCalculator.BusinessService - Contains the business logic	
		    
		    * RetailCustomerBonusCalculator.DataAccessService -  Interacts with SQLDB(UsersDB)
		    
		    * RetailCustomerBonusCalculator.UnitTests  -- Contains unit tests````

## Sample DB
I have put  a DB backup file named **UsersDb.bk** under **SampleDbFiles** folder. 

Also, i have put the sql script **(UsersDbScript)** to produce the required database and corresponding schema and sample datasets. Please take this script and run int ur local server

Please restore it to and pass your local connection string in **AppSettings.Json** file under RetailCustomerBonusCalculator. 

Above mentioned DB contains 2 tables named as **Customers & Transactions**.

I have put some random data of 1000 customers in first table and around 10k rows in transactions DB

Also, I am attaching the screenshot of DB structure for reference. Complete tables are shared in csv format under static data folder.
  
## API endpoints
My API exposes 2 endpoints 

/apiCustomerBonus/GetAllCustomersTransactions

		i.takes numberOfMonths as input param 
		
		ii.returns Customer data with the corresponding Reward points for all the customer for the given last numberOfMonths

/apiCustomerBonus/GetACustomerTransaction/{mobileNumber}

		i.takes MobileNumber(unique to each customer) and numberOfMonths as input params
		
		ii.returns Customer data with the corresponding Reward points for the mentioned single customer for the given last numberOfMonths
		

**Note: if numberOFmonths is <=0 , it returns the entire transactiondata.**

## Logging
I am using serilog as Logging tool and logging the data to **logs**directory in source project in **RetailCustomerBonusCalculator--.txt file** 
which gets rolled over every minute so that can have maximum number of logs for testing and also its logging level is above information.

## Unit test cases

Unit Test project is created as RetailCustomerBonusCalculator.UnitTests. I have written 4 testcases as a part of it.
To run the testcases please navigate to the testcase folder and run below command

**command**: dotnet test RetailCustomerBonusCalculator.UnitTests.csproj

## Docker Containerisation
PS: The Solution is Containersed. Pls clone the sourcecode and **run docker-compose up** from the RetailCustomerRewardPointsAssignmnet-Net6\RetailCustomerBonusCalculator folder
If this doesnt work. Please run with .net6 sdk in vs.

# Screenshots of response and sample data

## sample customers data from sql DB
<img width="264" alt="customers-db-data" src="https://user-images.githubusercontent.com/94400828/182003975-35a17c3e-ee1b-404a-91ed-82ae106c12af.png">

## sample transactions data from sql DB
<img width="298" alt="transactions-db-data" src="https://user-images.githubusercontent.com/94400828/182003978-ec82e1df-0b73-47e2-a88f-cdd962bd3279.png">

## Response for all the customers for one month period
<img width="524" alt="AllCustomers-1month" src="https://user-images.githubusercontent.com/94400828/182003973-d2d9a6fc-b158-4b1e-acd5-ab9c549cdd4c.png">

## Response for all the customers for 3 months period
<img width="537" alt="AllCustomers-3months" src="https://user-images.githubusercontent.com/94400828/182003974-44bcec30-ac54-4ab0-8788-dbaafae2e8ee.png">

## Response for all one single customer for 1 month period
<img width="534" alt="singleCustomer-1month" src="https://user-images.githubusercontent.com/94400828/182003976-aaa040de-a390-4415-8d7a-4faa363a5c9b.png">

## Response for one single customer for 3 month period
<img width="535" alt="singleCustomer-3months" src="https://user-images.githubusercontent.com/94400828/182003977-0e555ec9-2171-41f7-9cf0-43f70b2934dc.png">
