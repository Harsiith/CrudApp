import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  // Backend API URL — make sure this matches your .NET URL + controller route
  private apiUrl = 'http://localhost:5280/api/employee';

  constructor(private http: HttpClient) { }

  // GET all employees
  getEmployees(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }

  // GET employee by ID
  getEmployeeById(id: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/${id}`);
  }

  // POST add new employee
  addEmployee(emp: any): Observable<any> {
    return this.http.post(this.apiUrl, emp);
  }

  // PUT update employee
  updateEmployee(id: number, emp: any): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, emp);
  }

  // DELETE employee
  deleteEmployee(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }

  downloadExcel() {
  return this.http.get('http://localhost:5280/api/export/excel', { responseType: 'blob' });
}

downloadPdf() {
  return this.http.get('http://localhost:5280/api/export/pdf', { responseType: 'blob' });
}

}
