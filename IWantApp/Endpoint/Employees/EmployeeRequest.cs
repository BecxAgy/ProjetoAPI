﻿namespace IWantApp.Endpoint.Employees;

public record EmployeeRequest(string Email, string Password, string Name, string employeeCode);