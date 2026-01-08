# Uzoma API

This repository contains the **Uzoma API**, a backend built with **ASP.NET Core 10**, using **Entity Framework Core** and **JWT Authentication**. It supports a React frontend for an e-commerce platform.

---

## Base URL
http://localhost:5248



---

## Authentication Endpoints

| Endpoint            | Method | Description                  | Body Example |
|--------------------|--------|------------------------------|--------------|
| `/api/auth/register` | POST   | Register a new user          | `{ "name": "John Doe", "email": "john@example.com", "password": "Password123!", "role": "User" }` |
| `/api/auth/login`    | POST   | Login and get JWT token      | `{ "email": "john@example.com", "password": "Password123!" }` |

> **Note:** Use the JWT token from login for protected endpoints.

---

## Categories

| Endpoint                  | Method | Description                  | Auth Required |
|---------------------------|--------|------------------------------|---------------|
| `/api/categories`          | GET    | Get all categories           | No            |
| `/api/categories`          | POST   | Create a new category        | Admin only    |
| `/api/categories/debug`    | GET    | Debug endpoint               | No            |

### Example POST Body

```json
{
  "name": "Hoodies",
  "slug": "hoodies"
}

Products
Endpoint	Method	Description	Auth Required
/api/products	GET	Get all products	No
/api/products/{id}	GET	Get single product by ID	No
/api/products	POST	Create product	Admin only
/api/products/{id}	PUT	Update product	Admin only
/api/products/{id}	DELETE	Delete product	Admin only

Example POST Body
{
  "items": [
    { "productId": 1, "quantity": 2 },
    { "productId": 3, "quantity": 1 }
  ]
}


JWT Authentication

All protected endpoints require a Bearer token in headers:

Authorization: Bearer <JWT_TOKEN>


The token is obtained from /api/auth/login.


React Frontend Integration

React can call these endpoints using fetch or Axios:

// Example using fetch
const token = localStorage.getItem("jwt");
const res = await fetch("http://localhost:5248/api/categories", {
  headers: { Authorization: `Bearer ${token}` }
});
const data = await res.json();
console.log(data);