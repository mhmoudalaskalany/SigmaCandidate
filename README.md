# Sigma Candidate Api
- An Api To Handle the Collection Of Data From Candidates For Job Applications And Interviews


# Notes
- N-Tier Architecture is used to assure the decoupling as much as possible
- CRUD Service Pattern Has Been Used Over DDD because of the simplicity of the domain model 
- In Memory Database Has been Used Beside Sql Server With Entity Framework for Infrastrucutre Mode 1 abd CSV File Is Uses With Infrastructure Mode 0


# Running The Project
- Clone the Repository and start the project from Candidate.Api Lunch Profile
- To Save The Data in CSV file change the (InfrastructureType) in appsettings.json to 0 (default value)
- To Save The Date In A Database change the (InfrastructureType) in appsettings.json to 1 
- To Save The Data in InMemoryDatabase change UseInMemoryDatabase to true (default)
- To Save The Data in Sql Database  change UseInMemoryDatabase to false (Migration Will Be Migrated Authomatically)

![layers](https://github.com/mhmoudalaskalany/Images/blob/main/task_images/appsettings.png)


# Test:
- Unit Tests has been written to cover the most business rules and make sure code coverage is accepted
- some of the classes has been excluded from code coverage due to time shortage
![layers](https://github.com/mhmoudalaskalany/Images/blob/main/task_images/testresult.png)

# Improvements:
- DDD architecture can be use instead of CRUD service with Mediator and CQRS if the domain model is getting more complex
- More Tests Can be Written to Cover More Parts Of The Code
- Memory Caching Can Be Used To Cache Api Responses


# Time:
- Total Time Of Implementation 7:40 Hours including laying out the base architecture of the project and folder structure
