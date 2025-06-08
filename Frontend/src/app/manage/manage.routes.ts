import { Routes } from '@angular/router';
import { ManageComponent } from './manage.component';

export const ManageRoutes: Routes = [
  {
    path: '',
    component: ManageComponent,
    children: [
      {
        path: 'product',
        loadChildren: () => import('./product/product.module').then(m => m.ProductModule) // Lazy load ProductModule
      },
      {
        path: 'table',
        loadChildren: () => import('./table/table.module').then(m => m.TableModule) // Lazy load TableModule
      },
      { path: '', redirectTo: 'product', pathMatch: 'full' } // Default route
    ]
  }
];
