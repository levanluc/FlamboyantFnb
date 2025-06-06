import { DialogRef } from '@ngneat/dialog';
import { Component, ChangeDetectionStrategy, inject, ChangeDetectorRef } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { CategoryDataService } from '../../../services/category-data.service';

interface GroupData {
  groupName: string;
}

@Component({
  templateUrl: './add-group-modal.component.html',
  standalone: true,
  imports: [CommonModule, FormsModule],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AddGroupModalComponent {
  ref: DialogRef<GroupData, boolean> = inject(DialogRef);
  formData: GroupData = { groupName: '' };
  constructor(private categoryDataService: CategoryDataService, private cdr: ChangeDetectorRef) {
    // Initialize formData if needed
    this.formData = { groupName: ''};
  }

  onSubmit(form: NgForm) {
    if (form.valid) {
      var newCategoryRequest = {
        Name: this.formData.groupName
      };
      this.categoryDataService.createCategory(newCategoryRequest).subscribe({
        next: (response) => {
          this.ref.close(response);
        },
        error: (err) => {
          // Optionally handle error (show message, etc.)
        }
      });
    } else {
      Object.values(form.controls).forEach(control => control.markAsTouched());
    }
  }

  closeModal(result?: any) {
    this.ref.close(result);
  }
}
