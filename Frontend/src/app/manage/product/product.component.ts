import { Component, inject, OnInit } from '@angular/core';
import { DialogService } from '@ngneat/dialog';
import { AddProductModalComponent } from '../../../shared/modal/AddProductModal/AddProductModal.component';
import { LazyLoadEvent } from 'primeng/api'; // Import PrimeNG LazyLoadEvent

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrl: './product.component.css'
})
export class ProductComponent implements OnInit {
  dialogService = inject(DialogService);

  rows: any[] = []; // Holds the data for the current page
  selectedProducts: any[] = []; // Holds selected products
  expandedRowId: number | null = null;

  // Pagination settings
  page = {
    size: 15, // Number of items per page (corresponds to PrimeNG 'rows' property)
    totalElements: 0, // Total number of items (corresponds to PrimeNG 'totalRecords')
    pageNumber: 0 // Current page number (0-indexed)
  };

  productTab: { [productId: number]: 'info' | 'stock' } = {}; // Correctly typed object

  ngOnInit() {
    this.loadProductData();
  }

  /**
   * Handles page change event from PrimeNG table
   * @param event LazyLoadEvent from PrimeNG
   */
  setPage(event: LazyLoadEvent) {
    // event.first is the offset of the first record
    // event.rows is the number of rows per page (new page size)
    this.page.pageNumber = (event.first !== undefined && event.rows !== undefined && event.rows > 0)
                           ? Math.floor(event.first / event.rows)
                           : 0;
    this.page.size = event.rows !== undefined ? event.rows : 15;

    // If you handle sorting server-side, you'd also use event.sortField and event.sortOrder here
    this.loadProductData();
  }

  loadProductData() {
    // This is where you would typically make an HTTP request to your backend
    // Pass this.page.pageNumber, this.page.size, sortField, sortOrder etc.
    // For now, we'll simulate it with mock data

    const first = this.page.pageNumber * this.page.size; // Calculate offset for slicing

    // Mock data source (replace with actual service call)
    const mockDataSource = Array.from({ length: 58698 }, (_, i) => ({
      id: i + 1, // Essential for dataKey="id" in p-table for selection
      productCode: `SP${9934431572 - i}`,
      productName: i % 3 === 0 ? `[SKU${i}] - Product Name ${i} with a very long description that might overflow` : `Product ${i}`,
      priceBeforeTax: (10000 + i * 100).toLocaleString(),
      costPrice: (2000 + i * 50).toLocaleString(),
      supplierOrder: i % 5,
      inventory: i % 10,
      customerOrder: i % 3,
      createdAt: new Date(Date.now() - i * 24 * 60 * 60 * 1000).toLocaleDateString('vi-VN')
    }));

    this.rows = mockDataSource.slice(first, first + this.page.size);
    this.page.totalElements = mockDataSource.length;

    // Note: For checkbox selection with server-side data, if you navigate pages,
    // selectedProducts might need more sophisticated management if you want selections
    // to persist across pages when data is reloaded.
    // For now, selection is only for the current page's data.
  }

  openAddProductModal() {
    this.dialogService.open(AddProductModalComponent, {
      data: {
        title: 'Thêm hàng mới',
      },
    });
  }

  onRowClick(product: any) {
    if (this.expandedRowId === product.id) {
      this.expandedRowId = null;
    } else {
      this.expandedRowId = product.id;
      // Ensure the value assigned is either 'info' or 'stock'
      if (!this.productTab[product.id]) {
        this.productTab[product.id] = 'info'; // Default to 'info'
      }
    }
  }

  cleanNumber(value: any): number | null {
    if (value == null || value === '') return null;
    // Remove commas and convert to number
    const num = Number((value + '').replace(/,/g, ''));
    return isNaN(num) ? null : num;
  }

  parseDate(dateStr: string): Date | null {
    if (!dateStr) return null;
    const parts = dateStr.split('/');
    if (parts.length !== 3) return null;
    // day/month/year
    const day = parseInt(parts[0], 10);
    const month = parseInt(parts[1], 10) - 1; // JS months are 0-based
    const year = parseInt(parts[2], 10);
    return new Date(year, month, day);
  }
}
