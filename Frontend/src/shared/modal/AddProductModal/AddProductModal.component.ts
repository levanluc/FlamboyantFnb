import { DialogService, DialogRef } from '@ngneat/dialog';
import { Component, ChangeDetectionStrategy, inject, ChangeDetectorRef } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { NumberOnlyDirective } from '../../../app/directives/number-only.directive';

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
  imageUrl?: string;
  imageFile?: File;
}

@Component({
  templateUrl: './add-product-modal.component.html',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    NumberOnlyDirective, // Assuming NumberOnlyDirective is imported correctly
    // Add NumberOnlyDirective to imports
  ],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AddProductModalComponent {
  ref: DialogRef<Data, boolean> = inject(DialogRef);
  formData: Data = { title: 'Thêm hàng mới' };
  productTab: 'info' | 'stock' = 'info';
  private cdr: ChangeDetectorRef = inject(ChangeDetectorRef);

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

  onImageSelected(event: Event) {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files[0]) {
      const file = input.files[0];
      const reader = new FileReader();
      reader.onload = (e: any) => {
        this.formData.imageUrl = e.target.result; // Store base64 string or preview URL
        this.cdr.detectChanges(); // Trigger change detection to update the view immediately
      };
      reader.readAsDataURL(file);
      // Optionally, store the file itself for upload
      this.formData.imageFile = file;
    }
  }
}
