# MVC for Companies

## Overview

This MVC Dashboard project is designed as a 4-tier architecture solution for small companies. It manages departments, users, roles, and employees, providing a comprehensive system for business operations.

## Key Features

- **Architecture**: 4-tier architecture for modularity and scalability.
- **Technologies**: 
  - **Language**: C#
  - **Framework**: ASP.NET MVC with Entity Framework
  - **Database**: Microsoft SQL Server
- **Authentication**: Secure Identity-based authentication system for data integrity and user access control.

## Project Structure

- **Company.data**: Data Access Layer, handling database operations.
- **Company.Repository**: Implements repository patterns to abstract and encapsulate data access logic
- **Company.Service**: Business Logic Layer, containing business rules and logic.
- **Company.Web**: Presentation Layer, managing UI and user interactions.

## Environment Variables
For security purposes, sensitive data such as connection strings, email credentials, and API keys should be stored in environment variables, not directly in the appsettings.json file. Replace the placeholders in the appsettings.json file with environment variables in your local or server environment.

## Getting Started

### Prerequisites

- .NET SDK
- SQL Server


## Usage

- **Dashboard**: Access the dashboard to manage company operations.
- **Departments**: CRUD operations for department management.
- **Users and Roles**: Manage users and their roles for access control.
- **Employees**: Manage employee information and records.

