import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TableComponent } from './table.component';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { TableModule as PrimeTableModule } from 'primeng/table';

const routes: Routes = [
  { path: '', component: TableComponent }
];

@NgModule({
  declarations: [TableComponent],
  imports: [CommonModule, RouterModule.forChild(routes), FormsModule, PrimeTableModule],
  exports: [TableComponent]
})
export class TableModule {}
