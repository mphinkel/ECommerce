import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CustomersPageComponent } from './customers-page/customers-page.component';
import { OrdersPageComponent } from './orders-page/orders-page.component';

const routes: Routes = [
  { path: 'customers', component: CustomersPageComponent, pathMatch: 'full' },
  { path: 'orders', component: OrdersPageComponent, pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
