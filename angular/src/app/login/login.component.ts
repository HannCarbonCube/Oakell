import { Component } from '@angular/core';
import { AuthService } from '@abp/ng.core';
import { LoginParams } from '@abp/ng.core';
import { OakellAuthService } from '../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
//This is a custom login component and not the component currently in use on the frontend.
//This component will be used later on when custom login screen gets built
//Destroy this comments when that happens
export class LoginComponent {
  username: string = '';
  password: string = '';

  constructor(
    private authService: AuthService,
    private oakellAuthService: OakellAuthService
  ) {}

  onSubmit() {
    const loginParams: LoginParams = {
      username: this.username,
      password: this.password,
      rememberMe: true,
      redirectUrl: '/home'
    };

    this.authService.login(loginParams).subscribe(response => {
      console.log('Login successful', response);
    }, error => {
      console.error('Login failed', error);
    });
  }
}
