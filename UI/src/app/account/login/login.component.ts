import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, NgForm, Validators } from '@angular/forms';

import { AuthenticationService } from 'src/app/service/authentication.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  invalidLogin?: boolean;
  loginForm!:FormGroup

  constructor( private authService : AuthenticationService , private fb:FormBuilder) { }

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      username: ['', [
        Validators.required,
      ]],
      password: ['', [Validators.required]],
    });
  }
  onSubmit(){     

    this.authService.Login(
      this.loginForm.get('username')!.value,
      this.loginForm!.get('password')!.value
    );
  }



}
