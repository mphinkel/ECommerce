import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Customer } from './Customer';
import { Observable } from 'rxjs';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type' : 'application/json'
  })
}

@Injectable({
  providedIn: 'root'
})
export class CustomersService {
  url = 'http://localhost:5001/api/customers';

  constructor(private http: HttpClient) { }

  GetCustomers(){
    return this.http.get(this.url);
  }

  GetCustomer(customerId: number): Observable<Customer> {
    const apiUrl = `${this.url}/${customerId}`;
    return this.http.get<Customer>(apiUrl);
  }

  SaveCustomer(customer: Customer): Observable<any> {
    return this.http.post<Customer>(this.url, customer, httpOptions);
  }

  UpdateCustomer(customer: Customer): Observable<any> {
    return this.http.put<Customer>(this.url, customer, httpOptions);
  }

  DeleteCustomer(customerId: number): Observable<any> {
    const apiUrl = `${this.url}/${customerId}`;
    return this.http.delete<number>(apiUrl, httpOptions);
  }
}
