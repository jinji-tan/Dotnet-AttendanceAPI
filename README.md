# Attendance API Features

The backend for the Attendance System is built using ASP.NET Core and Dapper.

## FEATURES

### 1. User Management & Security
- **Account Registration**: Create a new account with a unique username.
- **Secure Login**: Authentication using JWT (JSON Web Tokens).
- **Password Hashing**: Secure storage using BCrypt.

### 2. Attendance Tracking
- **Time In**: Start a new shift. The system prevents multiple active "Time In" sessions.
- **Time Out**: End an active shift.
- **Military Time**: All time tracking is stored and displayed in 24-hour formats.

### 3. Reporting & Analytics
- **Daily Hours**: Automatically calculates total hours worked for the current day.
- **Weekly Hours**: Calculates total hours worked from the start of the current week (Monday).
- **Monthly Hours**: Calculates total hours worked for the current calendar month.

### 4. Data Access
- **Personal Records**: Users can view their own history of "Time In" and "Time Out" logs.
- **Global Logs**: An endpoint is available to view all attendance records for the entire organization.

<!-- REGISTER & LOGIN
![LOGIN API Demo](Asset/)
TOKEN
![TOKEN API Demo](Asset/) -->