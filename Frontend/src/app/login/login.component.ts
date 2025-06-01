import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import UserDataService from '../../services/userdata.service';
import { UserLoginRequest } from '../../models/requests/user-login-request.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  username = '';
  password = '';

  constructor(private userDataService: UserDataService, private router: Router) { }
  ngOnInit() {
  }

  onLogin(loginForm: NgForm) {
    if (loginForm.valid) {
      const request: UserLoginRequest = {
        username: this.username,
        password: this.password
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
          // Handle login error
          console.error('Login failed:', error);
        }
      });
    }
  }
}
