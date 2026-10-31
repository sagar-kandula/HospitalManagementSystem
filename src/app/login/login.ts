
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { Auth, LoginRequest } from '../services/auth';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login {
  email = '';
  password = '';
  message = '';
  isLoading = false;

  constructor(
    private auth: Auth,
    private router: Router
  ) { }

  onLogin(): void {
    if (!this.email || !this.password) {
      this.message = 'Please enter your email and password.';
      return;
    }

    const credentials: LoginRequest = {
      email: this.email,
      password: this.password
    };

    this.isLoading = true;
    this.message = '';

    this.auth.login(credentials).subscribe({
      next: (response) => {
        this.isLoading = false;

        console.log('Login successful. Role:', response.role);

        this.router.navigate(['/dashboard']);
      },
      error: (error) => {
        this.isLoading = false;

        this.message =
          error.status === 401
            ? 'Invalid email or password.'
            : 'Unable to log in. Check that the backend is running.';
      }
    });
  }
}
