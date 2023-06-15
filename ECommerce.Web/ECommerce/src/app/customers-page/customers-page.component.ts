import { CustomersService } from './../customers.service';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Customer } from 'src/app/Customer';
import { Router } from '@angular/router';

@Component({
  selector: 'app-customers-page',
  templateUrl: './customers-page.component.html',
  styleUrls: ['./customers-page.component.css']
})
export class CustomersPageComponent implements OnInit {

  form: any;
  customers: any;
  title!: string;
  name!: string;
  customerId!: number;

  viewTable: boolean = true;
  viewForm: boolean = false;

  constructor(private customersService: CustomersService, private router: Router) {}

  ngOnInit(): void {
    this.customersService.GetCustomers()
      .subscribe(result => {
        this.customers = result;
      });
    }

    ShowAddForm(): void {
      this.viewTable = false;
      this.viewForm = true;
      this.title = 'New Customer';
      this.form = new FormGroup({
        name: new FormControl(null),
        address: new FormControl(null)
      });
    }
    
    Voltar(): void {
      this.viewForm = true;
      this.viewForm = false;
    }

    SendForm(): void {
      const customer: Customer = this.form.value;
  
      if (customer.customerId > 0) {
        this.customersService.UpdateCustomer(customer).subscribe(() => {
          this.viewForm = false;
          this.viewTable = true;

          alert('Customer updated');

          this.customersService.GetCustomers().subscribe((records) => {
            this.customers = records;
          });
        });
      } else {
        this.customersService.SaveCustomer(customer).subscribe(() => {
          this.viewForm = false;
          this.viewTable = true;

          alert('Customer saved');

          this.customersService.GetCustomers().subscribe((records) => {
            this.customers = records;
          });
        });
      }
    }
  
    ShowUpdateForm(customerId: number): void {
      this.viewTable = false;
      this.viewForm = true;
  
      this.customersService.GetCustomer(customerId).subscribe((result) => {
        this.title = `Update ${result.name} ${result.address}`;
  
        this.form = new FormGroup({
          customerId: new FormControl(result.customerId),
          name: new FormControl(result.name),
          address: new FormControl(result.address)
        });
      });
    }
  
    DeleteCustomer(customerId: number){
      this.customersService.DeleteCustomer(customerId).subscribe(() => {
        alert('Customer deleted');

        this.customersService.GetCustomers().subscribe(records => {
          this.customers = records;
        });
      });
    }

    ViewOrders(customerId: any): void {
      this.router.navigate(['/orders'],
        { queryParams: {customerId: customerId }}
      );
    }
  }
