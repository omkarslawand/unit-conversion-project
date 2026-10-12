# Unit Converter API

## Overview

Unit Converter API is an ASP.NET Core Web API that converts numerical values between different units of measurement.

The application currently supports the following conversion categories:

- Length
- Weight
- Temperature
- Volume
- Area
- Time

The solution follows a data-driven approach where units and conversion factors are stored in a JSON configuration file (`Data/units.json`). This allows new units and categories to be added with minimal code changes.

---

## How to Run the Project Locally

### Prerequisites

- .NET 8 SDK (or latest stable .NET SDK)

Verify installation:

```bash
dotnet --version
```

### Clone the Repository

```bash
git clone <repository-url>
cd unit-converter-api
```

### Restore Dependencies

```bash
dotnet restore
```

### Build the Project

```bash
dotnet build
```

### Run the Application

```bash
dotnet run
```

The API will start locally and display the application URLs in the console.

### Access Swagger

Open the Swagger UI in your browser:

```text
https://localhost:<port>/swagger
```

Swagger can be used to view and test all available API endpoints.

---

## Available Endpoints

### Get All Categories

```http
GET /api/Conversion/categories
```

Returns all supported conversion categories.

### Get Units by Category

```http
GET /api/Conversion/categories/{category}/units
```

Returns all units available within the specified category.

Example:

```http
GET /api/Conversion/categories/Weight/units
```

### Convert Units

```http
POST /api/Conversion
```

Example Request:

```json
{
  "category": "Weight",
  "fromUnit": "Kilogram",
  "toUnit": "Gram",
  "value": 3
}
```

Example Response:

```json
{
  "success": true,
  "message": "Conversion Successful from 'Kilogram' to 'Gram'.",
  "data": 3000
}
```

---

## Using the API

The API can be tested using Swagger UI, Postman, or any HTTP client.

### Step 1: Get Available Categories

Request:

```http
GET /api/Conversion/categories
```

Example Response:

```json
{
  "success": true,
  "message": "Categories Fetched Successfully.",
  "data": ["Length", "Weight", "Volume", "Temperature", "Time", "Area"]
}
```

Use this endpoint to discover all supported conversion categories.

---

### Step 2: Get Units for a Category

Request:

```http
GET /api/Conversion/categories/Weight/units
```

Example Response:

```json
{
  "success": true,
  "message": "Units fetched successfully from category 'Weight'.",
  "data": ["Kilogram", "Gram", "Pound"]
}
```

Use this endpoint to retrieve all units available within a specific category.

---

### Step 3: Convert a Value

Request:

```http
POST /api/Conversion
```

Request Body:

```json
{
  "category": "Weight",
  "fromUnit": "Kilogram",
  "toUnit": "Gram",
  "value": 3
}
```

Example Response:

```json
{
  "success": true,
  "message": "Conversion Successful from 'Kilogram' to 'Gram'.",
  "data": 3000
}
```

The API converts the supplied value from the source unit to the target unit and returns the converted result.

---

### Example Workflow

Convert 5 Kilometers to Meters:

1. Call:

```http
GET /api/Conversion/categories
```

2. Verify that `Length` is a supported category.

3. Call:

```http
GET /api/Conversion/categories/Length/units
```

4. Verify that `Kilometer` and `Meter` are available units.

5. Call:

```http
POST /api/Conversion
```

Request Body:

```json
{
  "category": "Length",
  "fromUnit": "Kilometer",
  "toUnit": "Meter",
  "value": 5
}
```

Response:

```json
{
  "success": true,
  "message": "Conversion Successful from 'Kilometer' to 'Meter'.",
  "data": 5000
}
```

---

## Design Decisions

### 1. Service-Based Architecture

Business logic is separated from controllers using services and interfaces. This keeps controllers lightweight and improves maintainability and testability.

### 2. Data-Driven Configuration

Units and conversion factors are stored in `Data/units.json` rather than hardcoded throughout the application. This allows new categories and units to be added without modifying the conversion engine.

### 3. Generic Conversion Logic

Length, Weight, Volume, Area, and Time conversions use a common factor-based conversion mechanism. Each category has a base unit, and conversions are performed through that base unit.

Example:

```text
Kilometer
   ↓
Meter (Base Unit)
   ↓
Foot
```

### 4. Dedicated Temperature Conversion Service

Temperature conversions require formulas rather than simple multiplication factors. A separate `TemperatureConversionService` was implemented to handle temperature-specific conversion logic.

Example:

```text
Fahrenheit
   ↓
Celsius (Base Unit)
   ↓
Kelvin
```

### 5. Consistent API Responses

All endpoints return a generic `ApiResponse<T>` object containing:

- Success status
- Message
- Data

This provides a consistent response structure across the API.

### 6. Case-Insensitive Unit and Category Handling

The application supports values such as:

```text
meter
Meter
METER
```

and treats them as the same unit to improve usability.

---

## Trade-Offs and Future Improvements

For simplicity, unit definitions are currently stored in a JSON file. In a production environment, unit configurations could be stored in a database and managed through an administrative interface.

Possible future enhancements include:

- Global exception handling middleware
- Unit and integration tests
- Database-backed unit configuration
- Caching frequently used conversion data
- API versioning
- Additional conversion categories

---

## Technologies Used

- ASP.NET Core Web API
- C#
- Dependency Injection
- Swagger / OpenAPI
- JSON Configuration
