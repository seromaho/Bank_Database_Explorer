# How to use this repository

- set up [MSSQL Server](https://www.microsoft.com/en-us/sql-server) at `localhost:1433`; MSSQL login name: `SA`; MSSQL SA password: `P@ssw0rd`
- set up [Redis](https://redis.io/) at `localhost:6379`

   .. or ..

- use the included **Docker Compose** file (`docker-compose.yml`) to set up both of the above, using [Docker](https://www.docker.com/)

1. start up the `Bank_Database_MVC` project

# Solution Features

- query a database of German banking houses in a web browser

   ![screenshot](Bank_Database_MVC\Data\Images\screenshot-query-page.png)

- get input validation while you type

   ![screenshot](Bank_Database_MVC\Data\Images\screenshot-query-page-input-1.png)

- get the query results from the database

   ![screenshot](Bank_Database_MVC\Data\Images\screenshot-query-result-144-database.png)

- get the query results from a distributed cache

   ![screenshot](Bank_Database_MVC\Data\Images\screenshot-query-result-144-cache.png)
