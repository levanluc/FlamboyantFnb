import { Component, inject, OnInit } from '@angular/core';
import { DialogService } from '@ngneat/dialog';
import { AddProductModalComponent } from '../../../shared/modal/AddProductModal/AddProductModal.component';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrl: './product.component.css'
})
export class ProductComponent implements OnInit {
  dialogService = inject(DialogService);
  ngOnInit() {
    const dialogRef = this.dialogService.open(AddProductModalComponent, {
      data: {
        title: '',
      },
    });
  }
}
