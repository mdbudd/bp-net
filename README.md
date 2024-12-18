# .NET Core 6.0 API Boilerplate
Repo to explore .NET Core.

## Quick Start

You'll need Docker installed on your system. Once installed, run..

```
docker compose up -d
```

## Setup Mongo DB

The best way to get the example Books DB into Mongo DB and interacting with the API is to download Compass from Mongo DB. Once installed, connect to `http://localhost:27017`.

Once connected to the docker container, create a new Database called `BookStore` and a collections called `Books`. Import the `/Books.json` data into this collection.

Go to `http://localhost:5069/api/Books` to check database has been updated.

## MS Sql Setup

There are a couple of examples of MS Sql. The first is a built in MS Sql server, running in a container via the Docker Compose file. For the purposes of a Demo, the Products database is created and seeded during build.

Go to `http://localhost:5069/Products?take=10&skip=0` to check database has been seeded.

The second example is a direct connection to an external database server. This one won't work straight away but if you convert the Data Models and change the connection string to an existing server, you'll have quick and easy integration.

## Documentation

Swagger docs are located at `http://localhost:5069/swagger`

## GraphQL

GraphQL endpoint is located at `http://localhost:5069/graphql`. If you navigate to the root of the server in a browser you will find the GraphQL playground pointng to the graphql endpoint.


## JWT Authentication

There is also a rudimentary Authentication system setup. Initial Users (connected via the `UserService`) are currently stored in the MS Sql/SQLite databases & seeded during build for development purposes.