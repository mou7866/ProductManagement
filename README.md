
# **Product and Category Management System**

This project is a **Full Stack .NET & Angular application** for managing products and categories, designed to run in a **Dockerized environment** with **PostgreSQL**, **Redis**, and **PgAdmin**.

---

## **Prerequisites**

Ensure you have the following installed on your machine:

- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- [Node.js](https://nodejs.org/) (for local Angular development, optional)
- [Dotnet SDK](https://dotnet.microsoft.com/en-us/download/dotnet) (for local .NET development, optional)

---

## **Running the Project with Docker**

Follow these steps to set up and run the application using Docker:

### **1. Clone the Repository**

```bash
git clone https://github.com/mou7866/ProductManagement.git
cd ProductManage
```

---

### **2. Build and Start the Containers**

Use Docker Compose to build and start all required services:

```bash
docker-compose up --build
```

This will start the following services:

- **API**: A .NET 8 Web API for managing products and categories.
- **UI**: An Angular-based frontend for interacting with the API.
- **PostgreSQL**: A relational database for storing products and categories.
- **Redis**: A caching layer (optional).
- **PgAdmin**: A PostgreSQL administration tool accessible via a web interface.

---

### **3. Access the Application**

Once all containers are up and running, you can access the different services at the following URLs:

- **Angular UI**: [http://localhost:4200](http://localhost:4200)
- **Swagger API Documentation**: [http://localhost:5000/swagger](http://localhost:5000/swagger)
- **PgAdmin**: [http://localhost:8080](http://localhost:8080)
  - **Default Email**: `admin@localhost.com`
  - **Default Password**: `admin`

---

## **Environment Variables**

The application uses the following environment variables, defined in `docker-compose.yml`:

### **API Environment Variables**

| Variable                     | Description                                     |
|------------------------------|-------------------------------------------------|
| `ASPNETCORE_ENVIRONMENT`     | Sets the environment (Development/Production).  |
| `ConnectionStrings__DefaultConnection` | Connection string for PostgreSQL.           |

### **PgAdmin Environment Variables**

| Variable                | Description                      |
|-------------------------|----------------------------------|
| `PGADMIN_DEFAULT_EMAIL` | Default login email for PgAdmin. |
| `PGADMIN_DEFAULT_PASSWORD` | Default login password for PgAdmin. |

---

## **Database Setup**

The PostgreSQL container automatically creates the database `ProductManagementDB`. If you need to modify or manually set up the database schema, you can use the included Liquibase migrations.

### **Running Liquibase Migrations**

To manually apply database migrations using Liquibase:

```bash
docker-compose run liquibase update
```

---

## **Stopping the Containers**

To stop all running containers:

```bash
docker-compose down
```

This will stop and remove all containers.

---

## **Troubleshooting**

### **Common Issues**

1. **Port Already in Use**  
   If you encounter port conflicts, make sure no other applications are using ports **5000**, **5001**, **4200**, **5435**, or **8080**.

2. **Database Connection Issues**  
   Ensure the PostgreSQL container is running and that the connection string in `docker-compose.yml` is correct.

3. **Swagger Not Accessible**  
   Check the API container logs using:

   ```bash
   docker-compose logs api
   ```

---

## **Future Enhancements**

- Add role-based access control (RBAC) for user management.
- Implement product import/export functionality.
- Add unit and integration tests for better code coverage.

---

## **Contributors**

- **Mohammed Yusuf Essa** – [GitHub Profile](https://github.com/mou7866)

