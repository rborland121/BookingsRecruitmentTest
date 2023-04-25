# Zonal Bookings Back-End Recruitment Test
---
Thanks for taking the time to do our back-end coding test. The challenge is consisted of two parts:
1. a [task](#task) to create a simple SpaceX API
2. some [follow-up questions](#follow-up-questions)
---
## Task
We have provided you with a C# .NET Core application with minimum code setup to get you started, the main items we are looking for are:
- Create a new Rocket controller to get rocket information from the SpaceX API
- Expand the Launch controller to do the following:
- - Implement CRUD around the Launch entity
- - Fix any bugs you encounter 
- Make sure the SpaceX API client is registered correctly
- Write unit test coverage for the following: 
- - LaunchBL
- - New Rocket implementation
- - SpaceX API client
- Any code refactors for making the code more readable and testable is allowed

You will be graded based on the above acceptance criteria's

---
## Follow up Questions
- If you had more time, what further improvements or new features would you add?
- Which parts are you most proud of? And why?
- Which parts did you spend the most time with? What did you find most difficult?
- How did you find the test overall? Did you have any issues or have difficulties completing? If you have any suggestions on how we can improve the test, we'd love to hear them.

---
## Running the solution:
- Clone the repo to your computer `git clone https://github.com/arman-zonal/BookingsRecruitmentTest.git`
- Basic .NET Core application that can be run using Visual Studio
- Do not forget to change/add a new origin for your repository when submitting the work

---
## Submission Guidelines
- Create a public GitHub repository for our developers to be able to pull down your solution (**make sure it is public**)
- Push your code to the created repository
- Send the GitHub link of your repository to HR
- Make sure to add answers to the follow up questions in the email 

---
## Documentation:
SpaceX API [here](https://docs.spacexdata.com/#16c58b5e-44de-4183-b858-0fae51d242a5)

Use only the Launches and Rockets endpoints from the above documentation

Launch - are details surrounding test flights of rockets
Rocket - details of the manufactured rocket

### Provided DTO's:
SpaceXLaunchDTO
```
public class SpaceXLaunchDTO
    {
        [JsonPropertyName("flight_number")]
        public int FlightNumber { get; set; }

        [JsonPropertyName("mission_name")]
        public string MissionName { get; set; }

        [JsonPropertyName("mission_id")]
        public int[] MissionId { get; set; }

        [JsonPropertyName("launch_year")]
        public string LaunchYear { get; set; }

        [JsonPropertyName("launch_date_unix")]
        public int LaunchDateUnix { get; set; }

        [JsonPropertyName("launch_date_utc")]
        public DateTime LaunchDateUTC { get; set; }

        [JsonPropertyName("launch_date_local")]
        public DateTime LaunchDateLocal { get; set; }

        [JsonPropertyName("rocket")]
        public SpaceXRocketDTO Rocket { get; set; }
    }
```

SpaceXRocketDTO
```
public class SpaceXRocketDTO
    {
        [JsonPropertyName("rocket_id")]
        public string RocketId { get; set; }

        [JsonPropertyName("rocket_name")]
        public string RocketName { get; set; }

        [JsonPropertyName("rocket_type")]
        public string RocketType { get; set; }
    }
```

LaunchDTO
```
public class LaunchDTO
    {
        public int FlightNumber { get; set; }
        public string MissionName { get; set; }
        public string LaunchYear { get; set; }
        public DateTime LaunchDateUTC { get; set; }
    }
```

RocketDTO
```
public class RocketDTO
{
    public string RocketId { get; set; }
    public string RocketName { get; set; }
    public string RocketType { get; set; }
}
```
