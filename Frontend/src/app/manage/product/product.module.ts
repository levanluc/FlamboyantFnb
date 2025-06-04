import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { ProductComponent } from './product.component';
import { TableModule } from 'primeng/table';

const routes: Routes = [
  { path: '', component: ProductComponent } // Default route for ProductModule
];

@NgModule({
  declarations: [ProductComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes), // Configure routing for ProductComponent
    TableModule
  ]
})
export class ProductModule { }
