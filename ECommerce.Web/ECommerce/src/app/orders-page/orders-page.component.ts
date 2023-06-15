import { OrdersService } from '../orders.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-orders-page',
  templateUrl: './orders-page.component.html',
  styleUrls: ['./orders-page.component.css']
})
export class OrdersPageComponent implements OnInit {

  orders: any;
  customerId!: number;

  constructor(private ordersService: OrdersService,
    private router: Router, private route: ActivatedRoute) { }

    ngOnInit(): void {
      this.route.queryParams
        .subscribe(params => {
          this.customerId = params['customerId'];
        });
  
      this.ordersService.GetOrders(this.customerId)
        .subscribe(result => {
          this.orders = result;
        });
      }

  Voltar(): void {
    this.router.navigate(['/customers']);
  }

}
