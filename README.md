# SaunaApi

- RESTful Api for Sauna-rent WebSite

# Models

- ## User

  - **id**  - int (read-only)
  - **username** - string (required) max 64 chars
  - **email** - string (required) max 64 chars
  - **firstname** - string (required)
  - **lastname** - string (required)
  - **password** - string (required)
  - **access_token** - string (read-only) - Bearer token for authentication
  - **created_at** - date (read-only)

- ## BookedTime

  - **id** - int (read-only)
  - **User_id** - int (read-only)
  - **Booked_time** - date (read-only)

- ## Order

  - **id** - int (read-only)
  - **User_id** - int (required)
  - **List<BookedTime> ListTime** - List of DateTime
  - **Price** - Decimal (required)
  - **Is_it_payd** - Boolean (required)
  - **created_at** - date (read-only)

# List of API calls
- ## User 

  - **GET /api/User/{id}** - Get user

  - **PUT /api/User /{id}** - Update user profile

  - **POST /api/User** - For creating a new user, doesn't need an authentication token 

  - **POST /api/User/get-token** - To get access_token again, send your username and password  
 

- ## BookedTime 

  - **GET /api/BookerTime** - Get list of booked times 

  - **GET /api/BookerTime/{id}** - Get details of specific booked time 

  - **PUT /api/BookerTime/{id}** - Change booked time 

  - **POST /api/BookerTime** - Book new time 

  - **DELETE /api/BookerTime/{id}** - Cancel booked time 

- ## Order 

  - **GET /api/Order** - Get list of orders 

  - **GET /api/Order/{id}** - Get details of specific order 

  - **PUT /api/Order/{id}** - Update order status 

  - **POST /api/Order** - Create a new order 

  - **DELETE /api/Order/{id}** - Delete order 


# ***User Methods***

- # Create an account

  - Doesn't need an authentication token

## POST /api/User

### Body: 

{ 

    "username": "andrus234", 

    “email”: “andrus@gmail.com”, 

    "firstname": "Andrus", 

    "lastname": "Remets", 

    "password": "red123" 

} 

### Response: 

{ 

    "id": 1, 

    "username": "andrus234", 

    “email”: “andrus@gmail.com”, 

    "firstname": "Andrus", 

    "lastname": "Remets", 

    "created_at": "2024-03-20 15:25:15", 

    "access_token": "Eo6KKi5AghYvFczGSRnI9T1_ZQEUDMA0" 

} 

- # Get authentication token 

  - Doesn't need an authentication token 

## POST /api/User/get-token 

### Body: 

{ 

    "username": "andrus234", 

    "password": "red123" 

} 

### Resonse:

{ 

    "access_token": "Eo6KKi5AghYvFczGSRnI9T1_ZQEUDMA0" 

} 

- # View profile 

  - Access token needed

## GET /users/{id} 

### Body: 

empty 

### Response: 

{ 

        "id": 1, 

        "username": "andrus@gmail.com", 

        “email”: “andrus@gmail.com”, 

        "firstname": "Andrus", 

        "lastname": "Remets" 

} 

- # Update profile
  - Access token needed
  - Possible change password, email or both

## PUT /users/{id} 

### Body: 
{ 

    "password": "qwe123asd" 

} 
#### or
{ 

    “email”: “andrus2@gmail.com” 

} 
### Response: 

{ 

        "username": "andrus@gmail.com", 

        “email”: “andrus@gmail.com”, 

        "firstname": "Andrus", 

        "lastname": "Remets" ,
        
        "password": "qwe123asd"

} 

# ***BookedTime Methods***

- # Create a time booking
  - Doesn't need an authentication token 

## POST /api/BookedTime 

### Body: 

{ 

    "User_id": 1, 

    “Booked_time”: “2024-04-01T10:00:00”

} 

### Response: 

{ 

    "id": 1, 

    "User_id": 1, 

    “Booked_time”: “2024-04-01T10:00:00” 

} 

- # Changing booking time
  - Access token needed
## PUT /api/BookedTime/{id}

### Body:
{ 

    "id": 1, 

    “Booked_time”: “2024-04-01T11:00:00”

} 

### Response: 

{ 

    "id": 1, 

    "User_id": 1, 

    “Booked_time”: “2024-04-01T11:00:00” 

} 

- # Get all booked times
  - Doesn't need an authentication token

## GET /api/BookedTime

### Body:

empty

### Response:

[{ 

    "id": 1, 

    "User_id": 1, 

    “Booked_time”: “2024-04-01T11:00:00” 

},
{ 

    "id": 2, 

    "User_id": 1, 

    “Booked_time”: “2024-04-01T10:00:00” 

} ]

- # Get booked time by id
  - Doesn't need an authentication token

## GET /api/BookedTime/{id}

### Body:

empty

### Response:

{ 

    "id": 1, 

    "User_id": 1, 

    “Booked_time”: “2024-04-01T11:00:00” 

}

- # Delete booking time by id
  - Access token needed
## DELETE /api/BookedTime/{id}

### Body:

{ 

    "id": 1, 

    “access_token”: “rthyj3554yyjyjfd54676i709-4t4t” 

}
### Response:

empty

# ***Order Methods***

- # Create an order
  - Doesn't need an authentication token
## POST /api/Order

### Body:

{

}
