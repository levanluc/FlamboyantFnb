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
      { path: '', redirectTo: 'product', pathMatch: 'full' } // Default route
    ]
  }
];
