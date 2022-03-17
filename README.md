#Technical Assignment

#Problem statement 
Please write an application that outputs:
• All customers’ first names (comma separated) who are 56.
• All customer’s IDs and associated phone numbers. Refer to the phone number rules on
the next page. E.g.
ID: 5, Phone Number: (02) 9122 3451
ID: 7, Phone Number: Missing or invalid
• The number of valid phone numbers per state, displayed in ascending alphabetical
order. E.g.
ACT: 5
NSW: 10

#setting up project environment

Technical Assignment using .Net CORE 5.0 using c#
# Requirements
- Visual Studio 
- .Net CORE 5.0
- C#
# Structure
- Solution contains 4 projects 
- CustomerModel : class library contains different data models.
- CustomerServices: class library contain service methods to handle business logic. 
- CustomerAPI- ASP.NET CORE Web API contain controller with  method to
               1) To get customers’ first names (comma separated) who are 56.
               2) All customer’s IDs and associated phone numbers.
               3) The number of valid phone numbers per state, displayed in ascending alphabetical
               with swagger support to test 
- CustomerServicesTests - the Unit test Project using MSTest 

# Building
- Open Visual Studio ->Right click on CustomerAPI ->Properties ->Build tab -> Output section -> select "XML Documentation file"
                       This will generate swagger UI Documentation based on XML comments
- Build solution -> Build -> Build All 
                        Make sure package dependancies Swashbuckle.AspNetCore and Swashbuckle.AspNetCore.Annotations,Microsoft.Extensions.Configuration.Json and
                        Microsoft.Extensions.DependencyInjection have installed.
- Right click on CustomerAPI project and click on "set as start up proect" 

# Execution
-- Open Visual Studio -> Run -> Start Without Debugging
This will launch http://localhost:8573/swagger/index.html  , this is swagger UI with documentation related to each API methods and model structure 
to generate documentation click on link /swagger/v1/swagger.json  this will generate API json based document.

# Testing
- Open CustomerServicesTests project -> open Test Explorer -> Run all tests


                        
