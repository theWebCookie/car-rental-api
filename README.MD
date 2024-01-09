# REST API for [Car Rent](https://github.com/theWebCookie/car-rental)

## Cars API

### 1. Retrieve all Cars

- **Method**: GET
- **Endpoint**: `/cars/`
- **Action**: Fetches a list of all cars.

### 2. Retrieve a Car by ID

- **Method**: GET
- **Endpoint**: `/cars/{id}`
- **Action**: Retrieves information about a car based on the provided ID.

### 2. Retrieve a Car by carType

- **Method**: GET
- **Endpoint**: `/cars/type/{carType}`
- **Action**: Retrieves information about a car based on the provided carType.

### 3. Add a new Car

- **Method**: POST
- **Endpoint**: `/cars/`
- **Action**: Creates a new car based on the provided data.

### 4. Update information for a Car by ID

- **Method**: PUT
- **Endpoint**: `/cars/{id}`
- **Action**: Updates information about a car based on the provided data.

### 5. Delete a Car by ID

- **Method**: DELETE
- **Endpoint**: `/cars/{id}`
- **Action**: Deletes a car based on the provided ID.

## Reservations API

### 1. Retrieve all Reservations

- **Method**: GET
- **Endpoint**: `/reservations/`
- **Action**: Fetches a list of all reservations.

### 2. Retrieve a Reservation by ID

- **Method**: GET
- **Endpoint**: `/reservations/{id}`
- **Action**: Retrieves information about a reservation based on the provided ID.

### 3. Add a new Reservation

- **Method**: POST
- **Endpoint**: `/reservations/`
- **Action**: Creates a new reservation based on the provided data.

### 4. Update information for a Reservation by ID

- **Method**: PUT
- **Endpoint**: `/reservations/{id}`
- **Action**: Updates information about a reservation based on the provided data.

### 5. Delete a Reservation by ID

- **Method**: DELETE
- **Endpoint**: `/reservations/{id}`
- **Action**: Deletes a reservation based on the provided ID.

## Users API

### 1. Retrieve all Users

- **Method**: GET
- **Endpoint**: `/users/`
- **Action**: Fetches a list of all users.

### 2. Retrieve a User by ID

- **Method**: GET
- **Endpoint**: `/users/{id}`
- **Action**: Retrieves information about a user based on the provided ID.

### 3. Add a new User

- **Method**: POST
- **Endpoint**: `/users/`
- **Action**: Creates a new user based on the provided data.

### 4. Update information for a User by ID

- **Method**: PUT
- **Endpoint**: `/users/{id}`
- **Action**: Updates information about a user based on the provided data.

### 5. Delete a User by ID

- **Method**: DELETE
- **Endpoint**: `/users/{id}`
- **Action**: Deletes a user based on the provided ID.
