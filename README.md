# Meal Mate

Supports you with your meal planning and your daily shopping. 

---

I didn't want to repeat the following things regarding the meal plan and shopping list every one to two weeks:
- "Which ingredients should be available and which planned meals can I cook with them?"
- "Hmm.. what I gonna eat this or next week? What do I like?"
- "What ingredients do I need for the planned meals?"
- Then I have to check the recipes and add the items to my shopping list

This procedure consumes time that I rather spend somewhere elese. Therefore I create a little app that helps me with the following stuff: 

**Roadmap**

🚲 Basic 
- [x] Show me shopping lists with the stuff that I have to buy somewhere.  
  - [x] I can add manually add items to the list.
  - [x] I can check off/remove items on the shopping list during shopping.
  - [x] Send live updates to my list and other user of the list

- [ ] Manage the meals I like with the corresponding ingredients.
  - [ ] I can select a meal and add the ingredients to the shopping list.

🚀 Nice to have:

- [ ] Authentication
- [ ] Associate the shopping list with an user
- [ ] Manage a meal plan with the meals for the next one to two weeks
- [ ] Share a shopping list with other user


# Setup

## Create a postgre sql database server

Easy setup with a docker container.
```docker-compose
version: '3.8'
services:
  db:
    image: postgres:14.1-alpine
    restart: always
    environment:
      - POSTGRES_USER=postgres
      - POSTGRES_PASSWORD=postgres
    ports:
      - '5432:5432'
    volumes:
      - db:/var/lib/postgresql/data

  adminer:
    image: adminer
    restart: always
    ports:
      - 8080:8080
volumes:
  db:
    driver: local
```

## Run the backend
Depends on what you want to do :) Here are some possibilities
- Run the `.csproj` file with dotnet
- Run a published executable
- Start a container (see `backend/docker-compose.yml`)

### Configuration

#### Set the connection string for the PostgreSQL database
There are several possibilties to set the connection string :)

Option 1: Modifiy the `appsettings*.json` for modification during development
```json
"ConnectionStrings": {
    "PostgresDatabase": "Host=localhost:5432;Database=meal-mate;Username=postgres;Password=postgres"
}
```

Option 2: Set an environment variable (e.g. production)
```
ConnectionStrings__PostgresDatabase=Host=localhost;Port=5432;Database=meal-mate;Username=postgres;Password=postgres
```

## Run the frontend
- Local development: `cd frontend && npm run dev`

### Configuration

#### Set the backend-server-url environment variable

Option 1: Use a `frontend/.env.development`
```
VITE_API_SERVER=https://localhost:44352
```

