import { DialogService, DialogRef } from '@ngneat/dialog';
import { Component, ChangeDetectionStrategy, inject } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';

interface Data {
  title: string;
  productCode?: string;
  barcode?: string;
  productName?: string;
  productGroup?: string;
  costPrice?: number;
  sellingPriceBeforeTax?: number;
  sellingPriceAfterTax?: number;
  inventory?: number;
}

@Component({
  templateUrl: './add-product-modal.component.html',
  standalone: true,
  imports: [FormsModule],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AddProductModalComponent {
  ref: DialogRef<Data, boolean> = inject(DialogRef);
  formData: Data = { title: 'Thêm hàng mới' };

  get title() {
    return this.ref.data?.title || this.formData.title || 'Hello world';
  }

  constructor() {
    if (this.ref.data) {
      this.formData = { ...this.formData, ...this.ref.data };
    }
  }

  onSubmit(form: NgForm) {
    if (form.valid) {
      console.log('Form Submitted!', form.value);
      this.ref.close(form.value);
    } else {
      console.log('Form is invalid');
      Object.values(form.controls).forEach(control => {
        control.markAsTouched();
      });
    }
  }

  closeModal(result?: any) {
    this.ref.close(result);
  }
}
