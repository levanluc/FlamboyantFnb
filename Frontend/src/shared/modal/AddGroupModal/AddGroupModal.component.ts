import { DialogRef } from '@ngneat/dialog';
import { Component, ChangeDetectionStrategy, inject, ChangeDetectorRef } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { CommonModule } from '@angular/common';

interface GroupData {
  groupName: string;
  groupCode?: string;
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
  private cdr: ChangeDetectorRef = inject(ChangeDetectorRef);

  onSubmit(form: NgForm) {
    if (form.valid) {
      this.ref.close(true);
    } else {
      Object.values(form.controls).forEach(control => control.markAsTouched());
    }
  }

  closeModal(result?: any) {
    this.ref.close(result);
  }
}
