
# Proposals

## Proposal 1

**Name:** "Dream Real Estate Solutions"

**Description:** A website that uses the federal governemnt API to pull crime statistics for an area, which will be used as supplementary data for housing market prices.

**Details:**

1. Pulls crime statistics and other data via the government API. 
2. Will also pull housing market prices via Zillow API.

**Requirements Fullfilled:**

* Webpage: Front page welcoming user, will have a login and other user features
* Algorithm used will generate a rating and risk factor; additionally, an algorithm that generates a predicted loss of value based of crime, such as theft, burglary, hit and run, etc.
* APIs used: Government, Zillow
* SQL: User login information will be stored with SQL server

---

## Proposal 2

**Name:** "Peak Training"

**Description:** A website that uses the Fitbit  API to track user physical statistics as a tool of guidance for improving resting heart rate, physical endurance, and strength. Of course, let user know that they should talk to their doctor before following tips, additionally, that we are not responsible for thier injuries, all that.

**Details:**

1. Can show user progress, in terms of performance and body composition. 
2. Can possibly have some nutritional recommendations based on what the user is trying to achieve.
3. This is primarily a TRAINING app, not a fitness app. When you train its because you want to improve youself, not to look good.

**Requirements Fullfilled:**

* Webpage: Front page welcoming user, will have a login and other user features
* Algorithm will use user request, and statistics to generate a fitness guidance routine, which would be appropriate for the user.
* API used: Fitbit
* SQL: User login info, and statistics, stored in SQL server

---

## Proposal 3

**Name:** "LOB: League of Board Games"

**Description:** A website that hosts traditional board games. Players can play against an AI, or each other.

**Details:**

1. The board games in question can be, for example: Chess, Checkers, Go, and more.
    * We don't want to implement modern games like Monopoly, for a multitude of reasons.
    * Traditional board games, like the ones mentioned, are much simpler and have the potential to be done in a single sprint. Though the AI component may need its own sprint.
2. Users can log in, but this will be optional for certain services.
3. The AI in question can be a rudimentary one written in JavaScript, or a more advanced one that uses machine learning.

**Requirements Fullfilled:**

* Webpage: Front page welcomes user, lets user login, and play a game
    * Currently unknown how rendering games will work; Options include `two.js`, `three.js`
* Algorithm components:
    * All games should have a starting/backup AI, that doesn't rely on third-party AI services.
        * In future sprints, there can also be a "hard" AI that does use machine leaning via third-party services.
    * An algorithm, whether AI or not, to help determine a player's ELO/MMR rating in a potential PvP mode.
        * In addition, an algorithmic component can be added for matchmaking.
* APIs used: One major AI service (IBM Watson, Google Cloud AI, ML via AWS, etc.)
    * Other API integrations are being considered
* SQL: User login informaton, statistics, match histories, all stored in SQL server.