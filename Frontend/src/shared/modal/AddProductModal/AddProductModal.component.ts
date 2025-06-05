import { DialogService, DialogRef } from '@ngneat/dialog';
import { Component, ChangeDetectionStrategy, inject, ChangeDetectorRef, Output, EventEmitter, Input, Optional } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { NumberOnlyDirective } from '../../../app/directives/number-only.directive';
import { HttpService } from '../../../utilities/http-service';
import { environment } from '../../../environments/environment';
import UserDataService from '../../../services/userdata.service';
import { Product } from '../../../models/requests/product.model';
import { AddGroupModalComponent } from '../AddGroupModal/AddGroupModal.component';

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
  private httpService = inject(HttpService);
  private userDataService = inject(UserDataService);
  private apiUrl = environment.apiUrl;
  private dialog: DialogService = inject(DialogService);

  @Input() onProductCreated?: (response: any) => void;

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
      this.userDataService.createProduct(form.value).subscribe({
        next: (response) => {
          if (this.onProductCreated) {
            this.onProductCreated(response);
          }
          this.ref.close(true);
        },
        error: (err) => {
          console.error('Error creating product:', err);
        }
      });
    } else {
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
  openProductGroupDialog() {
    // Use DialogService.open and return the observable directly
    const dialogRef = this.dialog.open(AddGroupModalComponent, {
      width: '400px',
      // Use id to avoid duplicate dialogs
      id: 'add-group-modal',
    });
    // DialogService.open returns DialogRef or undefined
    if (dialogRef && typeof dialogRef.afterClosed$?.subscribe === 'function') {
      dialogRef.afterClosed$.subscribe(result => {
        if (result && typeof result === 'object' && 'groupName' in result) {
          // Optionally update the product group list and select the new group
          // Example: this.productGroups.push(result.groupName);
          // this.formData.productGroup = result.groupName;
          this.cdr.markForCheck();
        }
      });
    } else {
      // Fallback: force change detection in case dialog is not shown
      this.cdr.markForCheck();
    }
  }
}
