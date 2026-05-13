# Scheduling Application

A C# Windows Forms desktop application for managing customer appointments. Built as a WGU C969 performance assessment.

## Features

- User authentication with English/Spanish language support
- Location detection via IP API
- Login history logging
- 15-minute appointment alert on login
- Customer CRUD operations
- Appointment CRUD operations with timezone handling
- Calendar view for appointments - in progress
- Reports generated with LINQ and lambda expressions - in progress

## Tech Stack

- C# / .NET Framework
- Windows Forms
- MySQL Database
- MySql.Data NuGet package

## Setup

This is a local development project. The connection string with credentials is hardcoded for simplicity since this runs on a local MySQL instance. In a production app these would be moved to a secure config file.

To run:
1. Install MySQL and create a database called `scheduling_db`
2. Run the included `database_setup.sql` script to create tables
3. Update connection string in `LoginForm.cs` and `MainForm.cs` if needed
4. Build and run in Visual Studio

## Login

Use `test` / `test` for the username and password.

## Screenshots



### Customer Management
![Customers](screenshots/customer.png)

### Appointment Management
![Appointments](screenshots/appointment.png)

### Login Form
![Login Form](screenshots/login.png)