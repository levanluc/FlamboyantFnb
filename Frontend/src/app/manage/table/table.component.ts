import { Component } from '@angular/core';

interface TableItem {
  id: number;
  name: string;
  seats: number;
  status: string;
  createdAt: Date;
}

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.css']
})
export class TableComponent {
  rows: TableItem[] = [
    { id: 1, name: 'Bàn 1', seats: 4, status: 'Đang sử dụng', createdAt: new Date('2025-06-08T10:00:00') },
    { id: 2, name: 'Bàn 2', seats: 6, status: 'Trống', createdAt: new Date('2025-06-07T09:00:00') }
  ];
  selectedTables: TableItem[] = [];
  page = { size: 15, totalElements: 2 };

  // Popup control
  showAddModal = false;
  showEditModal = false;
  showDeleteModal = false;
  currentEditTable: TableItem | null = null;
  currentDeleteTable: TableItem | null = null;

  setPage(event: any) {
    this.page.size = event.rows;
  }

  openAddModal() {
    this.showAddModal = true;
  }
  closeAddModal() {
    this.showAddModal = false;
  }

  openEditModal(table: TableItem) {
    this.currentEditTable = { ...table };
    this.showEditModal = true;
  }
  closeEditModal() {
    this.showEditModal = false;
    this.currentEditTable = null;
  }

  openDeleteModal(table: TableItem) {
    this.currentDeleteTable = table;
    this.showDeleteModal = true;
  }
  closeDeleteModal() {
    this.showDeleteModal = false;
    this.currentDeleteTable = null;
  }
}
