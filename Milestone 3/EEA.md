# Requirements Workup

## Elicitation

1. Is the goal or outcome well defined?  Does it make sense?
2. What is not clear from the given description?
3. How about scope?  Is it clear what is included and what isn't?
4. What do you not understand?
    * Technical domain knowledge
    * Business domain knowledge
5. Is there something missing?
6. Get answers to these questions.


### Elicitation Answers
    1. The goal of the project is to provide users with a tool to be informed about the community they will be a part of if they pick a home of interest.
    2. How will it be achieved? What will provide this particular application with the capacity to inform users?
    3. The scope is clear. Users will have an alternate tool for making informed desicions about home they will potentially buy. This means a log in service is needed, visual representation of crime, as well as textual representation of crime, and market information about homes.
    4. Technical domain knowledge, the destination is clear to the team. Business domain knowledge gets fuzzy when thinking about the legality of real estate application that provides insight into the crime that a community experiences. 
    5. The understanding of utilizing information provided by the API.

## Analysis

Go through all the information gathered during the previous round of elicitation.  

1. For each attribute, term, entity, relationship, activity ... precisely determine its bounds, limitations, types and constraints in both form and function.  Write them down.
2. Do they work together or are there some conflicting requirements, specifications or behaviors?
3. Have you discovered if something is missing?  
4. Return to Elicitation activities if unanswered questions remain.

### Analysis Answers
    1. 
    A. Login Feature is a top priority. This essential for the entire application because a user will want to be able to save thier research. The log in service provides a one stop shop for that. The hurdles with implementing the login feature is minimal due to MVC's highly documented login service provider.
    B. Check crime rates from zipcodes is another key feature because it is what makes this application stand out. This feature will be the driving factor into developement of other features such as Longitude and Latitiude coordinates. Some constraints would include lack of information in part of the API. This includes the crime APIs and the google maps API and the ability of our team to coordinate the data of each API.
    C. Home price history is another top feature because it allows us to compare information between homes, as well as help coorilate price increase or decrease due to crime in the area. Zillow's api may lack historic data on homes, and just like the previous answer, hurdles arise in our ability to coordinate atleast three different APIs.
    2.
    A. The login feature works in hand with the other fearures because it will provide users with the ability to store information uniquely to themselves. This information is gathered by the other two main features.
    B. When a user logs in, the user can have their saved information compared to looked up information when searching for a new home. As such, they work together to provide a research tool. 
    C. Home prices can be compared between homes, and coorilate price to crime rates. In effect, all three features depend on eachother to provide a unique service unlike any other real estate app.
    3. Nothing missing, however that may change as we break down the Epics.




