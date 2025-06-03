import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { HttpErrorInterceptor } from '../interceptors/http-error.interceptor';
import { NgxSpinnerModule } from 'ngx-spinner';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { provideHotToastConfig } from '@ngxpert/hot-toast';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  {
    path: 'man', loadChildren: () =>
      import('./manage/manage.module').then(m => m.ManageModule)
  }
];

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent // Add LoginComponent to declarations
  ],
  imports: [
    BrowserModule,
    FormsModule, // <-- Add this line
    RouterModule.forRoot(routes),
    NgxSpinnerModule,
    BrowserAnimationsModule
    // Remove HttpClientModule from here
  ],
  providers: [
    provideHttpClient(withInterceptorsFromDi()), // <-- Add this line
    {
      provide: HTTP_INTERCEPTORS,
      useClass: HttpErrorInterceptor,
      multi: true
    },
    provideHotToastConfig({
      duration: 2000,
      position: 'top-right'
    })
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
