import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CustomersPageComponent } from './customers-page/customers-page.component';

import { CustomersService } from './customers.service';
import { HttpClientModule } from '@angular/common/http';

import { ReactiveFormsModule } from '@angular/forms';
import { ModalModule } from 'ngx-bootstrap/modal';
import { OrdersPageComponent } from './orders-page/orders-page.component';
import { OrdersService } from './orders.service';

@NgModule({
  declarations: [
    AppComponent,
    CustomersPageComponent,
    OrdersPageComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    ModalModule.forRoot()
  ],
  providers: [ HttpClientModule, CustomersService, OrdersService ],
  bootstrap: [AppComponent]
})
export class AppModule { }
