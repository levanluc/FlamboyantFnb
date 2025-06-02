import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ManageRoutes } from './manage.routes';
import { ManageComponent } from './manage.component';
import { MenuComponent } from './menu/menu.component';
import { SidebarComponent } from './sidebar/sidebar.component';

@NgModule({
  declarations: [
    ManageComponent,
    MenuComponent,
    SidebarComponent,
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(ManageRoutes),
  ],
  providers: []
})
export class ManageModule { }
