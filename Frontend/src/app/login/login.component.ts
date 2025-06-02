import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { NgForm } from '@angular/forms';
import UserDataService from '../../services/userdata.service';
import { UserLoginRequest } from '../../models/requests/user-login-request.model';
import { Router } from '@angular/router';
import { HttpStatusCode } from '@angular/common/http';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  username = '';
  password = '';
  errorMessage: string | null = null;

  @ViewChild('username') usernameInput!: ElementRef;

  constructor(private userDataService: UserDataService, private router: Router) { }
  ngOnInit() {
  }

  onLogin(loginForm: NgForm) {
    this.errorMessage = null;
    if (loginForm.valid) {
      const request: UserLoginRequest = {
        username: loginForm.value.username,
        password: loginForm.value.password
      };

      this.userDataService.login(request).subscribe({
        next: (response) => {
          if (response?.Code === 0 && response.Data) {
            localStorage.setItem('user_session', JSON.stringify(response.Data));
            this.router.navigate(['/man']);
          }
          console.log('Login successful:', response);
        },
        error: (error) => {
          console.error('Login failed:', error);
          if (error.status === HttpStatusCode.BadRequest) {
            this.errorMessage = error.error.Message || 'Đăng nhập thất bại.';
            loginForm.resetForm();
            setTimeout(() => {
              this.usernameInput?.nativeElement.focus();
            });
          }
        }
      });
    }
  }
}
