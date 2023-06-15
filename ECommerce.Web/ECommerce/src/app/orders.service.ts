import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class OrdersService {
  url = 'http://localhost:5002/api/orders';

  constructor(private http: HttpClient) { }

  GetOrders(customerId: number){
    const apiUrl = `${this.url}/${customerId}`;
    return this.http.get(apiUrl);
  }
}
